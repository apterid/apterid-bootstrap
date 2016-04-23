//
// IronMeta ApteridExpressions Parser; Generated 2016-04-23 22:41:57Z UTC
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

            // CALL FunctionLiteral
            var _start_i3 = _index;
            _ApteridExpressions_Item _r3;

            _ApteridExpressions_Args _actual_args3 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args3 = _actual_args3.Concat(_args.Skip(_arg_index));
            _r3 = _MemoCall(_memo, "FunctionLiteral", _index, FunctionLiteral, _actual_args3);

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void FunctionLiteral(_ApteridExpressions_Memo _memo, int _index, _ApteridExpressions_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridExpressions_Item indent = null;
            _ApteridExpressions_Item p = null;
            _ApteridExpressions_Item e = null;

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

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // OR 7
            int _start_i7 = _index;

            // AND 8
            int _start_i8 = _index;

            // CALL pat.Pattern
            var _start_i10 = _index;
            _ApteridExpressions_Item _r10;

            _ApteridExpressions_Args _actual_args10 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args10 = _actual_args10.Concat(_args.Skip(_arg_index));
            _r10 = _MemoCall(_memo, "pat.Pattern", _index, pat.Pattern, _actual_args10);

            if (_r10 != null) _index = _r10.NextIndex;

            // BIND p
            p = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label8; }

            // CALL lex.IWS
            var _start_i11 = _index;
            _ApteridExpressions_Item _r11;

            _ApteridExpressions_Args _actual_args11 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args11 = _actual_args11.Concat(_args.Skip(_arg_index));
            _r11 = _MemoCall(_memo, "lex.IWS", _index, lex.IWS, _actual_args11);

            if (_r11 != null) _index = _r11.NextIndex;

        label8: // AND
            var _r8_2 = _memo.Results.Pop();
            var _r8_1 = _memo.Results.Pop();

            if (_r8_1 != null && _r8_2 != null)
            {
                _memo.Results.Push( new _ApteridExpressions_Item(_start_i8, _index, _memo.InputEnumerable, _r8_1.Results.Concat(_r8_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i8;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i7; } else goto label7;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "expected pattern");

        label7: // OR
            int _dummy_i7 = _index; // no-op for label

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // OR 13
            int _start_i13 = _index;

            // AND 14
            int _start_i14 = _index;

            // CALLORVAR lex.MKF
            _ApteridExpressions_Item _r15;

            _r15 = _MemoCall(_memo, "lex.MKF", _index, lex.MKF, null);

            if (_r15 != null) _index = _r15.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label14; }

            // CALL lex.IWS
            var _start_i16 = _index;
            _ApteridExpressions_Item _r16;

            _ApteridExpressions_Args _actual_args16 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args16 = _actual_args16.Concat(_args.Skip(_arg_index));
            _r16 = _MemoCall(_memo, "lex.IWS", _index, lex.IWS, _actual_args16);

            if (_r16 != null) _index = _r16.NextIndex;

        label14: // AND
            var _r14_2 = _memo.Results.Pop();
            var _r14_1 = _memo.Results.Pop();

            if (_r14_1 != null && _r14_2 != null)
            {
                _memo.Results.Push( new _ApteridExpressions_Item(_start_i14, _index, _memo.InputEnumerable, _r14_1.Results.Concat(_r14_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i14;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i13; } else goto label13;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "expected \"=>\"");

        label13: // OR
            int _dummy_i13 = _index; // no-op for label

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

            // OR 18
            int _start_i18 = _index;

            // CALL LiteralExp
            var _start_i20 = _index;
            _ApteridExpressions_Item _r20;

            _ApteridExpressions_Args _actual_args20 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args20 = _actual_args20.Concat(_args.Skip(_arg_index));
            _r20 = _MemoCall(_memo, "LiteralExp", _index, LiteralExp, _actual_args20);

            if (_r20 != null) _index = _r20.NextIndex;

            // BIND e
            e = _memo.Results.Peek();

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i18; } else goto label18;

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors();
            _memo.AddError(_index, () => "expected expression");

        label18: // OR
            int _dummy_i18 = _index; // no-op for label

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

            // ACT
            var _r4 = _memo.Results.Peek();
            if (_r4 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _ApteridExpressions_Item(_r4.StartIndex, _r4.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.FunctionLiteral>(_IM_Result, new
            {
                pattern = p.Results.FirstOrDefault(),
                body = e.Results
            }); }, _r4), true) );
            }

        label1: // ARGS 1
            _arg_input_index = _arg_index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // ARGS 22
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND indent
            indent = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label22;
            }

            // CALL LiteralExp
            var _start_i25 = _index;
            _ApteridExpressions_Item _r25;

            _ApteridExpressions_Args _actual_args25 = new _ApteridExpressions_Item[] { indent };
            if (_args != null) _actual_args25 = _actual_args25.Concat(_args.Skip(_arg_index));
            _r25 = _MemoCall(_memo, "LiteralExp", _index, LiteralExp, _actual_args25);

            if (_r25 != null) _index = _r25.NextIndex;

        label22: // ARGS 22
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

