/*右回避*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRightAvoidDrawSword : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnRightAvoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._avoidVelocity = owner._transform.right * owner._avoidVelocityMagnification;
            owner._nextMotionFlame = 50;
            owner._deceleration = 0.9f;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            owner._avoidTime++;
            MoveAvoid(owner);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnRightAvoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            if (owner._avoidTime >= owner._nextMotionFlame)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            

        }

        private void MoveAvoid(PlayerState owner)
        {
            if (owner._avoidTime <= 10)
            {
                owner._rigidbody.velocity *= owner._deceleration;
            }

            if (owner._avoidTime >= 30)
            {
                owner._rigidbody.velocity *= 0.8f;
            }

            if (!owner._isProcess) return;

            owner._rigidbody.AddForce(owner._avoidVelocity, ForceMode.Impulse);

            owner._isProcess = false;
        }
    }
}