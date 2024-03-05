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

        public override void OnUpdate(PlayerState owner)
        {
            
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._idleMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 移動.
            owner.TransitionRun();

            if (owner._openMenu) return;

            // 回復.
            if (owner._stateTransitionFlag[(int)StateTransitionKinds.RECOVERY])
            {
                owner.StateTransition(_recovery);
            }
            // 抜刀する.
            if (owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWSWORDTRANSITION])
            {
                owner.StateTransition(_drawSwordTransition);
            }
            // 気刃斬り1.
            if (owner._stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1])
            {
                owner.StateTransition(_spiritBlade1);
            }
        }
    }
}


