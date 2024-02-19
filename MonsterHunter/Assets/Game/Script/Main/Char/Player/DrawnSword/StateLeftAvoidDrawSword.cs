/*¶‰ñ”ð*/

using UnityEngine;

public partial class Player
{
    public class StateLeftAvoidDrawSword : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnLeftAvoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._avoidVelocity = -owner._transform.right * owner._avoidVelocityMagnification;
            owner._nextMotionFlame = 50;
            owner._deceleration = 0.9f;
        }

        public override void OnUpdate(Player owner)
        {

        }

        public override void OnFixedUpdate(Player owner)
        {
            owner._avoidTime++;
            //MoveAvoid(owner);
            owner.MoveAvoid();
            //Debug.Log(owner._isCauseDamage);
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnLeftAvoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(Player owner)
        {
            // ƒAƒCƒhƒ‹.
            if (owner._avoidTime >= owner._nextMotionFlame)
            {
                owner.StateTransition(_idleDrawnSword);
            }
        }
    }
}


