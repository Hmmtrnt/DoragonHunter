/*疲労状態のダッシュ*/

using UnityEngine;

public partial class PlayerState
{
    public class StateFatigueDash : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._fatigueMotion = true;
            owner._moveVelocityMagnification = owner._moveVelocityFatigueDashMagnigication;
        }

        public override void OnUpdate(PlayerState owner)
        {
            Move(owner);
            owner.RotateDirection();
            owner.ConsumeStamina();
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._fatigueMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 待機状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.IDLE], _idle);
            // 走る状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RUN], _running);
        }


        // 移動
        private void Move(PlayerState owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityFatigueDashMagnigication + 
                new Vector3(0.0f, owner._gravity, 0.0f);
        }
    }
}

