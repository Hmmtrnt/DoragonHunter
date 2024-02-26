/*抜刀前転回避*/

using UnityEngine;

public partial class PlayerState
{
    public class StateAvoidDrawSword : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnAvoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._avoidVelocity = owner._transform.forward * owner._avoidVelocityMagnification;
            owner._deceleration = 0.9f;
            owner._flameAvoid = true;
        }

        public override void OnUpdate(PlayerState owner)
        {
            owner._avoidTime++;
            //owner.MoveAvoid();
            if (owner._stateFlame == 6)
            {
                owner._flameAvoid = false;
            }
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            //owner._avoidTime++;
            owner.MoveAvoid();
            //if (owner._stateFlame == 6)
            //{
            //    owner._flameAvoid = false;
            //}
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnAvoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 走る.
            if (owner._stateTime >= 1.0f)
            {
                // スティック傾けていたらRunに.
                if (owner._leftStickHorizontal != 0 ||
                    owner._leftStickVertical != 0)
                {
                    owner.StateTransition(_runDrawnSword);
                }
            }
            // 待機状態.
            //if (owner._avoidTime >= owner._avoidMaxTime)
            if (owner._stateTime >= 1.3f)
            {
                if (owner._leftStickHorizontal == 0 &&
                    owner._leftStickVertical == 0)
                {
                    owner.StateTransition(_idleDrawnSword);
                }
            }
        }
    }
}


