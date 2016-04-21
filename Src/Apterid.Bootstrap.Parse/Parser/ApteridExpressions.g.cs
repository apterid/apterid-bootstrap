//
// IronMeta ApteridExpressions Parser; Generated 2016-04-21 18:17:20Z UTC
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

    using _ApteridExpressions_Inputs = IEnumerable<char>;
    using _ApteridExpressions_Results = IEnumerable<Syntax.Node>;
    using _ApteridExpressions_Item = IronMeta.Matcher.MatchItem<char, Syntax.Node>;
    using _ApteridExpressions_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, Syntax.Node>>;
    using _ApteridExpressions_Memo = IronMeta.Matcher.MatchState<char, Syntax.Node>;
    using _ApteridExpressions_Rule = System.Action<IronMeta.Matcher.MatchState<char, Syntax.Node>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, Syntax.Node>>>;
    using _ApteridExpressions_Base = IronMeta.Matcher.Matcher<char, Syntax.Node>;

    public partial class ApteridExpressions : ApteridParserBase
    {
        public ApteridExpressions()
            : base()
        {
            _setTerminals();
        }

        public ApteridExpressions(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "BooleanLiteral",
                "DecimalInteger",
                "IntegerLiteral",
                "Literal",
            };
        }


        public void Expression(_ApteridExpressions_Memo _memo, int _index, _ApteridExpressions_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridExpressions_Item indent = null;

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

            // CALL FuncDefExp
            var _start_i3 = _index;
            _ApteridExpressions_Item _r3;

            _ApteridExpressions_Args _actual_args3 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args3 = _actual_args3.Concat(_args.Skip(_arg_index));
            _r3 = _MemoCall(_memo, "FuncDefExp", _index, FuncDefExp, _actual_args3);

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void FuncDefExp(_ApteridExpressions_Memo _memo, int _index, _ApteridExpressions_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridExpressions_Item indent = null;

            // OR 0
            int _start_i0 = _index;

            // ARGS 1
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label1;
            }

            // AND 4
            int _start_i4 = _index;

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // AND 7
            int _start_i7 = _index;

            // CALL pat.Pattern
            var _start_i8 = _index;
            _ApteridExpressions_Item _r8;

            _ApteridExpressions_Args _actual_args8 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args8 = _actual_args8.Concat(_args.Skip(_arg_index));
            _r8 = _MemoCall(_memo, "pat.Pattern", _index, pat.Pattern, _actual_args8);

            if (_r8 != null) _index = _r8.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label7; }

            // CALL lex.IWS
            var _start_i9 = _index;
            _ApteridExpressions_Item _r9;

            _ApteridExpressions_Args _actual_args9 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args9 = _actual_args9.Concat(_args.Skip(_arg_index));
            _r9 = _MemoCall(_memo, "lex.IWS", _index, lex.IWS, _actual_args9);

            if (_r9 != null) _index = _r9.NextIndex;

        label7: // AND
            var _r7_2 = _memo.Results.Pop();
            var _r7_1 = _memo.Results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _memo.Results.Push( new _ApteridExpressions_Item(_start_i7, _index, _memo.InputEnumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i7;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // LITERAL "=>"
            _ParseLiteralString(_memo, ref _index, "=>");

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _ApteridExpressions_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // CALL lex.IWS
            var _start_i11 = _index;
            _ApteridExpressions_Item _r11;

            _ApteridExpressions_Args _actual_args11 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args11 = _actual_args11.Concat(_args.Skip(_arg_index));
            _r11 = _MemoCall(_memo, "lex.IWS", _index, lex.IWS, _actual_args11);

            if (_r11 != null) _index = _r11.NextIndex;

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _ApteridExpressions_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALL LiteralExp
            var _start_i12 = _index;
            _ApteridExpressions_Item _r12;

            _ApteridExpressions_Args _actual_args12 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args12 = _actual_args12.Concat(_args.Skip(_arg_index));
            _r12 = _MemoCall(_memo, "LiteralExp", _index, LiteralExp, _actual_args12);

            if (_r12 != null) _index = _r12.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _ApteridExpressions_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

        label1: // ARGS 1
            _arg_input_index = _arg_index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

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

            // CALL LiteralExp
            var _start_i16 = _index;
            _ApteridExpressions_Item _r16;

            _ApteridExpressions_Args _actual_args16 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args16 = _actual_args16.Concat(_args.Skip(_arg_index));
            _r16 = _MemoCall(_memo, "LiteralExp", _index, LiteralExp, _actual_args16);

            if (_r16 != null) _index = _r16.NextIndex;

        label13: // ARGS 13
            _arg_input_index = _arg_index; // no-op for label

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void LiteralExp(_ApteridExpressions_Memo _memo, int _index, _ApteridExpressions_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridExpressions_Item indent = null;

            // OR 0
            int _start_i0 = _index;

            // ARGS 1
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label1;
            }

            // AND 4
            int _start_i4 = _index;

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // AND 7
            int _start_i7 = _index;

            // LITERAL '('
            _ParseLiteralChar(_memo, ref _index, '(');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label7; }

            // CALL lex.IWS
            var _start_i9 = _index;
            _ApteridExpressions_Item _r9;

            _ApteridExpressions_Args _actual_args9 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args9 = _actual_args9.Concat(_args.Skip(_arg_index));
            _r9 = _MemoCall(_memo, "lex.IWS", _index, lex.IWS, _actual_args9);

            if (_r9 != null) _index = _r9.NextIndex;

        label7: // AND
            var _r7_2 = _memo.Results.Pop();
            var _r7_1 = _memo.Results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _memo.Results.Push( new _ApteridExpressions_Item(_start_i7, _index, _memo.InputEnumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i7;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // CALL Expression
            var _start_i10 = _index;
            _ApteridExpressions_Item _r10;

            _ApteridExpressions_Args _actual_args10 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args10 = _actual_args10.Concat(_args.Skip(_arg_index));
            _r10 = _MemoCall(_memo, "Expression", _index, Expression, _actual_args10);

            if (_r10 != null) _index = _r10.NextIndex;

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _ApteridExpressions_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // CALL lex.IWS
            var _start_i11 = _index;
            _ApteridExpressions_Item _r11;

            _ApteridExpressions_Args _actual_args11 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args11 = _actual_args11.Concat(_args.Skip(_arg_index));
            _r11 = _MemoCall(_memo, "lex.IWS", _index, lex.IWS, _actual_args11);

            if (_r11 != null) _index = _r11.NextIndex;

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _ApteridExpressions_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // LITERAL ')'
            _ParseLiteralChar(_memo, ref _index, ')');

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _ApteridExpressions_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

        label1: // ARGS 1
            _arg_input_index = _arg_index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

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

            // CALL Literal
            var _start_i16 = _index;
            _ApteridExpressions_Item _r16;

            _ApteridExpressions_Args _actual_args16 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args16 = _actual_args16.Concat(_args.Skip(_arg_index));
            _r16 = _MemoCall(_memo, "Literal", _index, Literal, _actual_args16);

            if (_r16 != null) _index = _r16.NextIndex;

        label13: // ARGS 13
            _arg_input_index = _arg_index; // no-op for label

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Literal(_ApteridExpressions_Memo _memo, int _index, _ApteridExpressions_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridExpressions_Item indent = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            // QUES
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Pop(); _memo.ArgResults.Push(new _ApteridExpressions_Item(_arg_index, _memo.InputEnumerable)); }

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // OR 4
            int _start_i4 = _index;

            // CALLORVAR BooleanLiteral
            _ApteridExpressions_Item _r5;

            _r5 = _MemoCall(_memo, "BooleanLiteral", _index, BooleanLiteral, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i4; } else goto label4;

            // CALLORVAR IntegerLiteral
            _ApteridExpressions_Item _r6;

            _r6 = _MemoCall(_memo, "IntegerLiteral", _index, IntegerLiteral, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // OR
            int _dummy_i4 = _index; // no-op for label

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void BooleanLiteral(_ApteridExpressions_Memo _memo, int _index, _ApteridExpressions_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // REGEXP true|false
            _ParseRegexp(_memo, ref _index, _re0);

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridExpressions_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { var str = new string (_IM_Result.Inputs.ToArray());
            return Make<Syntax.Literal<bool>>(_IM_Result, new { value = bool.Parse(str) }); }, _r0), true) );
            }

        }


        public void IntegerLiteral(_ApteridExpressions_Memo _memo, int _index, _ApteridExpressions_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // CALLORVAR DecimalInteger
            _ApteridExpressions_Item _r0;

            _r0 = _MemoCall(_memo, "DecimalInteger", _index, DecimalInteger, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void DecimalInteger(_ApteridExpressions_Memo _memo, int _index, _ApteridExpressions_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // REGEXP [0-9]+(_?[0-9]+)*
            _ParseRegexp(_memo, ref _index, _re1);

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridExpressions_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { var str = new string(_IM_Result.Inputs.ToArray());
            var value = BigInteger.Parse(str.Replace("_", ""));
            return Make<Syntax.Literal<BigInteger>>(_IM_Result, new { value = value }); }, _r0), true) );
            }

        }

        static readonly Verophyle.Regexp.StringRegexp _re0 = new Verophyle.Regexp.StringRegexp(@"true|false");
        static readonly Verophyle.Regexp.StringRegexp _re1 = new Verophyle.Regexp.StringRegexp(@"[0-9]+(_?[0-9]+)*");

    } // class ApteridExpressions

} // namespace Apterid.Bootstrap.Parse.Parser

