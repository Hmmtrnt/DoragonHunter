/*疲労状態のダッシュ*/

using UnityEngine;

public partial class Player
{
    public class StateFatigueDash : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._fatigueMotion = true;
            owner._moveVelocityMagnification = owner._moveVelocityFatigueDashMagnigication;
        }

        public override void OnUpdate(Player owner)
        {

        }

        public override void OnFixedUpdate(Player owner)
        {
            Move(owner);
            owner.RotateDirection();
            owner._stamina -= owner._isDashStaminaCost;
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._fatigueMotion = false;
        }

        public override void OnChangeState(Player owner)
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
        private void Move(Player owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityFatigueDashMagnigication + new Vector3(0.0f, owner._gravity, 0.0f);
        }
    }
}

