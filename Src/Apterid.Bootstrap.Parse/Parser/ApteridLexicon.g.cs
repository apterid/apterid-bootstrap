//
// IronMeta ApteridLexicon Parser; Generated 2016-04-23 21:32:56Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace Apterid.Bootstrap.Parse.Parser
{

    using _ApteridLexicon_Inputs = IEnumerable<char>;
    using _ApteridLexicon_Results = IEnumerable<Syntax.Node>;
    using _ApteridLexicon_Item = IronMeta.Matcher.MatchItem<char, Syntax.Node>;
    using _ApteridLexicon_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, Syntax.Node>>;
    using _ApteridLexicon_Memo = IronMeta.Matcher.MatchState<char, Syntax.Node>;
    using _ApteridLexicon_Rule = System.Action<IronMeta.Matcher.MatchState<char, Syntax.Node>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, Syntax.Node>>>;
    using _ApteridLexicon_Base = IronMeta.Matcher.Matcher<char, Syntax.Node>;

    public partial class ApteridLexicon : ApteridParserBase
    {
        public ApteridLexicon()
            : base()
        {
            _setTerminals();
        }

        public ApteridLexicon(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "DOT",
                "EOF",
                "EOL",
                "EQ",
                "Identifier",
                "InlineComment",
                "IWS",
                "LineComment",
                "MKF",
                "QualifiedIdentifier",
                "SC",
                "SE",
                "UNIT",
                "WS",
            };
        }


        public void Keyword(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridLexicon_Item text = null;

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
            _ApteridLexicon_Item _r4;

            if (text.Production != null)
            {
                var _p4 = (System.Action<_ApteridLexicon_Memo, int, IEnumerable<_ApteridLexicon_Item>>)(object)text.Production;
                _r4 = _MemoCall(_memo, text.Production.Method.Name, _index, _p4, _args != null ? _args.Skip(_arg_index) : null);
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
                _memo.Results.Push( new _ApteridLexicon_Item(_r3.StartIndex, _r3.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Keyword>(_IM_Result); }, _r3), true) );
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void Identifier(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // REGEXP _|_[_0-9a-zA-Z]+|[a-zA-Z][_0-9a-zA-Z]*
            _ParseRegexp(_memo, ref _index, _re0);

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridLexicon_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Identifier>(_IM_Result); }, _r0), true) );
            }

        }


        public void QualifiedIdentifier(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridLexicon_Item q = null;
            _ApteridLexicon_Item n = null;

            // AND 1
            int _start_i1 = _index;

            // STAR 3
            int _start_i3 = _index;
            var _res3 = Enumerable.Empty<Syntax.Node>();
        label3:

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR Identifier
            _ApteridLexicon_Item _r5;

            _r5 = _MemoCall(_memo, "Identifier", _index, Identifier, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR DOT
            _ApteridLexicon_Item _r6;

            _r6 = _MemoCall(_memo, "DOT", _index, DOT, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push(new _ApteridLexicon_Item(_start_i3, _index, _memo.InputEnumerable, _res3.Where(_NON_NULL), true));
            }

            // BIND q
            q = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR Identifier
            _ApteridLexicon_Item _r8;

            _r8 = _MemoCall(_memo, "Identifier", _index, Identifier, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // BIND n
            n = _memo.Results.Peek();

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _ApteridLexicon_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.QualifiedIdentifier>(_IM_Result, new { q = q.Results, n = n.Results.FirstOrDefault() }); }, _r0), true) );
            }

        }


        public void TypeName(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridLexicon_Item indent = null;

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

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR QualifiedIdentifier
            _ApteridLexicon_Item _r4;

            _r4 = _MemoCall(_memo, "QualifiedIdentifier", _index, QualifiedIdentifier, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // AND 6
            int _start_i6 = _index;

            // AND 7
            int _start_i7 = _index;

            // AND 8
            int _start_i8 = _index;

            // AND 9
            int _start_i9 = _index;

            // AND 10
            int _start_i10 = _index;

            // LITERAL '<'
            _ParseLiteralChar(_memo, ref _index, '<');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label10; }

            // CALL IWS
            var _start_i12 = _index;
            _ApteridLexicon_Item _r12;

            _ApteridLexicon_Args _actual_args12 = new _ApteridLexicon_Item[] { indent };
            if (_args != null) _actual_args12 = _actual_args12.Concat(_args.Skip(_arg_index));
            _r12 = _MemoCall(_memo, "IWS", _index, IWS, _actual_args12);

            if (_r12 != null) _index = _r12.NextIndex;

        label10: // AND
            var _r10_2 = _memo.Results.Pop();
            var _r10_1 = _memo.Results.Pop();

            if (_r10_1 != null && _r10_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i10, _index, _memo.InputEnumerable, _r10_1.Results.Concat(_r10_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i10;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label9; }

            // CALLORVAR TypeName
            _ApteridLexicon_Item _r13;

            _r13 = _MemoCall(_memo, "TypeName", _index, TypeName, null);

            if (_r13 != null) _index = _r13.NextIndex;

        label9: // AND
            var _r9_2 = _memo.Results.Pop();
            var _r9_1 = _memo.Results.Pop();

            if (_r9_1 != null && _r9_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i9, _index, _memo.InputEnumerable, _r9_1.Results.Concat(_r9_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i9;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label8; }

            // STAR 14
            int _start_i14 = _index;
            var _res14 = Enumerable.Empty<Syntax.Node>();
        label14:

            // AND 15
            int _start_i15 = _index;

            // AND 16
            int _start_i16 = _index;

            // LITERAL ','
            _ParseLiteralChar(_memo, ref _index, ',');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label16; }

            // CALL IWS
            var _start_i18 = _index;
            _ApteridLexicon_Item _r18;

            _ApteridLexicon_Args _actual_args18 = new _ApteridLexicon_Item[] { indent };
            if (_args != null) _actual_args18 = _actual_args18.Concat(_args.Skip(_arg_index));
            _r18 = _MemoCall(_memo, "IWS", _index, IWS, _actual_args18);

            if (_r18 != null) _index = _r18.NextIndex;

        label16: // AND
            var _r16_2 = _memo.Results.Pop();
            var _r16_1 = _memo.Results.Pop();

            if (_r16_1 != null && _r16_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i16, _index, _memo.InputEnumerable, _r16_1.Results.Concat(_r16_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i16;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label15; }

            // CALLORVAR TypeName
            _ApteridLexicon_Item _r19;

            _r19 = _MemoCall(_memo, "TypeName", _index, TypeName, null);

            if (_r19 != null) _index = _r19.NextIndex;

        label15: // AND
            var _r15_2 = _memo.Results.Pop();
            var _r15_1 = _memo.Results.Pop();

            if (_r15_1 != null && _r15_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i15, _index, _memo.InputEnumerable, _r15_1.Results.Concat(_r15_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i15;
            }

            // STAR 14
            var _r14 = _memo.Results.Pop();
            if (_r14 != null)
            {
                _res14 = _res14.Concat(_r14.Results);
                goto label14;
            }
            else
            {
                _memo.Results.Push(new _ApteridLexicon_Item(_start_i14, _index, _memo.InputEnumerable, _res14.Where(_NON_NULL), true));
            }

        label8: // AND
            var _r8_2 = _memo.Results.Pop();
            var _r8_1 = _memo.Results.Pop();

            if (_r8_1 != null && _r8_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i8, _index, _memo.InputEnumerable, _r8_1.Results.Concat(_r8_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i8;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label7; }

            // CALL IWS
            var _start_i20 = _index;
            _ApteridLexicon_Item _r20;

            _ApteridLexicon_Args _actual_args20 = new _ApteridLexicon_Item[] { indent };
            if (_args != null) _actual_args20 = _actual_args20.Concat(_args.Skip(_arg_index));
            _r20 = _MemoCall(_memo, "IWS", _index, IWS, _actual_args20);

            if (_r20 != null) _index = _r20.NextIndex;

        label7: // AND
            var _r7_2 = _memo.Results.Pop();
            var _r7_1 = _memo.Results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i7, _index, _memo.InputEnumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i7;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // LITERAL '>'
            _ParseLiteralChar(_memo, ref _index, '>');

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridLexicon_Item(_index, _memo.InputEnumerable)); }

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void UNIT(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL "()"
            _ParseLiteralString(_memo, ref _index, "()");

        }


        public void DOT(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL '.'
            _ParseLiteralChar(_memo, ref _index, '.');

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridLexicon_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Punct>(_IM_Result); }, _r0), true) );
            }

        }


        public void EQ(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL '='
            _ParseLiteralChar(_memo, ref _index, '=');

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridLexicon_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Punct>(_IM_Result); }, _r0), true) );
            }

        }


        public void MKF(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL "=>"
            _ParseLiteralString(_memo, ref _index, "=>");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridLexicon_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Punct>(_IM_Result); }, _r0), true) );
            }

        }


        public void SC(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // PLUS 0
            int _start_i0 = _index;
            var _res0 = Enumerable.Empty<Syntax.Node>();
        label0:

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR WS
            _ApteridLexicon_Item _r2;

            _r2 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR InlineComment
            _ApteridLexicon_Item _r3;

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
                    _memo.Results.Push(new _ApteridLexicon_Item(_start_i0, _index, _memo.InputEnumerable, _res0.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

        }


        public void SE(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // AND 0
            int _start_i0 = _index;

            // STAR 1
            int _start_i1 = _index;
            var _res1 = Enumerable.Empty<Syntax.Node>();
        label1:

            // OR 2
            int _start_i2 = _index;

            // CALLORVAR InlineComment
            _ApteridLexicon_Item _r3;

            _r3 = _MemoCall(_memo, "InlineComment", _index, InlineComment, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // CALLORVAR WS
            _ApteridLexicon_Item _r4;

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
                _memo.Results.Push(new _ApteridLexicon_Item(_start_i1, _index, _memo.InputEnumerable, _res1.Where(_NON_NULL), true));
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // OR 5
            int _start_i5 = _index;

            // CALLORVAR LineComment
            _ApteridLexicon_Item _r6;

            _r6 = _MemoCall(_memo, "LineComment", _index, LineComment, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i5; } else goto label5;

            // CALLORVAR EOL
            _ApteridLexicon_Item _r7;

            _r7 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label5: // OR
            int _dummy_i5 = _index; // no-op for label

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void InlineComment(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

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
            _ApteridLexicon_Item _r7;

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
            _memo.Results.Push( _r9 == null ? new _ApteridLexicon_Item(_start_i9, _memo.InputEnumerable) : null);
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
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i8, _index, _memo.InputEnumerable, _r8_1.Results.Concat(_r8_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push(new _ApteridLexicon_Item(_start_i5, _index, _memo.InputEnumerable, _res5.Where(_NON_NULL), true));
            }

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _ApteridLexicon_Item(_r1.StartIndex, _r1.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.InlineComment>(_IM_Result); }, _r1), true) );
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
            _ApteridLexicon_Item _r19;

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
            _ApteridLexicon_Item _r23;

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
            _memo.Results.Push( _r21 == null ? new _ApteridLexicon_Item(_start_i21, _memo.InputEnumerable) : null);
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
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i20, _index, _memo.InputEnumerable, _r20_1.Results.Concat(_r20_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push(new _ApteridLexicon_Item(_start_i17, _index, _memo.InputEnumerable, _res17.Where(_NON_NULL), true));
            }

        label15: // AND
            var _r15_2 = _memo.Results.Pop();
            var _r15_1 = _memo.Results.Pop();

            if (_r15_1 != null && _r15_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i15, _index, _memo.InputEnumerable, _r15_1.Results.Concat(_r15_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i15;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label14; }

            // CALLORVAR EOF
            _ApteridLexicon_Item _r26;

            _r26 = _MemoCall(_memo, "EOF", _index, EOF, null);

            if (_r26 != null) _index = _r26.NextIndex;

        label14: // AND
            var _r14_2 = _memo.Results.Pop();
            var _r14_1 = _memo.Results.Pop();

            if (_r14_1 != null && _r14_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i14, _index, _memo.InputEnumerable, _r14_1.Results.Concat(_r14_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i13, _index, _memo.InputEnumerable, _r13_1.Results.Concat(_r13_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i13;
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void LineComment(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridLexicon_Item ec = null;
            _ApteridLexicon_Item eol = null;

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
            _ApteridLexicon_Item _r8;

            _r8 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // NOT 7
            var _r7 = _memo.Results.Pop();
            _memo.Results.Push( _r7 == null ? new _ApteridLexicon_Item(_start_i7, _memo.InputEnumerable) : null);
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
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push(new _ApteridLexicon_Item(_start_i5, _index, _memo.InputEnumerable, _res5.Where(_NON_NULL), true));
            }

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
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
            _ApteridLexicon_Item _r12;

            _r12 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i11; } else goto label11;

            // CALLORVAR EOF
            _ApteridLexicon_Item _r13;

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
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _ApteridLexicon_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new Syntax.Node[]
            {
                Make<Syntax.EndComment>(ec),
                Make<Syntax.EndOfLine>(eol),
            }; }, _r0), true) );
            }

        }


        public void IWS(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridLexicon_Item indent = null;
            _ApteridLexicon_Item s = null;

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // ARGS 2
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label2;
            }

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // CALLORVAR WS
            _ApteridLexicon_Item _r8;

            _r8 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridLexicon_Item(_index, _memo.InputEnumerable)); }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // CALLORVAR EOL
            _ApteridLexicon_Item _r9;

            _r9 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // COND 10
            int _start_i10 = _index;

            // CALLORVAR WS
            _ApteridLexicon_Item _r13;

            _r13 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r13 != null) _index = _r13.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridLexicon_Item(_index, _memo.InputEnumerable)); }

            // BIND s
            s = _memo.Results.Peek();

            // COND
            if (_memo.Results.Peek() == null || !(s.Length() > indent.Length())) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i10; }

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

        label2: // ARGS 2
            _arg_input_index = _arg_index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // ARGS 14
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label14;
            }

            // AND 17
            int _start_i17 = _index;

            // AND 18
            int _start_i18 = _index;

            // AND 19
            int _start_i19 = _index;

            // CALLORVAR WS
            _ApteridLexicon_Item _r21;

            _r21 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r21 != null) _index = _r21.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridLexicon_Item(_index, _memo.InputEnumerable)); }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label19; }

            // CALLORVAR EOL
            _ApteridLexicon_Item _r22;

            _r22 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r22 != null) _index = _r22.NextIndex;

        label19: // AND
            var _r19_2 = _memo.Results.Pop();
            var _r19_1 = _memo.Results.Pop();

            if (_r19_1 != null && _r19_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i19, _index, _memo.InputEnumerable, _r19_1.Results.Concat(_r19_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i19;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label18; }

            // CALLORVAR WS
            _ApteridLexicon_Item _r25;

            _r25 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r25 != null) _index = _r25.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridLexicon_Item(_index, _memo.InputEnumerable)); }

            // BIND s
            s = _memo.Results.Peek();

        label18: // AND
            var _r18_2 = _memo.Results.Pop();
            var _r18_1 = _memo.Results.Pop();

            if (_r18_1 != null && _r18_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i18, _index, _memo.InputEnumerable, _r18_1.Results.Concat(_r18_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i18;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label17; }

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "source code in an expression must be indented");

        label17: // AND
            var _r17_2 = _memo.Results.Pop();
            var _r17_1 = _memo.Results.Pop();

            if (_r17_1 != null && _r17_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i17, _index, _memo.InputEnumerable, _r17_1.Results.Concat(_r17_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i17;
            }

        label14: // ARGS 14
            _arg_input_index = _arg_index; // no-op for label

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // ARGS 27
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label27;
            }

            // CALLORVAR WS
            _ApteridLexicon_Item _r30;

            _r30 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r30 != null) _index = _r30.NextIndex;

        label27: // ARGS 27
            _arg_input_index = _arg_index; // no-op for label

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void WS(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

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
                    _memo.Results.Push(new _ApteridLexicon_Item(_start_i2, _index, _memo.InputEnumerable, _res2.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

            // ACT
            var _r1 = _memo.Results.Peek();
            if (_r1 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridLexicon_Item(_r1.StartIndex, _r1.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.Whitespace>(_IM_Result); }, _r1), true) );
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
                    _memo.Results.Push(new _ApteridLexicon_Item(_start_i5, _index, _memo.InputEnumerable, _res5.Where(_NON_NULL), true));
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
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void EOL(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // AND 1
            int _start_i1 = _index;

            // LITERAL '\r'
            _ParseLiteralChar(_memo, ref _index, '\r');

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _ApteridLexicon_Item(_index, _memo.InputEnumerable)); }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // LITERAL '\n'
            _ParseLiteralChar(_memo, ref _index, '\n');

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _ApteridLexicon_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _ApteridLexicon_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { _memo.Positions.Add(_IM_Result.NextIndex);
            return Make<Syntax.EndOfLine>(_IM_Result); }, _r0), true) );
            }

        }


        public void EOF(_ApteridLexicon_Memo _memo, int _index, _ApteridLexicon_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // NOT 1
            int _start_i1 = _index;

            // ANY
            _ParseAny(_memo, ref _index);

            // NOT 1
            var _r1 = _memo.Results.Pop();
            _memo.Results.Push( _r1 == null ? new _ApteridLexicon_Item(_start_i1, _memo.InputEnumerable) : null);
            _index = _start_i1;

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridLexicon_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { _memo.Positions.Add(_IM_Result.NextIndex);
            return Make<Syntax.EndOfFile>(_IM_Result); }, _r0), true) );
            }

        }

        static readonly Verophyle.Regexp.StringRegexp _re0 = new Verophyle.Regexp.StringRegexp(@"_|_[_0-9a-zA-Z]+|[a-zA-Z][_0-9a-zA-Z]*");

    } // class ApteridLexicon

} // namespace Apterid.Bootstrap.Parse.Parser

