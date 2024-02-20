/*‰ñ”ðŒã‚ÌŒã‚ë‰ñ”ð*/

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
            owner._avoidVelocity = -owner._transform.forward * owner._avoidVelocityMagnification;
            owner._nextMotionFlame = 50;
            owner._deceleration = 0.93f;
            owner._flameAvoid = true;
        }

        public override void OnUpdate(Player owner)
        {

        }

        public override void OnFixedUpdate(Player owner)
        {
            owner._avoidTime++;
            owner.MoveAvoid();
            if (owner._stateFlame == 6)
            {
                owner._flameAvoid = false;
            }
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnBackAvoidMotion = false;
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