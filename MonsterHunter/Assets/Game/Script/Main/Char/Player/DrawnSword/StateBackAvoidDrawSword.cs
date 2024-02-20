/*回避後の後ろ回避*/

using UnityEngine;

public partial class Player
{
    public class StateBackAvoidDrawSword : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnBackAvoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._avoidVelocity = -owner._transform.forward * (owner._avoidVelocityMagnification + 10);
            owner._nextMotionFlame = 100;
            owner._deceleration = 0.9f;
            owner._flameAvoid = true;
        }

        public override void OnUpdate(Player owner)
        {
            owner._avoidTime++;
            if (owner._stateFlame == 6)
            {
                owner._flameAvoid = false;
            }
        }

        public override void OnFixedUpdate(Player owner)
        {
            
            owner.MoveAvoid();
            
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnBackAvoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(Player owner)
        {
            // 走る.
            if (owner._avoidTime >= 90)
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