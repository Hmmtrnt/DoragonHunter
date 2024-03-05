/*待機状態*/

using UnityEngine;

public partial class PlayerState
{
    public class StateIdle : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            // アニメーション開始.
            owner._idleMotion = true;
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._idleMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 移動.
            owner.TransitionMove();

            if (owner._openMenu) return;

            // 回復.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RECOVERY], _recovery);
            // 抜刀する.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWSWORDTRANSITION], _drawSwordTransition);
            // 気刃斬り1.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1], _spiritBlade1);
        }
    }
}


