Apterid is a functional language for .NET.  The main design principle is to fit into the .NET ecosystem neatly, not to bolt an ML-style language on top of .NET.

# Types

## Primitives

The following primitive .NET types are available in Apterid:

| Keyword | .NET type      | Example                          | Remarks                                    |
| ------- | -------------- | -------------------------------- | ------------------------------------------ |
| bool    | System.Boolean | **true** **false**               |                                            |
| byte    | System.Byte    | 1b                               | Unsigned octet                             |
| sbyte   | System.SByte   | 1bs                              | Signed octet                               |
| int16   | System.Int16   | 1s                               |                                            |
| uint16  | System.UInt16  | 1su                              |                                            |
| int     |                | 1                                | Alias for `int32`                          |
| int32   | System.Int32   | 1                                |                                            |
| uint32  | System.UInt32  | 1u                               |                                            |
| int64   | System.Int64   | 1l                               |                                            |
| uint64  | System.UInt64  | 1lu                              |                                            |
| ptr     | System.IntPtr  |                                  | A native pointer as a signed integer; either 32 or 64 bits depending on platform |
| uptr    | System.UIntPtr |                                  |                                            |
| char    | System.Char    | 'a'                              | UTF-16 code point                          |
| float   | System.Single  | 1.1f, 1e2f, 1.1e2f, -1.1e-2.2f   |                                            |
| double  | System.Double  | 1.1, 1e2, 1.1e2, -1.1e-2.2       |                                            |
| decimal | System.Decimal | 1.1m                             | Binary-coded decimal                       |
| ()      | System.Void    | ()                               | "unit" type                                |

## Enums

An enumeration is a set of identifiers for numerical values.  The numeric type used to store the enum corresponds to the type of the literals used to initialize the values (which type must be the same for all the initializers).

    type MyEnum =
      Nothing = 0
      Alpha   = 1
      Beta    = 2

The initializers can be expressions.
Uses of enum values must be qualified, e.g. `MyEnum.Alpha`.
You can use the .NET `[Flags]` attribute on enums.

    [Flags]
    type Another =
      Foo = 1 << 0
      Bar = 1 << 1
      Baz = 1 << 2

You can convert enum values to the equivalent value in a numeric type by casting:

    n = <int>Another.Foo

## Records

Records store multiple named values together.

### Structs

Record types are structs by default.

    type MyStructType : Interface1 =
    {
      field1 : int // public by default; initialized to 0
      private field2 = "baz"

      internal Property1 : get = i => fmt "{0} {1}" field2 i; set = i str => { field2 = str; field1 = i }
      Property2 : get : () -> int; set : int -> MyStructType
      private method1 = a b => a + b
      funcField : int -> double
    }

The property setter must take arguments of all the types in the getter's signature, and return the record type.

Fields and properties are conceptually immutable.  You can create a new record with altered values:

    foo = MyStructType { field2 = 1.2 }
    bar = foo { Property1 = "foobar"; field2 = 3.4 }

Omitting the implementation of a getter or setter results in an auto property.

### Classes

    type MyClassType : class BaseClass, Interface1, Interface2 =
    {
      fieldA : int
      b : string

      public MyClassType = a b => { fieldA = a; this.b = b }
      public MyClassType = a b c => { fieldA = a + b; this.b = c }

      virtual Method1 = x y => x + y
      override Method2 = z w => base.Method2 z w
    }

As with structs, property setters must return a new object of the type.

### Interfaces

    type IMyInterface : interface BaseInterface1, BaseInterface2 =
    {
      Property1 : get : () -> string; set : string -> IMyInterface
    }

## Variants

    type VariantExample =
      First<string>
      Second<int>
      Third<MyClassType>

Variant types are always struct types.

You can pattern-match as follows:

    case foo of
      First f -> ...
      Second i -> ...
      Third o -> ...

## Functions

Function types are specified in the usual way:

    f : int -> string -> bool
    f n s = (fmt "{0} {1}" n s) == "1 qux"

## Aliases

    type MyAlias = ISomething<With,Generic,Args>

## Built-In Types

### Options

The `IOption<T>` type is defined as follows:

    type IOption<T> =
      Some<T>
      None

There is special syntax for defining option types:

    foo : int?

There are various operators that return option types.

### Lists

A list consists of a value followed by a list, which may be empty.

    l1 = 'a' :: [ 'a', 'b', 'c' ]
    l2 = 'a' :: 'b' :: 'c' :: []

The type of these values is `IList<char>`.

List elements may be accessed by index:

    l1[0]
    l2[-1]

Negative indices count back from one past the end of the list, so -1 is the index of the last element of the list.

Element access can test for an index and return an option:

    foo : char?
    foo = l1?[128] // None

Lists may be sliced

    l1[0..1] // [ 'a', 'b' ]
    l1[1..]  // [ 'b', 'c' ]
    l1[9..]  // []
    l1[..9]  // [ 'a', 'b' ]

### Tuples

A tuple is a struct with fields Item1 to ItemN.  There is special syntax for creating them:

    (1, 'v')

This value is of type `ITuple<int, char>``.  This is **not** the .NET `Tuple` type!

Tuple items may be accessed by index as well:

    t = (2, 'd')
    t[0] == 2 // true

### Arrays

An array is a .NET array, which can be multidimensional or jagged.

    foo : int||
    bar : char|,|
    baz : double|| ||

    foo = | 1,2,3 |
    bar = | 'a', 'b' |
          | 'c', 'd' |

Array elements can be accessed by index, sliced, and test:

    foo[0]   // 1
    bar[1..] // | 'c', 'd' |
    foo?[1]  // Some 2

### Strings

A value of type `string` is an array of `char` values.

    foo : string
    foo = "something"

As an array, strings can be accessed by index and sliced.

### Maps

Maps are of type `IMap<K,V>`, which is an alias for `IDictionary<K,V>`.

    m : IMap<int, string>
    m = { 0 = "foo", 1 = "bar", 2 = "baz" }

Map elements can be accessed by key, and tested:

    foo = m[0]   // "foo"
    bar = m[123] // None

### Sets

Sets are of type `ISet<T>`:

    m = { 0, 1, 2 }

## Modules

Modules are just static classes; however, they are declared with the `module` keyword instead of `type`.

    module Something =
      Field1 = "foo"
      private field2 = 0
      Property1 : get = 1; set n = this { field2 = n }

These are public by default, but you can declare them private.  References to public members of a module must be qualified.

# Pattern Matching



# Functions

# Generics

# Standard Library
