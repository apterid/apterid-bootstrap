//
// IronMeta ApteridPatterns Parser; Generated 2016-04-21 18:22:22Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace Apterid.Bootstrap.Parse.Parser
{

    using _ApteridPatterns_Inputs = IEnumerable<char>;
    using _ApteridPatterns_Results = IEnumerable<Syntax.Node>;
    using _ApteridPatterns_Item = IronMeta.Matcher.MatchItem<char, Syntax.Node>;
    using _ApteridPatterns_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, Syntax.Node>>;
    using _ApteridPatterns_Memo = IronMeta.Matcher.MatchState<char, Syntax.Node>;
    using _ApteridPatterns_Rule = System.Action<IronMeta.Matcher.MatchState<char, Syntax.Node>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, Syntax.Node>>>;
    using _ApteridPatterns_Base = IronMeta.Matcher.Matcher<char, Syntax.Node>;

    public partial class ApteridPatterns : ApteridParserBase
    {
        public ApteridPatterns()
            : base()
        {
            _setTerminals();
        }

        public ApteridPatterns(bool handle_left_recursion)
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


        public void Pattern(_ApteridPatterns_Memo _memo, int _index, _ApteridPatterns_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridPatterns_Item indent = null;

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

            // CALL UnitPattern
            var _start_i3 = _index;
            _ApteridPatterns_Item _r3;

            _ApteridPatterns_Args _actual_args3 = new _ApteridPatterns_Item[] { indent };
            if (_args != null) _actual_args3 = _actual_args3.Concat(_args.Skip(_arg_index));
            _r3 = _MemoCall(_memo, "UnitPattern", _index, UnitPattern, _actual_args3);

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void UnitPattern(_ApteridPatterns_Memo _memo, int _index, _ApteridPatterns_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _ApteridPatterns_Item indent = null;

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

            // CALLORVAR lex.UNIT
            _ApteridPatterns_Item _r5;

            _r5 = _MemoCall(_memo, "lex.UNIT", _index, lex.UNIT, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALL lex.IWS
            var _start_i6 = _index;
            _ApteridPatterns_Item _r6;

            _ApteridPatterns_Args _actual_args6 = new _ApteridPatterns_Item[] { indent };
            if (_args != null) _actual_args6 = _actual_args6.Concat(_args.Skip(_arg_index));
            _r6 = _MemoCall(_memo, "lex.IWS", _index, lex.IWS, _actual_args6);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _ApteridPatterns_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _ApteridPatterns_Item(_r3.StartIndex, _r3.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return Make<Syntax.UnitPattern>(_IM_Result); }, _r3), true) );
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


    } // class ApteridPatterns

} // namespace Apterid.Bootstrap.Parse.Parser

