//
// IronMeta Parser Parser; Generated 2015-04-06 23:12:44Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace Apterid.Bootstrap.Compiler
{

    using _Parser_Inputs = IEnumerable<char>;
    using _Parser_Results = IEnumerable<int>;
    using _Parser_Item = IronMeta.Matcher.MatchItem<char, int>;
    using _Parser_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, int>>;
    using _Parser_Memo = Memo<char, int>;
    using _Parser_Rule = System.Action<Memo<char, int>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, int>>>;
    using _Parser_Base = IronMeta.Matcher.Matcher<char, int>;

    public partial class Parser : IronMeta.Matcher.CharMatcher<int>
    {
        public Parser()
            : base()
        {
            _setTerminals();
        }

        public Parser(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "BindingAnnotation",
                "BindingDefinition",
                "Comment",
                "EOF",
                "EOL",
                "FunctionDeclaration",
                "ModuleDeclaration",
                "NamespaceDeclaration",
                "Spaces",
                "TypeDeclaration",
                "UsingStatement",
                "Whitespace",
            };
        }


        public void SourceSpan(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // PLUS 0
            int _start_i0 = _index;
            var _res0 = Enumerable.Empty<int>();
        label0:

            // CALLORVAR SourceElement
            _Parser_Item _r1;

            _r1 = _MemoCall(_memo, "SourceElement", _index, SourceElement, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // PLUS 0
            var _r0 = _memo.Results.Pop();
            if (_r0 != null)
            {
                _res0 = _res0.Concat(_r0.Results);
                goto label0;
            }
            else
            {
                if (_index > _start_i0)
                    _memo.Results.Push(new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _res0.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

        }


        public void SourceElement(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // OR 3
            int _start_i3 = _index;

            // OR 4
            int _start_i4 = _index;

            // OR 5
            int _start_i5 = _index;

            // OR 6
            int _start_i6 = _index;

            // CALLORVAR Whitespace
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "Whitespace", _index, Whitespace, null);

            if (_r7 != null) _index = _r7.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i6; } else goto label6;

            // CALLORVAR UsingStatement
            _Parser_Item _r8;

            _r8 = _MemoCall(_memo, "UsingStatement", _index, UsingStatement, null);

            if (_r8 != null) _index = _r8.NextIndex;

        label6: // OR
            int _dummy_i6 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i5; } else goto label5;

            // CALLORVAR NamespaceDeclaration
            _Parser_Item _r9;

            _r9 = _MemoCall(_memo, "NamespaceDeclaration", _index, NamespaceDeclaration, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label5: // OR
            int _dummy_i5 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i4; } else goto label4;

            // CALLORVAR ModuleDeclaration
            _Parser_Item _r10;

            _r10 = _MemoCall(_memo, "ModuleDeclaration", _index, ModuleDeclaration, null);

            if (_r10 != null) _index = _r10.NextIndex;

        label4: // OR
            int _dummy_i4 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i3; } else goto label3;

            // CALLORVAR TypeDeclaration
            _Parser_Item _r11;

            _r11 = _MemoCall(_memo, "TypeDeclaration", _index, TypeDeclaration, null);

            if (_r11 != null) _index = _r11.NextIndex;

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // CALLORVAR FunctionDeclaration
            _Parser_Item _r12;

            _r12 = _MemoCall(_memo, "FunctionDeclaration", _index, FunctionDeclaration, null);

            if (_r12 != null) _index = _r12.NextIndex;

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR BindingDeclaration
            _Parser_Item _r13;

            _r13 = _MemoCall(_memo, "BindingDeclaration", _index, BindingDeclaration, null);

            if (_r13 != null) _index = _r13.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR BindingDefinition
            _Parser_Item _r14;

            _r14 = _MemoCall(_memo, "BindingDefinition", _index, BindingDefinition, null);

            if (_r14 != null) _index = _r14.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void UsingStatement(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "not implemented");

        }


        public void NamespaceDeclaration(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "not implemented");

        }


        public void ModuleDeclaration(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "not implemented");

        }


        public void TypeDeclaration(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "not implemented");

        }


        public void FunctionDeclaration(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "not implemented");

        }


        public void BindingAnnotation(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "not implemented");

        }


        public void BindingDefinition(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "not implemented");

        }


        public void Whitespace(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // PLUS 0
            int _start_i0 = _index;
            var _res0 = Enumerable.Empty<int>();
        label0:

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR Comment
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "Comment", _index, Comment, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR Spaces
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "Spaces", _index, Spaces, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // PLUS 0
            var _r0 = _memo.Results.Pop();
            if (_r0 != null)
            {
                _res0 = _res0.Concat(_r0.Results);
                goto label0;
            }
            else
            {
                if (_index > _start_i0)
                    _memo.Results.Push(new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _res0.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

        }


        public void Comment(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL "//"
            _ParseLiteralString(_memo, ref _index, "//");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // STAR 3
            int _start_i3 = _index;
            var _res3 = Enumerable.Empty<int>();
        label3:

            // AND 4
            int _start_i4 = _index;

            // NOT 5
            int _start_i5 = _index;

            // CALLORVAR EOL
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // NOT 5
            var _r5 = _memo.Results.Pop();
            _memo.Results.Push( _r5 == null ? new _Parser_Item(_start_i5, _memo.InputEnumerable) : null);
            _index = _start_i5;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // ANY
            _ParseAny(_memo, ref _index);

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // STAR 3
            var _r3 = _memo.Results.Pop();
            if (_r3 != null)
            {
                _res3 = _res3.Concat(_r3.Results);
                goto label3;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _res3.Where(_NON_NULL), true));
            }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR EOL
            _Parser_Item _r8;

            _r8 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r8 != null) _index = _r8.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Spaces(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item c = null;

            // COND 0
            int _start_i0 = _index;

            // ANY
            _ParseAny(_memo, ref _index);

            // BIND c
            c = _memo.Results.Peek();

            // COND
            if (_memo.Results.Peek() == null || !(Char.IsWhiteSpace(c))) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i0; }

        }


        public void EOL(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // LITERAL '\r'
            _ParseLiteralChar(_memo, ref _index, '\r');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL '\n'
            _ParseLiteralChar(_memo, ref _index, '\n');

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL '\n'
            _ParseLiteralChar(_memo, ref _index, '\n');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL '\r'
            _ParseLiteralChar(_memo, ref _index, '\r');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void EOF(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // NOT 0
            int _start_i0 = _index;

            // ANY
            _ParseAny(_memo, ref _index);

            // NOT 0
            var _r0 = _memo.Results.Pop();
            _memo.Results.Push( _r0 == null ? new _Parser_Item(_start_i0, _memo.InputEnumerable) : null);
            _index = _start_i0;

        }

    } // class Parser

} // namespace Apterid.Bootstrap.Compiler

