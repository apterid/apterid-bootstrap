//
// IronMeta ApteridParser Parser; Generated 2015-09-13 05:06:37Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;
using System.Numerics;

#pragma warning disable 0219
#pragma warning disable 1591

namespace Apterid.Bootstrap.Parse
{

    using _ApteridParser_Inputs = IEnumerable<char>;
    using _ApteridParser_Results = IEnumerable<Syntax.Node>;
    using _ApteridParser_Item = IronMeta.Matcher.MatchItem<char, Syntax.Node>;
    using _ApteridParser_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, Syntax.Node>>;
    using _ApteridParser_Memo = IronMeta.Matcher.MatchState<char, Syntax.Node>;
    using _ApteridParser_Rule = System.Action<IronMeta.Matcher.MatchState<char, Syntax.Node>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, Syntax.Node>>>;
    using _ApteridParser_Base = IronMeta.Matcher.Matcher<char, Syntax.Node>;

    public partial class ApteridParser : IronMeta.Matcher.Matcher<char, Syntax.Node>
    {
        public ApteridParser()
            : base()
        {
            _setTerminals();
        }

        public ApteridParser(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "Binding",
                "DecimalInteger",
                "Directive",
                "DirectiveExpression",
                "DOT",
                "DoubleReturn",
                "EOF",
                "EOL",
                "EQ",
                "ErrorSection",
                "Expression",
                "Identifier",
                "InlineComment",
                "IntegerLiteral",
                "LineComment",
                "Literal",
                "ModuleBody",
                "ModuleItem",
                "ModulePart",
                "QualifiedIdentifier",
                "SC",
                "SE",
                "WS",
            };
        }


        public void ApteridSource(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // AND 1
            int _start_i1 = _index;

            // STAR 2
            int _start_i2 = _index;
            var _res2 = Enumerable.Empty<Syntax.Node>();
        label2:

            // CALLORVAR SourcePart
            _ApteridParser_Item _r3;

            _r3 = _MemoCall(_memo, "SourcePart", _index, SourcePart, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // STAR 2
            var _r2 = _memo.Results.Pop();
            if (_r2 != null)
            {
                _res2 = _res2.Concat(_r2.Results);
                goto label2;
            }
            else
            {
                _memo.Results.Push(new _ApteridParser_Item(_start_i2, _index, _memo.InputEnumerable, _res2.Where(_NON_NULL), true));
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR EOF
            _ApteridParser_Item _r4;

            _r4 = _MemoCall(_memo, "EOF", _index, EOF, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Source>(_IM_Result); }, _r0), true) );
            }

        }


        public void SourcePart(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // CALLORVAR Directive
            _ApteridParser_Item _r3;

            _r3 = _MemoCall(_memo, "Directive", _index, Directive, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // CALLORVAR TopLevelModule
            _ApteridParser_Item _r4;

            _r4 = _MemoCall(_memo, "TopLevelModule", _index, TopLevelModule, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // PLUS 5
            int _start_i5 = _index;
            var _res5 = Enumerable.Empty<Syntax.Node>();
        label5:

            // CALLORVAR SE
            _ApteridParser_Item _r6;

            _r6 = _MemoCall(_memo, "SE", _index, SE, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // PLUS 5
            var _r5 = _memo.Results.Pop();
            if (_r5 != null)
            {
                _res5 = _res5.Concat(_r5.Results);
                goto label5;
            }
            else
            {
                if (_index > _start_i5)
                    _memo.Results.Push(new _ApteridParser_Item(_start_i5, _index, _memo.InputEnumerable, _res5.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // PLUS 7
            int _start_i7 = _index;
            var _res7 = Enumerable.Empty<Syntax.Node>();
        label7:

            // CALLORVAR ErrorSection
            _ApteridParser_Item _r8;

            _r8 = _MemoCall(_memo, "ErrorSection", _index, ErrorSection, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // PLUS 7
            var _r7 = _memo.Results.Pop();
            if (_r7 != null)
            {
                _res7 = _res7.Concat(_r7.Results);
                goto label7;
            }
            else
            {
                if (_index > _start_i7)
                    _memo.Results.Push(new _ApteridParser_Item(_start_i7, _index, _memo.InputEnumerable, _res7.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Directive(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL '#'
            _ParseLiteralChar(_memo, ref _index, '#');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR WS
            _ApteridParser_Item _r4;

            _r4 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR DirectiveExpression
            _ApteridParser_Item _r5;

            _r5 = _MemoCall(_memo, "DirectiveExpression", _index, DirectiveExpression, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void DirectiveExpression(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // LITERAL "if"
            _ParseLiteralString(_memo, ref _index, "if");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR SC
            _ApteridParser_Item _r6;

            _r6 = _MemoCall(_memo, "SC", _index, SC, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LITERAL '!'
            _ParseLiteralChar(_memo, ref _index, '!');

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR SC
            _ApteridParser_Item _r9;

            _r9 = _MemoCall(_memo, "SC", _index, SC, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR Identifier
            _ApteridParser_Item _r10;

            _r10 = _MemoCall(_memo, "Identifier", _index, Identifier, null);

            if (_r10 != null) _index = _r10.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL "endif"
            _ParseLiteralString(_memo, ref _index, "endif");

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void TopLevelModule(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            _ApteridParser_Item indent = null;
            _ApteridParser_Item name = null;
            _ApteridParser_Item body = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // AND 7
            int _start_i7 = _index;

            // CALLORVAR SC
            _ApteridParser_Item _r10;

            _r10 = _MemoCall(_memo, "SC", _index, SC, null);

            if (_r10 != null) _index = _r10.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

            // BIND indent
            indent = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label7; }

            // CALL Keyword
            var _start_i11 = _index;
            _ApteridParser_Item _r11;
            var _arg11_0 = "module";

            _r11 = _MemoCall(_memo, "Keyword", _index, Keyword, new _ApteridParser_Item[] { new _ApteridParser_Item(_arg11_0) });

            if (_r11 != null) _index = _r11.NextIndex;

        label7: // AND
            var _r7_2 = _memo.Results.Pop();
            var _r7_1 = _memo.Results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i7, _index, _memo.InputEnumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i7;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // CALLORVAR SC
            _ApteridParser_Item _r12;

            _r12 = _MemoCall(_memo, "SC", _index, SC, null);

            if (_r12 != null) _index = _r12.NextIndex;

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // CALLORVAR QualifiedIdentifier
            _ApteridParser_Item _r14;

            _r14 = _MemoCall(_memo, "QualifiedIdentifier", _index, QualifiedIdentifier, null);

            if (_r14 != null) _index = _r14.NextIndex;

            // BIND name
            name = _memo.Results.Peek();

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR SC
            _ApteridParser_Item _r16;

            _r16 = _MemoCall(_memo, "SC", _index, SC, null);

            if (_r16 != null) _index = _r16.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALLORVAR EQ
            _ApteridParser_Item _r17;

            _r17 = _MemoCall(_memo, "EQ", _index, EQ, null);

            if (_r17 != null) _index = _r17.NextIndex;

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR SE
            _ApteridParser_Item _r18;

            _r18 = _MemoCall(_memo, "SE", _index, SE, null);

            if (_r18 != null) _index = _r18.NextIndex;

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALL ModuleBody
            var _start_i20 = _index;
            _ApteridParser_Item _r20;

            _r20 = _MemoCall(_memo, "ModuleBody", _index, ModuleBody, new _ApteridParser_Item[] { indent });

            if (_r20 != null) _index = _r20.NextIndex;

            // BIND body
            body = _memo.Results.Peek();

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Module>(_IM_Result, new { name = name.Results.SingleOrDefault(), body = body.Results }); }, _r0), true) );
            }

        }


        public void ModuleBody(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridParser_Item outer = null;
            _ApteridParser_Item inner = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND outer
            outer = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // STAR 5
            int _start_i5 = _index;
            var _res5 = Enumerable.Empty<Syntax.Node>();
        label5:

            // OR 6
            int _start_i6 = _index;

            // COND 7
            int _start_i7 = _index;

            // AND 8
            int _start_i8 = _index;

            // CALLORVAR SC
            _ApteridParser_Item _r10;

            _r10 = _MemoCall(_memo, "SC", _index, SC, null);

            if (_r10 != null) _index = _r10.NextIndex;

            // BIND inner
            inner = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label8; }

            // CALLORVAR SE
            _ApteridParser_Item _r11;

            _r11 = _MemoCall(_memo, "SE", _index, SE, null);

            if (_r11 != null) _index = _r11.NextIndex;

        label8: // AND
            var _r8_2 = _memo.Results.Pop();
            var _r8_1 = _memo.Results.Pop();

            if (_r8_1 != null && _r8_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i8, _index, _memo.InputEnumerable, _r8_1.Results.Concat(_r8_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i8;
            }

            // COND
            if (_memo.Results.Peek() == null || !(inner.Length() > outer.Length())) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i7; }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i6; } else goto label6;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "the contents of a module must be indented -- even comments");

        label6: // OR
            int _dummy_i6 = _index; // no-op for label

            // STAR 5
            var _r5 = _memo.Results.Pop();
            if (_r5 != null)
            {
                _res5 = _res5.Concat(_r5.Results);
                goto label5;
            }
            else
            {
                _memo.Results.Push(new _ApteridParser_Item(_start_i5, _index, _memo.InputEnumerable, _res5.Where(_NON_NULL), true));
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // OR 13
            int _start_i13 = _index;

            // COND 14
            int _start_i14 = _index;

            // AND 15
            int _start_i15 = _index;

            // CALLORVAR SC
            _ApteridParser_Item _r17;

            _r17 = _MemoCall(_memo, "SC", _index, SC, null);

            if (_r17 != null) _index = _r17.NextIndex;

            // BIND inner
            inner = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label15; }

            // CALL ModuleItem
            var _start_i18 = _index;
            _ApteridParser_Item _r18;

            _r18 = _MemoCall(_memo, "ModuleItem", _index, ModuleItem, new _ApteridParser_Item[] { inner });

            if (_r18 != null) _index = _r18.NextIndex;

        label15: // AND
            var _r15_2 = _memo.Results.Pop();
            var _r15_1 = _memo.Results.Pop();

            if (_r15_1 != null && _r15_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i15, _index, _memo.InputEnumerable, _r15_1.Results.Concat(_r15_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i15;
            }

            // COND
            if (_memo.Results.Peek() == null || !(inner.Length() > outer.Length())) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i14; }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i13; } else goto label13;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "the contents of a module must be indented");

        label13: // OR
            int _dummy_i13 = _index; // no-op for label

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // STAR 20
            int _start_i20 = _index;
            var _res20 = Enumerable.Empty<Syntax.Node>();
        label20:

            // CALL ModulePart
            var _start_i21 = _index;
            _ApteridParser_Item _r21;

            _r21 = _MemoCall(_memo, "ModulePart", _index, ModulePart, new _ApteridParser_Item[] { inner });

            if (_r21 != null) _index = _r21.NextIndex;

            // STAR 20
            var _r20 = _memo.Results.Pop();
            if (_r20 != null)
            {
                _res20 = _res20.Concat(_r20.Results);
                goto label20;
            }
            else
            {
                _memo.Results.Push(new _ApteridParser_Item(_start_i20, _index, _memo.InputEnumerable, _res20.Where(_NON_NULL), true));
            }

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void ModulePart(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridParser_Item indent = null;
            _ApteridParser_Item ws = null;

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // OR 3
            int _start_i3 = _index;

            // ARGS 4
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label4;
            }

            // CALLORVAR SE
            _ApteridParser_Item _r7;

            _r7 = _MemoCall(_memo, "SE", _index, SE, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label4: // ARGS 4
            _arg_input_index = _arg_index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i3; } else goto label3;

            // ARGS 8
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label8;
            }

            // COND 11
            int _start_i11 = _index;

            // AND 12
            int _start_i12 = _index;

            // AND 13
            int _start_i13 = _index;

            // CALLORVAR SC
            _ApteridParser_Item _r15;

            _r15 = _MemoCall(_memo, "SC", _index, SC, null);

            if (_r15 != null) _index = _r15.NextIndex;

            // BIND ws
            ws = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label13; }

            // CALL ModuleItem
            var _start_i16 = _index;
            _ApteridParser_Item _r16;

            _r16 = _MemoCall(_memo, "ModuleItem", _index, ModuleItem, new _ApteridParser_Item[] { indent });

            if (_r16 != null) _index = _r16.NextIndex;

        label13: // AND
            var _r13_2 = _memo.Results.Pop();
            var _r13_1 = _memo.Results.Pop();

            if (_r13_1 != null && _r13_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i13, _index, _memo.InputEnumerable, _r13_1.Results.Concat(_r13_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i13;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label12; }

            // CALLORVAR SE
            _ApteridParser_Item _r17;

            _r17 = _MemoCall(_memo, "SE", _index, SE, null);

            if (_r17 != null) _index = _r17.NextIndex;

        label12: // AND
            var _r12_2 = _memo.Results.Pop();
            var _r12_1 = _memo.Results.Pop();

            if (_r12_1 != null && _r12_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i12, _index, _memo.InputEnumerable, _r12_1.Results.Concat(_r12_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i12;
            }

            // COND
            if (_memo.Results.Peek() == null || !(ws.Length() == indent.Length())) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i11; }

        label8: // ARGS 8
            _arg_input_index = _arg_index; // no-op for label

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // ARGS 18
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label18;
            }

            // AND 21
            int _start_i21 = _index;

            // AND 22
            int _start_i22 = _index;

            // AND 23
            int _start_i23 = _index;

            // CALLORVAR SC
            _ApteridParser_Item _r25;

            _r25 = _MemoCall(_memo, "SC", _index, SC, null);

            if (_r25 != null) _index = _r25.NextIndex;

            // BIND ws
            ws = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label23; }

            // CALL ModuleItem
            var _start_i26 = _index;
            _ApteridParser_Item _r26;

            _r26 = _MemoCall(_memo, "ModuleItem", _index, ModuleItem, new _ApteridParser_Item[] { indent });

            if (_r26 != null) _index = _r26.NextIndex;

        label23: // AND
            var _r23_2 = _memo.Results.Pop();
            var _r23_1 = _memo.Results.Pop();

            if (_r23_1 != null && _r23_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i23, _index, _memo.InputEnumerable, _r23_1.Results.Concat(_r23_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i23;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label22; }

            // CALLORVAR SE
            _ApteridParser_Item _r27;

            _r27 = _MemoCall(_memo, "SE", _index, SE, null);

            if (_r27 != null) _index = _r27.NextIndex;

        label22: // AND
            var _r22_2 = _memo.Results.Pop();
            var _r22_1 = _memo.Results.Pop();

            if (_r22_1 != null && _r22_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i22, _index, _memo.InputEnumerable, _r22_1.Results.Concat(_r22_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i22;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label21; }

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "parts of a module must indented and aligned");

        label21: // AND
            var _r21_2 = _memo.Results.Pop();
            var _r21_1 = _memo.Results.Pop();

            if (_r21_1 != null && _r21_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i21, _index, _memo.InputEnumerable, _r21_1.Results.Concat(_r21_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i21;
            }

        label18: // ARGS 18
            _arg_input_index = _arg_index; // no-op for label

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // ARGS 29
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label29;
            }

            // AND 32
            int _start_i32 = _index;

            // CALLORVAR Directive
            _ApteridParser_Item _r33;

            _r33 = _MemoCall(_memo, "Directive", _index, Directive, null);

            if (_r33 != null) _index = _r33.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label32; }

            // CALLORVAR SE
            _ApteridParser_Item _r34;

            _r34 = _MemoCall(_memo, "SE", _index, SE, null);

            if (_r34 != null) _index = _r34.NextIndex;

        label32: // AND
            var _r32_2 = _memo.Results.Pop();
            var _r32_1 = _memo.Results.Pop();

            if (_r32_1 != null && _r32_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i32, _index, _memo.InputEnumerable, _r32_1.Results.Concat(_r32_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i32;
            }

        label29: // ARGS 29
            _arg_input_index = _arg_index; // no-op for label

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // ARGS 35
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label35;
            }

            // CALLORVAR ErrorSection
            _ApteridParser_Item _r38;

            _r38 = _MemoCall(_memo, "ErrorSection", _index, ErrorSection, null);

            if (_r38 != null) _index = _r38.NextIndex;

        label35: // ARGS 35
            _arg_input_index = _arg_index; // no-op for label

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void ModuleItem(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridParser_Item indent = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // CALL Binding
            var _start_i3 = _index;
            _ApteridParser_Item _r3;

            _r3 = _MemoCall(_memo, "Binding", _index, Binding, new _ApteridParser_Item[] { indent });

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void Binding(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            _ApteridParser_Item name = null;
            _ApteridParser_Item body = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // AND 5
            int _start_i5 = _index;

            // CALLORVAR Identifier
            _ApteridParser_Item _r7;

            _r7 = _MemoCall(_memo, "Identifier", _index, Identifier, null);

            if (_r7 != null) _index = _r7.NextIndex;

            // BIND name
            name = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // CALLORVAR WS
            _ApteridParser_Item _r9;

            _r9 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r9 != null) _index = _r9.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR EQ
            _ApteridParser_Item _r10;

            _r10 = _MemoCall(_memo, "EQ", _index, EQ, null);

            if (_r10 != null) _index = _r10.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALLORVAR WS
            _ApteridParser_Item _r12;

            _r12 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR Expression
            _ApteridParser_Item _r14;

            _r14 = _MemoCall(_memo, "Expression", _index, Expression, null);

            if (_r14 != null) _index = _r14.NextIndex;

            // BIND body
            body = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR SE
            _ApteridParser_Item _r15;

            _r15 = _MemoCall(_memo, "SE", _index, SE, null);

            if (_r15 != null) _index = _r15.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Binding>(_IM_Result, new { name = name.Results.SingleOrDefault(), pattern = (Syntax.Pattern)null, body = body.Results }); }, _r0), true) );
            }

        }


        public void Expression(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // CALLORVAR Literal
            _ApteridParser_Item _r0;

            _r0 = _MemoCall(_memo, "Literal", _index, Literal, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void Literal(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // CALLORVAR IntegerLiteral
            _ApteridParser_Item _r0;

            _r0 = _MemoCall(_memo, "IntegerLiteral", _index, IntegerLiteral, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void IntegerLiteral(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // CALLORVAR DecimalInteger
            _ApteridParser_Item _r0;

            _r0 = _MemoCall(_memo, "DecimalInteger", _index, DecimalInteger, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void DecimalInteger(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // REGEXP [\+-]?[0-9]+(_?[0-9]+)*
            _ParseRegexp(_memo, ref _index, _re0);

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { var str = new string(_IM_Result.Inputs.ToArray());
            var value = BigInteger.Parse(str.Replace("_", ""));
            return Make<Syntax.Literal<BigInteger>>(_IM_Result, new { value = value }); }, _r0), true) );
            }

        }


        public void Keyword(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridParser_Item text = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND text
            text = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // CALLORVAR text
            _ApteridParser_Item _r4;

            if (text.Production != null)
            {
                var _p4 = (System.Action<_ApteridParser_Memo, int, IEnumerable<_ApteridParser_Item>>)(object)text.Production; // what type safety?
                _r4 = _MemoCall(_memo, text.Production.Method.Name, _index, _p4, null);
            }
            else
            {
                _r4 = _ParseLiteralObj(_memo, ref _index, text.Inputs);
            }

            if (_r4 != null) _index = _r4.NextIndex;

            // ACT
            var _r3 = _memo.Results.Peek();
            if (_r3 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r3.StartIndex, _r3.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Keyword>(_IM_Result); }, _r3), true) );
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void Identifier(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // REGEXP _|_[_0-9a-zA-Z]+|[a-zA-Z][_0-9a-zA-Z]*
            _ParseRegexp(_memo, ref _index, _re1);

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Identifier>(_IM_Result); }, _r0), true) );
            }

        }


        public void QualifiedIdentifier(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            _ApteridParser_Item i = null;

            // AND 1
            int _start_i1 = _index;

            // STAR 2
            int _start_i2 = _index;
            var _res2 = Enumerable.Empty<Syntax.Node>();
        label2:

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR Identifier
            _ApteridParser_Item _r4;

            _r4 = _MemoCall(_memo, "Identifier", _index, Identifier, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALLORVAR DOT
            _ApteridParser_Item _r5;

            _r5 = _MemoCall(_memo, "DOT", _index, DOT, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // STAR 2
            var _r2 = _memo.Results.Pop();
            if (_r2 != null)
            {
                _res2 = _res2.Concat(_r2.Results);
                goto label2;
            }
            else
            {
                _memo.Results.Push(new _ApteridParser_Item(_start_i2, _index, _memo.InputEnumerable, _res2.Where(_NON_NULL), true));
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR Identifier
            _ApteridParser_Item _r7;

            _r7 = _MemoCall(_memo, "Identifier", _index, Identifier, null);

            if (_r7 != null) _index = _r7.NextIndex;

            // BIND i
            i = _memo.Results.Peek();

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.QualifiedIdentifier>(_IM_Result); }, _r0), true) );
            }

        }


        public void DOT(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // LITERAL '.'
            _ParseLiteralChar(_memo, ref _index, '.');

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Punct>(_IM_Result); }, _r0), true) );
            }

        }


        public void EQ(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // LITERAL '='
            _ParseLiteralChar(_memo, ref _index, '=');

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Punct>(_IM_Result); }, _r0), true) );
            }

        }


        public void SC(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // PLUS 0
            int _start_i0 = _index;
            var _res0 = Enumerable.Empty<Syntax.Node>();
        label0:

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR WS
            _ApteridParser_Item _r2;

            _r2 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR InlineComment
            _ApteridParser_Item _r3;

            _r3 = _MemoCall(_memo, "InlineComment", _index, InlineComment, null);

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
                    _memo.Results.Push(new _ApteridParser_Item(_start_i0, _index, _memo.InputEnumerable, _res0.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

        }


        public void SE(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // STAR 1
            int _start_i1 = _index;
            var _res1 = Enumerable.Empty<Syntax.Node>();
        label1:

            // OR 2
            int _start_i2 = _index;

            // CALLORVAR InlineComment
            _ApteridParser_Item _r3;

            _r3 = _MemoCall(_memo, "InlineComment", _index, InlineComment, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // CALLORVAR WS
            _ApteridParser_Item _r4;

            _r4 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // STAR 1
            var _r1 = _memo.Results.Pop();
            if (_r1 != null)
            {
                _res1 = _res1.Concat(_r1.Results);
                goto label1;
            }
            else
            {
                _memo.Results.Push(new _ApteridParser_Item(_start_i1, _index, _memo.InputEnumerable, _res1.Where(_NON_NULL), true));
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // OR 5
            int _start_i5 = _index;

            // CALLORVAR LineComment
            _ApteridParser_Item _r6;

            _r6 = _MemoCall(_memo, "LineComment", _index, LineComment, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i5; } else goto label5;

            // CALLORVAR EOL
            _ApteridParser_Item _r7;

            _r7 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label5: // OR
            int _dummy_i5 = _index; // no-op for label

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void InlineComment(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // LITERAL "/*"
            _ParseLiteralString(_memo, ref _index, "/*");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // STAR 5
            int _start_i5 = _index;
            var _res5 = Enumerable.Empty<Syntax.Node>();
        label5:

            // OR 6
            int _start_i6 = _index;

            // CALLORVAR EOL
            _ApteridParser_Item _r7;

            _r7 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r7 != null) _index = _r7.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i6; } else goto label6;

            // AND 8
            int _start_i8 = _index;

            // NOT 9
            int _start_i9 = _index;

            // LITERAL "*/"
            _ParseLiteralString(_memo, ref _index, "*/");

            // NOT 9
            var _r9 = _memo.Results.Pop();
            _memo.Results.Push( _r9 == null ? new _ApteridParser_Item(_start_i9, _memo.InputEnumerable) : null);
            _index = _start_i9;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label8; }

            // ANY
            _ParseAny(_memo, ref _index);

        label8: // AND
            var _r8_2 = _memo.Results.Pop();
            var _r8_1 = _memo.Results.Pop();

            if (_r8_1 != null && _r8_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i8, _index, _memo.InputEnumerable, _r8_1.Results.Concat(_r8_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i8;
            }

        label6: // OR
            int _dummy_i6 = _index; // no-op for label

            // STAR 5
            var _r5 = _memo.Results.Pop();
            if (_r5 != null)
            {
                _res5 = _res5.Concat(_r5.Results);
                goto label5;
            }
            else
            {
                _memo.Results.Push(new _ApteridParser_Item(_start_i5, _index, _memo.InputEnumerable, _res5.Where(_NON_NULL), true));
            }

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL "*/"
            _ParseLiteralString(_memo, ref _index, "*/");

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // ACT
            var _r1 = _memo.Results.Peek();
            if (_r1 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r1.StartIndex, _r1.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.InlineComment>(_IM_Result); }, _r1), true) );
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // AND 13
            int _start_i13 = _index;

            // AND 14
            int _start_i14 = _index;

            // AND 15
            int _start_i15 = _index;

            // LITERAL "/*"
            _ParseLiteralString(_memo, ref _index, "/*");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label15; }

            // STAR 17
            int _start_i17 = _index;
            var _res17 = Enumerable.Empty<Syntax.Node>();
        label17:

            // OR 18
            int _start_i18 = _index;

            // CALLORVAR EOL
            _ApteridParser_Item _r19;

            _r19 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r19 != null) _index = _r19.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i18; } else goto label18;

            // AND 20
            int _start_i20 = _index;

            // NOT 21
            int _start_i21 = _index;

            // OR 22
            int _start_i22 = _index;

            // CALLORVAR EOF
            _ApteridParser_Item _r23;

            _r23 = _MemoCall(_memo, "EOF", _index, EOF, null);

            if (_r23 != null) _index = _r23.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i22; } else goto label22;

            // LITERAL "*/"
            _ParseLiteralString(_memo, ref _index, "*/");

        label22: // OR
            int _dummy_i22 = _index; // no-op for label

            // NOT 21
            var _r21 = _memo.Results.Pop();
            _memo.Results.Push( _r21 == null ? new _ApteridParser_Item(_start_i21, _memo.InputEnumerable) : null);
            _index = _start_i21;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label20; }

            // ANY
            _ParseAny(_memo, ref _index);

        label20: // AND
            var _r20_2 = _memo.Results.Pop();
            var _r20_1 = _memo.Results.Pop();

            if (_r20_1 != null && _r20_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i20, _index, _memo.InputEnumerable, _r20_1.Results.Concat(_r20_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i20;
            }

        label18: // OR
            int _dummy_i18 = _index; // no-op for label

            // STAR 17
            var _r17 = _memo.Results.Pop();
            if (_r17 != null)
            {
                _res17 = _res17.Concat(_r17.Results);
                goto label17;
            }
            else
            {
                _memo.Results.Push(new _ApteridParser_Item(_start_i17, _index, _memo.InputEnumerable, _res17.Where(_NON_NULL), true));
            }

        label15: // AND
            var _r15_2 = _memo.Results.Pop();
            var _r15_1 = _memo.Results.Pop();

            if (_r15_1 != null && _r15_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i15, _index, _memo.InputEnumerable, _r15_1.Results.Concat(_r15_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i15;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label14; }

            // CALLORVAR EOF
            _ApteridParser_Item _r26;

            _r26 = _MemoCall(_memo, "EOF", _index, EOF, null);

            if (_r26 != null) _index = _r26.NextIndex;

        label14: // AND
            var _r14_2 = _memo.Results.Pop();
            var _r14_1 = _memo.Results.Pop();

            if (_r14_1 != null && _r14_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i14, _index, _memo.InputEnumerable, _r14_1.Results.Concat(_r14_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i14;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label13; }

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "unterminated comment");

        label13: // AND
            var _r13_2 = _memo.Results.Pop();
            var _r13_1 = _memo.Results.Pop();

            if (_r13_1 != null && _r13_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i13, _index, _memo.InputEnumerable, _r13_1.Results.Concat(_r13_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i13;
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void LineComment(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            _ApteridParser_Item ec = null;
            _ApteridParser_Item eol = null;

            // AND 1
            int _start_i1 = _index;

            // AND 3
            int _start_i3 = _index;

            // LITERAL "//"
            _ParseLiteralString(_memo, ref _index, "//");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // STAR 5
            int _start_i5 = _index;
            var _res5 = Enumerable.Empty<Syntax.Node>();
        label5:

            // AND 6
            int _start_i6 = _index;

            // NOT 7
            int _start_i7 = _index;

            // CALLORVAR EOL
            _ApteridParser_Item _r8;

            _r8 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // NOT 7
            var _r7 = _memo.Results.Pop();
            _memo.Results.Push( _r7 == null ? new _ApteridParser_Item(_start_i7, _memo.InputEnumerable) : null);
            _index = _start_i7;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // ANY
            _ParseAny(_memo, ref _index);

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // STAR 5
            var _r5 = _memo.Results.Pop();
            if (_r5 != null)
            {
                _res5 = _res5.Concat(_r5.Results);
                goto label5;
            }
            else
            {
                _memo.Results.Push(new _ApteridParser_Item(_start_i5, _index, _memo.InputEnumerable, _res5.Where(_NON_NULL), true));
            }

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // BIND ec
            ec = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // OR 11
            int _start_i11 = _index;

            // CALLORVAR EOL
            _ApteridParser_Item _r12;

            _r12 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i11; } else goto label11;

            // CALLORVAR EOF
            _ApteridParser_Item _r13;

            _r13 = _MemoCall(_memo, "EOF", _index, EOF, null);

            if (_r13 != null) _index = _r13.NextIndex;

        label11: // OR
            int _dummy_i11 = _index; // no-op for label

            // BIND eol
            eol = _memo.Results.Peek();

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new Syntax.Node[]
            {
                Make<Syntax.EndComment>(ec),
                Make<Syntax.EndOfLine>(eol),
            }; }, _r0), true) );
            }

        }


        public void WS(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // PLUS 2
            int _start_i2 = _index;
            var _res2 = Enumerable.Empty<Syntax.Node>();
        label2:

            // LITERAL ' '
            _ParseLiteralChar(_memo, ref _index, ' ');

            // PLUS 2
            var _r2 = _memo.Results.Pop();
            if (_r2 != null)
            {
                _res2 = _res2.Concat(_r2.Results);
                goto label2;
            }
            else
            {
                if (_index > _start_i2)
                    _memo.Results.Push(new _ApteridParser_Item(_start_i2, _index, _memo.InputEnumerable, _res2.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

            // ACT
            var _r1 = _memo.Results.Peek();
            if (_r1 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r1.StartIndex, _r1.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Whitespace>(_IM_Result); }, _r1), true) );
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // AND 4
            int _start_i4 = _index;

            // PLUS 5
            int _start_i5 = _index;
            var _res5 = Enumerable.Empty<Syntax.Node>();
        label5:

            // LITERAL '\t'
            _ParseLiteralChar(_memo, ref _index, '\t');

            // PLUS 5
            var _r5 = _memo.Results.Pop();
            if (_r5 != null)
            {
                _res5 = _res5.Concat(_r5.Results);
                goto label5;
            }
            else
            {
                if (_index > _start_i5)
                    _memo.Results.Push(new _ApteridParser_Item(_start_i5, _index, _memo.InputEnumerable, _res5.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "tabs are not allowed in Apterid source");

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void ErrorSection(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridParser_Item indent = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 4
            int _start_i4 = _index;

            // PLUS 5
            int _start_i5 = _index;
            var _res5 = Enumerable.Empty<Syntax.Node>();
        label5:

            // AND 6
            int _start_i6 = _index;

            // NOT 7
            int _start_i7 = _index;

            // CALLORVAR DoubleReturn
            _ApteridParser_Item _r8;

            _r8 = _MemoCall(_memo, "DoubleReturn", _index, DoubleReturn, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // NOT 7
            var _r7 = _memo.Results.Pop();
            _memo.Results.Push( _r7 == null ? new _ApteridParser_Item(_start_i7, _memo.InputEnumerable) : null);
            _index = _start_i7;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // ANY
            _ParseAny(_memo, ref _index);

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // PLUS 5
            var _r5 = _memo.Results.Pop();
            if (_r5 != null)
            {
                _res5 = _res5.Concat(_r5.Results);
                goto label5;
            }
            else
            {
                if (_index > _start_i5)
                    _memo.Results.Push(new _ApteridParser_Item(_start_i5, _index, _memo.InputEnumerable, _res5.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR DoubleReturn
            _ApteridParser_Item _r10;

            _r10 = _MemoCall(_memo, "DoubleReturn", _index, DoubleReturn, null);

            if (_r10 != null) _index = _r10.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // ACT
            var _r3 = _memo.Results.Peek();
            if (_r3 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r3.StartIndex, _r3.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.ErrorSection>(_IM_Result); }, _r3), true) );
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void DoubleReturn(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR EOL
            _ApteridParser_Item _r2;

            _r2 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR WS
            _ApteridParser_Item _r4;

            _r4 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // OR 5
            int _start_i5 = _index;

            // CALLORVAR EOL
            _ApteridParser_Item _r6;

            _r6 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i5; } else goto label5;

            // CALLORVAR EOF
            _ApteridParser_Item _r7;

            _r7 = _MemoCall(_memo, "EOF", _index, EOF, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label5: // OR
            int _dummy_i5 = _index; // no-op for label

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void EOL(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // AND 1
            int _start_i1 = _index;

            // LITERAL '\r'
            _ParseLiteralChar(_memo, ref _index, '\r');

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // LITERAL '\n'
            _ParseLiteralChar(_memo, ref _index, '\n');

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { _memo.Positions.Add(_IM_Result.NextIndex);
            return Make<Syntax.EndOfLine>(_IM_Result); }, _r0), true) );
            }

        }


        public void EOF(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            // NOT 1
            int _start_i1 = _index;

            // ANY
            _ParseAny(_memo, ref _index);

            // NOT 1
            var _r1 = _memo.Results.Pop();
            _memo.Results.Push( _r1 == null ? new _ApteridParser_Item(_start_i1, _memo.InputEnumerable) : null);
            _index = _start_i1;

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { _memo.Positions.Add(_IM_Result.NextIndex);
            return Make<Syntax.EndOfFile>(_IM_Result); }, _r0), true) );
            }

        }

        static readonly Verophyle.Regexp.StringRegexp _re0 = new Verophyle.Regexp.StringRegexp(@"[\+-]?[0-9]+(_?[0-9]+)*");
        static readonly Verophyle.Regexp.StringRegexp _re1 = new Verophyle.Regexp.StringRegexp(@"_|_[_0-9a-zA-Z]+|[a-zA-Z][_0-9a-zA-Z]*");

    } // class ApteridParser

} // namespace Apterid.Bootstrap.Parse

