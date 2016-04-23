//
// IronMeta ApteridParser Parser; Generated 2016-04-23 22:37:28Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;
using System.Numerics;

#pragma warning disable 0219
#pragma warning disable 1591

namespace Apterid.Bootstrap.Parse.Parser
{

    using _ApteridParser_Inputs = IEnumerable<char>;
    using _ApteridParser_Results = IEnumerable<Syntax.Node>;
    using _ApteridParser_Item = IronMeta.Matcher.MatchItem<char, Syntax.Node>;
    using _ApteridParser_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, Syntax.Node>>;
    using _ApteridParser_Memo = IronMeta.Matcher.MatchState<char, Syntax.Node>;
    using _ApteridParser_Rule = System.Action<IronMeta.Matcher.MatchState<char, Syntax.Node>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, Syntax.Node>>>;
    using _ApteridParser_Base = IronMeta.Matcher.Matcher<char, Syntax.Node>;

    public partial class ApteridParser : ApteridParserBase
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
            };
        }


        public void ApteridSource(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

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

            // CALLORVAR lex.EOF
            _ApteridParser_Item _r4;

            _r4 = _MemoCall(_memo, "lex.EOF", _index, lex.EOF, null);

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

            int _arg_index = 0;
            int _arg_input_index = 0;

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

            // CALLORVAR lex.SE
            _ApteridParser_Item _r6;

            _r6 = _MemoCall(_memo, "lex.SE", _index, lex.SE, null);

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

            int _arg_index = 0;
            int _arg_input_index = 0;

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL '#'
            _ParseLiteralChar(_memo, ref _index, '#');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR lex.WS
            _ApteridParser_Item _r4;

            _r4 = _MemoCall(_memo, "lex.WS", _index, lex.WS, null);

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

            int _arg_index = 0;
            int _arg_input_index = 0;

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

            // CALLORVAR lex.SC
            _ApteridParser_Item _r6;

            _r6 = _MemoCall(_memo, "lex.SC", _index, lex.SC, null);

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

            // CALLORVAR lex.SC
            _ApteridParser_Item _r9;

            _r9 = _MemoCall(_memo, "lex.SC", _index, lex.SC, null);

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

            // CALLORVAR lex.Identifier
            _ApteridParser_Item _r10;

            _r10 = _MemoCall(_memo, "lex.Identifier", _index, lex.Identifier, null);

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

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridParser_Item indent = null;
            _ApteridParser_Item vis = null;
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

            // AND 8
            int _start_i8 = _index;

            // CALLORVAR lex.SC
            _ApteridParser_Item _r11;

            _r11 = _MemoCall(_memo, "lex.SC", _index, lex.SC, null);

            if (_r11 != null) _index = _r11.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

            // BIND indent
            indent = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label8; }

            // AND 14
            int _start_i14 = _index;

            // CALL lex.Keyword
            var _start_i15 = _index;
            _ApteridParser_Item _r15;
            var _arg15_0 = "public";

            _ApteridParser_Args _actual_args15 = new _ApteridParser_Item[] { new _ApteridParser_Item(_arg15_0) };
            if (_args != null) _actual_args15 = _actual_args15.Concat(_args.Skip(_arg_index));
            _r15 = _MemoCall(_memo, "lex.Keyword", _index, lex.Keyword, _actual_args15);

            if (_r15 != null) _index = _r15.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label14; }

            // CALLORVAR lex.SC
            _ApteridParser_Item _r16;

            _r16 = _MemoCall(_memo, "lex.SC", _index, lex.SC, null);

            if (_r16 != null) _index = _r16.NextIndex;

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

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

            // BIND vis
            vis = _memo.Results.Peek();

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

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label7; }

            // CALL lex.Keyword
            var _start_i17 = _index;
            _ApteridParser_Item _r17;
            var _arg17_0 = "module";

            _ApteridParser_Args _actual_args17 = new _ApteridParser_Item[] { new _ApteridParser_Item(_arg17_0) };
            if (_args != null) _actual_args17 = _actual_args17.Concat(_args.Skip(_arg_index));
            _r17 = _MemoCall(_memo, "lex.Keyword", _index, lex.Keyword, _actual_args17);

            if (_r17 != null) _index = _r17.NextIndex;

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

            // OR 18
            int _start_i18 = _index;

            // CALLORVAR lex.SC
            _ApteridParser_Item _r19;

            _r19 = _MemoCall(_memo, "lex.SC", _index, lex.SC, null);

            if (_r19 != null) _index = _r19.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i18; } else goto label18;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "expected qualified identifier");

        label18: // OR
            int _dummy_i18 = _index; // no-op for label

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

            // CALLORVAR lex.QualifiedIdentifier
            _ApteridParser_Item _r22;

            _r22 = _MemoCall(_memo, "lex.QualifiedIdentifier", _index, lex.QualifiedIdentifier, null);

            if (_r22 != null) _index = _r22.NextIndex;

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

            // CALLORVAR lex.SC
            _ApteridParser_Item _r24;

            _r24 = _MemoCall(_memo, "lex.SC", _index, lex.SC, null);

            if (_r24 != null) _index = _r24.NextIndex;

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

            // CALLORVAR lex.EQ
            _ApteridParser_Item _r25;

            _r25 = _MemoCall(_memo, "lex.EQ", _index, lex.EQ, null);

            if (_r25 != null) _index = _r25.NextIndex;

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

            // OR 26
            int _start_i26 = _index;

            // CALLORVAR lex.SE
            _ApteridParser_Item _r27;

            _r27 = _MemoCall(_memo, "lex.SE", _index, lex.SE, null);

            if (_r27 != null) _index = _r27.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i26; } else goto label26;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "expected new line then module body");

        label26: // OR
            int _dummy_i26 = _index; // no-op for label

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
            var _start_i30 = _index;
            _ApteridParser_Item _r30;

            _ApteridParser_Args _actual_args30 = new _ApteridParser_Item[] { indent };
            if (_args != null) _actual_args30 = _actual_args30.Concat(_args.Skip(_arg_index));
            _r30 = _MemoCall(_memo, "ModuleBody", _index, ModuleBody, _actual_args30);

            if (_r30 != null) _index = _r30.NextIndex;

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
                _memo.Results.Push( new _ApteridParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Module>(_IM_Result, new 
            {
                flags = GetFlags(vis.Results),
                name = name.Results.FirstOrDefault(), 
                body = body.Results 
            }); }, _r0), true) );
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

            // LOOK 4
            int _start_i4 = _index;

            // OR 5
            int _start_i5 = _index;

            // COND 6
            int _start_i6 = _index;

            // CALLORVAR lex.SC
            _ApteridParser_Item _r8;

            _r8 = _MemoCall(_memo, "lex.SC", _index, lex.SC, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // BIND inner
            inner = _memo.Results.Peek();

            // COND
            if (_memo.Results.Peek() == null || !(inner.Length() > outer.Length())) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i6; }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i5; } else goto label5;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "the contents of a module must be indented");

        label5: // OR
            int _dummy_i5 = _index; // no-op for label

            // LOOK 4
            var _r4 = _memo.Results.Pop();
            _memo.Results.Push( _r4 != null ? new _ApteridParser_Item(_start_i4, _memo.InputEnumerable) : null );
            _index = _start_i4;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // STAR 10
            int _start_i10 = _index;
            var _res10 = Enumerable.Empty<Syntax.Node>();
        label10:

            // CALL ModulePart
            var _start_i11 = _index;
            _ApteridParser_Item _r11;

            _ApteridParser_Args _actual_args11 = new _ApteridParser_Item[] { inner };
            if (_args != null) _actual_args11 = _actual_args11.Concat(_args.Skip(_arg_index));
            _r11 = _MemoCall(_memo, "ModulePart", _index, ModulePart, _actual_args11);

            if (_r11 != null) _index = _r11.NextIndex;

            // STAR 10
            var _r10 = _memo.Results.Pop();
            if (_r10 != null)
            {
                _res10 = _res10.Concat(_r10.Results);
                goto label10;
            }
            else
            {
                _memo.Results.Push(new _ApteridParser_Item(_start_i10, _index, _memo.InputEnumerable, _res10.Where(_NON_NULL), true));
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

            // ARGS 3
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label3;
            }

            // AND 6
            int _start_i6 = _index;

            // CALLORVAR Directive
            _ApteridParser_Item _r7;

            _r7 = _MemoCall(_memo, "Directive", _index, Directive, null);

            if (_r7 != null) _index = _r7.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // CALLORVAR lex.SE
            _ApteridParser_Item _r8;

            _r8 = _MemoCall(_memo, "lex.SE", _index, lex.SE, null);

            if (_r8 != null) _index = _r8.NextIndex;

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

        label3: // ARGS 3
            _arg_input_index = _arg_index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // ARGS 9
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label9;
            }

            // CALLORVAR lex.SE
            _ApteridParser_Item _r12;

            _r12 = _MemoCall(_memo, "lex.SE", _index, lex.SE, null);

            if (_r12 != null) _index = _r12.NextIndex;

        label9: // ARGS 9
            _arg_input_index = _arg_index; // no-op for label

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // ARGS 13
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label13;
            }

            // AND 16
            int _start_i16 = _index;

            // AND 17
            int _start_i17 = _index;

            // OR 18
            int _start_i18 = _index;

            // COND 19
            int _start_i19 = _index;

            // CALLORVAR lex.SC
            _ApteridParser_Item _r21;

            _r21 = _MemoCall(_memo, "lex.SC", _index, lex.SC, null);

            if (_r21 != null) _index = _r21.NextIndex;

            // BIND ws
            ws = _memo.Results.Peek();

            // COND
            if (_memo.Results.Peek() == null || !(ws.Length() == indent.Length())) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i19; }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i18; } else goto label18;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "parts of a module must be indented and aligned");

        label18: // OR
            int _dummy_i18 = _index; // no-op for label

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label17; }

            // CALL ModuleItem
            var _start_i23 = _index;
            _ApteridParser_Item _r23;

            _ApteridParser_Args _actual_args23 = new _ApteridParser_Item[] { indent };
            if (_args != null) _actual_args23 = _actual_args23.Concat(_args.Skip(_arg_index));
            _r23 = _MemoCall(_memo, "ModuleItem", _index, ModuleItem, _actual_args23);

            if (_r23 != null) _index = _r23.NextIndex;

        label17: // AND
            var _r17_2 = _memo.Results.Pop();
            var _r17_1 = _memo.Results.Pop();

            if (_r17_1 != null && _r17_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i17, _index, _memo.InputEnumerable, _r17_1.Results.Concat(_r17_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i17;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label16; }

            // CALLORVAR lex.SE
            _ApteridParser_Item _r24;

            _r24 = _MemoCall(_memo, "lex.SE", _index, lex.SE, null);

            if (_r24 != null) _index = _r24.NextIndex;

        label16: // AND
            var _r16_2 = _memo.Results.Pop();
            var _r16_1 = _memo.Results.Pop();

            if (_r16_1 != null && _r16_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i16, _index, _memo.InputEnumerable, _r16_1.Results.Concat(_r16_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i16;
            }

        label13: // ARGS 13
            _arg_input_index = _arg_index; // no-op for label

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // ARGS 25
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label25;
            }

            // CALLORVAR ErrorSection
            _ApteridParser_Item _r28;

            _r28 = _MemoCall(_memo, "ErrorSection", _index, ErrorSection, null);

            if (_r28 != null) _index = _r28.NextIndex;

        label25: // ARGS 25
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

            // CALL ModuleBinding
            var _start_i3 = _index;
            _ApteridParser_Item _r3;

            _ApteridParser_Args _actual_args3 = new _ApteridParser_Item[] { indent };
            if (_args != null) _actual_args3 = _actual_args3.Concat(_args.Skip(_arg_index));
            _r3 = _MemoCall(_memo, "ModuleBinding", _index, ModuleBinding, _actual_args3);

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void ModuleBinding(_ApteridParser_Memo _memo, int _index, _ApteridParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridParser_Item indent = null;
            _ApteridParser_Item vis = null;
            _ApteridParser_Item name = null;
            _ApteridParser_Item body = null;

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

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // AND 9
            int _start_i9 = _index;

            // CALL lex.Keyword
            var _start_i10 = _index;
            _ApteridParser_Item _r10;
            var _arg10_0 = "public";

            _ApteridParser_Args _actual_args10 = new _ApteridParser_Item[] { new _ApteridParser_Item(_arg10_0) };
            if (_args != null) _actual_args10 = _actual_args10.Concat(_args.Skip(_arg_index));
            _r10 = _MemoCall(_memo, "lex.Keyword", _index, lex.Keyword, _actual_args10);

            if (_r10 != null) _index = _r10.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label9; }

            // CALLORVAR lex.SC
            _ApteridParser_Item _r11;

            _r11 = _MemoCall(_memo, "lex.SC", _index, lex.SC, null);

            if (_r11 != null) _index = _r11.NextIndex;

        label9: // AND
            var _r9_2 = _memo.Results.Pop();
            var _r9_1 = _memo.Results.Pop();

            if (_r9_1 != null && _r9_2 != null)
            {
                _memo.Results.Push( new _ApteridParser_Item(_start_i9, _index, _memo.InputEnumerable, _r9_1.Results.Concat(_r9_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i9;
            }

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

            // BIND vis
            vis = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // OR 12
            int _start_i12 = _index;

            // AND 13
            int _start_i13 = _index;

            // CALLORVAR lex.Identifier
            _ApteridParser_Item _r15;

            _r15 = _MemoCall(_memo, "lex.Identifier", _index, lex.Identifier, null);

            if (_r15 != null) _index = _r15.NextIndex;

            // BIND name
            name = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label13; }

            // CALLORVAR lex.WS
            _ApteridParser_Item _r17;

            _r17 = _MemoCall(_memo, "lex.WS", _index, lex.WS, null);

            if (_r17 != null) _index = _r17.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

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

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i12; } else goto label12;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "expected binding name");

        label12: // OR
            int _dummy_i12 = _index; // no-op for label

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

            // OR 19
            int _start_i19 = _index;

            // AND 20
            int _start_i20 = _index;

            // CALLORVAR lex.EQ
            _ApteridParser_Item _r21;

            _r21 = _MemoCall(_memo, "lex.EQ", _index, lex.EQ, null);

            if (_r21 != null) _index = _r21.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label20; }

            // CALLORVAR lex.WS
            _ApteridParser_Item _r23;

            _r23 = _MemoCall(_memo, "lex.WS", _index, lex.WS, null);

            if (_r23 != null) _index = _r23.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridParser_Item(_index, _memo.InputEnumerable)); }

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

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i19; } else goto label19;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "expected \"=\"");

        label19: // OR
            int _dummy_i19 = _index; // no-op for label

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

            // OR 25
            int _start_i25 = _index;

            // CALL exp.Expression
            var _start_i27 = _index;
            _ApteridParser_Item _r27;

            _ApteridParser_Args _actual_args27 = new _ApteridParser_Item[] { indent };
            if (_args != null) _actual_args27 = _actual_args27.Concat(_args.Skip(_arg_index));
            _r27 = _MemoCall(_memo, "exp.Expression", _index, exp.Expression, _actual_args27);

            if (_r27 != null) _index = _r27.NextIndex;

            // BIND body
            body = _memo.Results.Peek();

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i25; } else goto label25;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "expected expression");

        label25: // OR
            int _dummy_i25 = _index; // no-op for label

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
                _memo.Results.Push( new _ApteridParser_Item(_r3.StartIndex, _r3.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { var args = new 
            {
                flags = GetFlags(vis.Results),
                name = name.Results.FirstOrDefault(), 
                pattern = (Syntax.Pattern)null, 
                body = body.Results 
            };
            return Make<Syntax.Binding>(_IM_Result, args); }, _r3), true) );
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

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

            int _arg_index = 0;
            int _arg_input_index = 0;

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR lex.EOL
            _ApteridParser_Item _r2;

            _r2 = _MemoCall(_memo, "lex.EOL", _index, lex.EOL, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR lex.WS
            _ApteridParser_Item _r4;

            _r4 = _MemoCall(_memo, "lex.WS", _index, lex.WS, null);

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

            // CALLORVAR lex.EOL
            _ApteridParser_Item _r6;

            _r6 = _MemoCall(_memo, "lex.EOL", _index, lex.EOL, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i5; } else goto label5;

            // CALLORVAR lex.EOF
            _ApteridParser_Item _r7;

            _r7 = _MemoCall(_memo, "lex.EOF", _index, lex.EOF, null);

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


    } // class ApteridParser

} // namespace Apterid.Bootstrap.Parse.Parser

