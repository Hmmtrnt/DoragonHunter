/*段差を登る*/

using UnityEngine;

public partial class PlayerState
{
    public class StateClimbSteps : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            
        }

        public override void OnUpdate(PlayerState owner)
        {
            
            owner._transform.position += new Vector3(0.0f, 0.01f, 0.0f);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 次の状態遷移を起こすタイミング.
            //if (owner._stateTime <= 1)
            //{
            //    return;
            //}
            //// 待機状態.
            //owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.IDLE], _idle);
            //// 走る状態.
            //owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RUN], _running);
            //// ダッシュ状態.
            //owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DASH], _dash);
        }
    }
}