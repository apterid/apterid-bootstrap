Apterid is a functional language for .NET.  The main design principle is to fit into the .NET ecosystem neatly, not to bolt an ML-style language on top of .NET.

It uses incremental and parallel parsing, analysis and generation so as to fit in nicely with Visual Studio.  The goal is not to immediately show errors in your whole file when you are in the middle of changing or adding something.

Another goal is to make it easy to debug -- currying is accomplished without delegates.  Clean stack traces.

# Identifiers

Identifiers can be:

- The wildcard identifier `_`
- Zero or one `_` code points, followed by one or more alphabetic, numeric or `_` code points, followed by zero or more apostrophes.
- Any string enclosed in backticks.

# Types

There is no difference in Apterid between the name of a type and a reference with that name to the `System.Type` object that defines the type.

System-defined types are static members of the module `Apterid.Types`; user-defined types are static members of the modules they appear in.

## Primitives

The following primitive .NET types are available in Apterid:

| Keyword    | .NET type                  | Example                          | Remarks                                    |
| ---------- | -------------------------- | -------------------------------- | ------------------------------------------ |
| bool       | System.Boolean             | **true** **false**               |                                            |
| byte       | System.Byte                | 1b                               | Unsigned octet                             |
| sbyte      | System.SByte               | 1bs                              | Signed octet                               |
| int16      | System.Int16               | 1s                               |                                            |
| uint16     | System.UInt16              | 1su                              |                                            |
| int        |                            | 1                                | Alias for `int32`                          |
| int32      | System.Int32               | 1                                |                                            |
| uint32     | System.UInt32              | 1u                               |                                            |
| int64      | System.Int64               | 1l                               |                                            |
| uint64     | System.UInt64              | 1lu                              |                                            |
| ptr        | System.IntPtr              |                                  | A native pointer as a signed integer       |
| uptr       | System.UIntPtr             |                                  |                                            |
| char       | System.Char                | 'a'                              | UTF-16 code point                          |
| float      | System.Single              | 1.1f, 1e2f, 1.1e2f, -1.1e-2.2f   |                                            |
| double     | System.Double              | 1.1, 1e2, 1.1e2, -1.1e-2.2       |                                            |
| decimal    | System.Decimal             | 1.1m                             | Binary-coded decimal                       |
| zint       | System.Numerics.BigInteger | 1z                               | Arbitrarily large integer                  |
| imag       | Apterid.Math.Imaginary     | 1i                               | Imaginary number                           |
| complex    | System.Numerics.Complex    | 1 + 2i                           | Complex number                             |
| quaternion | System.Numerics.Quaternion | 1 + 2i + 3j + 4k                 |                                            |
| ()         | System.Void                | ()                               | "unit" type                                |

## Enums

An enumeration is a set of identifiers for numerical values.  The numeric type used to store the enum corresponds to the type of the literals used to initialize the values (which type must be the same for all the initializers).

    type MyEnum =
      Nothing = 0
      Alpha   = 1
      Beta    = 2

The initializers can be numeric expressions that can use other enum values or static fields that are in scope.
Uses of enum values must be qualified, e.g. `MyEnum.Alpha`.
You can use the .NET `[Flags]` attribute on enums.

    [Flags]
    type Another = Foo = 1 << 0; Bar = 1 << 1; Baz = 1 << 2

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

      internal Property1
      	get () => field2
        set str => { field2 = str }

      private method1 = a b => a + b
      funcField : int -> double

      static Method2 c d = c * d
    }

The property setter must take arguments of all the types in the getter's signature, and return a value of the record type, which can be `this`.  In other words, values of struct types are mutable.

You can create a new record with altered values:

    foo = <MyStructType> { field1 = 1 }
    bar = foo { Property1 = "foobar"; field1 = 3 }

Omitting the implementation of both getter and setter results in an auto property.

Fields and properties may be dereferenced, but not methods.

    foo = <MyStructType>
    {
        Field1 = 2
        Method x = x + 1
    }
    bar = foo.Field1
    qux = Method foo 1 // where unambiguous
    baz = MyStructType.Method foo 1

### Classes

    type MyClassType : class BaseClass Interface1 Interface2 =
    {
      fieldA : int
      b : string

      public MyClassType = a b => { fieldA = a; this.b = b }
      public MyClassType a b c = { fieldA = a + b; this.b = c }

      virtual Method1 = x y => x + y
      override Method2 z w = base.Method2 z w
    }

Property setters must return a reference to an object of the type; it can be `this` or a new object.  In other words, objects of class types are mutable.

You can construct an object by using its constructor or a record expression:

    <MyClassType> 1 2
    <MyClassType> 3 4 5
    <MyClassType> { fieldA = 1; b = "foo" }

### Interfaces

    type IMyInterface : interface BaseInterface1, BaseInterface2 =
    {
      Property1 : string get set
    }

## Variants

    type VariantExample =
      First <string>
      Second <int, float>
      Third <MyClassType>

Variant types are implemented as struct types.

You can pattern-match as follows:

    if foo is
      First f -> ...
      Second i f -> ...
      Third o -> ...

## Functions

Function types are defined in the usual way:

    f : int -> string -> bool
    f n s = (fmt "{0} {1}" n s) == "1 qux"

If there is no type annotation for a function binding, the function can be polymorphic; you can bind the same name with different argument patterns.

## Aliases

    type MyAlias = ISomething<With,Generic,Args>

## Casting

You can perform an unchecked cast by prefixing an expression with a type in brackets:

    bar = 1.2
    foo = <int> bar

You can perform a checked cast by using a question mark:

    bar = "qux"
    foo = <int?> bar

Which returns an `Option<int>` value.

## Built-In Types

### Options

The `Option<T>` type is defined as follows:

    type Option<T> =
      Some<T>
      None

There is special syntax for declaring bindings with option types:

    foo : int?

There are various operators that return option types, including:

    foo.?Bar // if foo is Some f -> Some f.Bar; None -> None

### Lists

A list consists of a value followed by a list, which may be empty.

    l1 = 'a' :: [ 'a', 'b', 'c' ]
    l2 = 'a' :: 'b' :: 'c' :: []

The type of these values is `Apterid.List<char>` (which implements `IList<char>`).

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

A tuple is a struct with fields `Item1` to `ItemN`.  Tuple types are declared as:

    (string, int)

And values are created analogously:

    t : (int, char)
    t = (1, 'v')

This value is of type `Apterid.Tuple<int, char>`.  This is **not** the .NET `Tuple` type; in particular, it is a struct type!

Tuple items may be accessed by index as well:

    t = (2, 'd')
    t[0] == 2 // true

### Arrays

An array is a .NET array, which can be multidimensional or jagged.

    foo : int||
    bar : char|,|
    baz : double|| ||

    foo = | 1, 2, 3 |
    bar = | 'a', 'b' |
          | 'c', 'd' |
    baz = | | 1.1, 2.2 |, | 3.3 | |

Array elements can be accessed by index, sliced, and tested:

    foo[0]   // 1
    bar[1..] // | 'c', 'd' |
    foo?[1]  // Some 2

### Strings

A value of type `string` is an array of `char` values.

    foo : string
    foo = "something"

As an array, strings can be accessed by index and sliced.

You can use raw strings `@"foo"`, along with the usual escapes.

### Maps

Maps are of type `Apterid.Map<K,V>`, which implements `IDictionary<K,V>`.

    m : Map<int, string>
    m = { 0 = "foo", 1 = "bar", 2 = "baz" }

Map elements can be accessed by key, and tested:

    foo = m[0]    // "foo"
    bar = m?[123] // None

### Sets

Sets are of type `Apterid.Set<T>`, which implements `ISet<T>`:

    m = { 0, 1, 2 }

Set membership may be tested by treating a set as a `Map<T,bool>` with an infinite domain:

    foo = m[1]   // true
    bar = m[123] // false

## Modules

Modules are objects that can contain types, values and functions.

    module Something =
      Field1 = "foo"
      private field2 = 0
      Property1 : get = 1

These are public by default, but you can declare them private.
References to public members of a module must be qualified, unless their names are imported into the module via the `using` statement.

The module cannot contain bare expressions; a function of the same name as the module with one `()` parameter will be evaluated when the module's assembly is loaded.

### Namespaces

Top-level module declarations can have namespaces:

    module Namespace.Module =
      foo = 0

      module Submodule = // cannot have a namespace
        bar = 1

There is no `namespace` keyword.

### Imports

You can import names into a module with `using` statements that reference other modules (or .NET namespaces).  The module or namespace must be found in the containing project's references.

    using System.Collections.Generic

Using statements must appear before any bindings in the module.

# Bindings

A binding assigns a name to a particular value in a particular scope.  The scope of a binding starts with its declaration and extends to the end of the enclosing scope.

    foo = 0
    bar = foo + 1
    baz a = a * 3

In the body of a function, bindings must appear before the expression that is returned as the result of the function.

## Type Annotations

A binding may be immediately preceded by an annotation that defines the type of the binding.

    foo : int
    foo = 1

    bar : int -> string
    bar n = fmt "{0}" n

# Pattern Matching

Pattern matching occurs in the following contexts:

## Bindings

You can use simple structural matching for bindings:

    (a, b) = (1, 2)
    { x = X } = <Point> { X = 1; Y = 2 }

You can use the `_` identifier to match anything:

    f (a, _ ) = a + 1

## Function Arguments

You can use structural matching in function arguments, as well as type constraints:

    f [a, b] = a * b
    f c : int = c + 2
    f (d:int, e:string) = d - 1

## `if` Expressions

You can use pattern matching in `if` expressions:

    if bar.Get() is
      [a, b] -> a * b
      [a]    -> a
      []     -> 0

# Functions

Functions can be named or unnamed.

    f x = x + 1          // a -> int
    g h x = h x          // (a -> b) -> c
    j = g f 1            // 2
    k = g (x => x + 2) 1 // 3

There may be more than one binding to a function in a given scope; the first version of the function whose parameter pattern matches its inputs will be used when called.

    f [x, y] = x * y
    f [] = 0  

## Calling .NET Methods

You can call (and curry) methods from .NET libraries written in other languages as you would any other methods.

Methods that return class references (or `Nullable<T>`) will be treated as returning `Option<T>`.

Methods that return `()` (void) need to be called in a `do` expression.

Methods with `ref` or `out` parameters should be thought of as follows:

    string Method(int n, ref double m, out char c);

becomes

    Method : int -> double -> (string?, double?, char?)

where the result type is a tuple of the method's return type and any `ref` or `out` parameters' types.  `ref` parameters are included in the function's arguments, but not `out` parameters.


# Operators

Operators are static methods of some type or module, as usual (you do not use the `static` keyword with `operator X` declarations).  You can use the usual C# operator names, e.g.:

    type Point =
    {
        X : int
        Y : int

        operator + : Point -> Point -> Point
        operator + a b = Point { X = a.X + b.X; Y = a.Y + b.Y }

        static op_Multiply a b = Point { X = a.X * b.X; Y = a.Y * b.Y }
    }

You can create arbitrary prefix, infix, and postfix operators:

    type Vector =
    {
        X : float
        Y : float

        [Operator(Precedence.Multiplicative, Associative.Left)]
        cross : Vector -> Vector -> Vector
    }

These operators may be used anywhere the enclosing type's module is in scope; they may also be used qualified.

# Inference and Parameterized Types

Type inference does not extend beyond the scope of a function.  If there are unresolved types anywhere in a function, it will be implemented as a generic function.

    foo a b = a + b  // T3 foo<T1, T2, T3>(T1 a, T2 b) { a + b }

## Generics



## Constraints

# Standard Library

## Collection Methods

## Units of Measure
