/*疲労状態のダッシュ*/

using UnityEngine;

public partial class PlayerState
{
    public class StateFatigueDash : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._runMotion = true;
            owner._moveVelocityMagnification = owner._moveVelocityFatigueDashMagnigication;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            Move(owner);
            owner.RotateDirection();
            owner._stamina -= owner._isDashStaminaCost;
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._runMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // idle状態.
            if(owner._leftStickHorizontal == 0 &&
                owner._leftStickVertical == 0)
            {
                owner.ChangeState(_idle);
            }
            // run状態.
            else if (owner._input._RBButtonUp)
            {
                owner.ChangeState(_running);
            }

            
        }


        // 移動
        private void Move(PlayerState owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + new Vector3(0.0f, owner._gravity, 0.0f);
        }
    }
}

