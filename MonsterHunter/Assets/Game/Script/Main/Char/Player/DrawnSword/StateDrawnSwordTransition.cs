/*アイドル状態から抜刀状態への移行*/

using System.Buffers;
using UnityEngine;

public partial class PlayerState
{
    public class StateDrawnSwordTransition : StateBase
    {
        // 前進させるタイミング.
        private const float _forwardTiming = 0.2f;
        // 前進を終了させるタイミング.
        private const float _forwardStopTiming = 0.5f;
        // 移動力.
        private const float _forwardPower = 5.5f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSwordMotion = true;
            owner._unsheathedSword = true;
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(PlayerState owner)
        {
            // 前進させる.
            if (owner._stateTime >= _forwardTiming && owner._stateTime <= _forwardStopTiming)
            {
                owner.ForwardStep(_forwardPower);
            }

            // 抜刀効果音再生.
            owner.SEPlay(0.24f, (int)SEManager.HunterSE.DRAWSWORD);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSwordMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 次の状態遷移を起こすタイミング.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.DRAWSWORDTRANSITION])
            {
                return;
            }

            // 抜刀待機状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWIDLE], _idleDrawnSword);
            // 抜刀移動状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWRUN], _runDrawnSword);
        }
    }
}