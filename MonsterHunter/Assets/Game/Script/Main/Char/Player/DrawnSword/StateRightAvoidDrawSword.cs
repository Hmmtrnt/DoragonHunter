/*右回避*/

using UnityEngine;

public partial class Player
{
    public class StateRightAvoidDrawSword : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnRightAvoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._avoidVelocity = owner._transform.right * owner._avoidVelocityMagnification;
            owner._nextMotionFlame = 80;
            owner._deceleration = 0.9f;
        }

        public override void OnUpdate(Player owner)
        {
            owner._avoidTime++;
        }

        public override void OnFixedUpdate(Player owner)
        {
            owner.MoveAvoid();
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnRightAvoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(Player owner)
        {
            // 走る.
            if(owner._avoidTime >= 70)
            {
                // スティック傾けていたらRunに.
                if (owner._leftStickHorizontal != 0 ||
                    owner._leftStickVertical != 0)
                {
                    owner.StateTransition(_runDrawnSword);
                }
            }
            // 待機.
            if (owner._avoidTime >= owner._nextMotionFlame)
            {
                owner.StateTransition(_idleDrawnSword);
            }
        }
    }
}