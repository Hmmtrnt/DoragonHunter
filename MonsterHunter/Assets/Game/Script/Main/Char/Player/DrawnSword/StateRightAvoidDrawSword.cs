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
            owner._nextMotionFlame = 50;
            owner._deceleration = 0.9f;
        }

        public override void OnUpdate(Player owner)
        {

        }

        public override void OnFixedUpdate(Player owner)
        {
            owner._avoidTime++;
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
            // アイドル.
            if (owner._avoidTime >= owner._nextMotionFlame)
            {
                owner.StateTransition(_idleDrawnSword);
            }
        }
    }
}