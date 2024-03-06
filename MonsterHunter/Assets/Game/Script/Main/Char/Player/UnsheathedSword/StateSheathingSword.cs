/*納刀状態に遷移*/

using System.Buffers;
using UnityEngine;

public partial class PlayerState
{
    public class StateSheathingSword : StateBase
    {
        // 前進を始めるタイミング.
        private const float _forwardStartTiming = 0.33f;
        // 移動力.
        private const float _forwardPower = 10;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSheathingSword = true;
            owner._unsheathedSword = false;
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(PlayerState owner)
        {
            if (owner._stateTime <= _forwardStartTiming)
            {
                owner.ForwardStep(_forwardPower);
            }
            // 納刀効果音再生.
            owner.SEPlayTest(0.18f, (int)SEManager.HunterSE.SHEATHINGSWORD);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSheathingSword = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 次の状態遷移を起こすタイミング.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.SHEATHINGSWORD]) return;

            // 待機状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.IDLE], _idle);
            // 走る状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RUN], _running);
            // ダッシュ状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DASH], _dash);
        }
    }
}


