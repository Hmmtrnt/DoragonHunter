/*左回避*/

using UnityEngine;

public partial class PlayerState
{
    public class StateLeftAvoidDrawSword : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnLeftAvoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._avoidVelocity = -owner._transform.right * owner._avoidVelocityMagnification;
            owner._nextMotionFlame = 80;
            owner._deceleration = 0.9f;
        }

        public override void OnUpdate(PlayerState owner)
        {
            //owner._avoidTime++;
            owner.MoveAvoid();
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            
            
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnLeftAvoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 走る.
            if (owner._stateTime >= 1.18f)
            {
                // スティック傾けていたらRunに.
                if (owner._leftStickHorizontal != 0 ||
                    owner._leftStickVertical != 0)
                {
                    owner.StateTransition(_runDrawnSword);
                }
            }
            // 待機.
            if (owner._stateTime >= 1.28f)
            {
                owner.StateTransition(_idleDrawnSword);
            }
        }
    }
}


