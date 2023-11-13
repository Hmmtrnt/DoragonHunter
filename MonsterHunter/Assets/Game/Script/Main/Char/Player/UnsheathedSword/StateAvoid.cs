/*回避*/

using UnityEngine;

public partial class PlayerState
{
    public class StateAvoid : StateBase
    {
        // 回避した後の減速
        private float _deceleration = 0.95f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._isAvoiding = true;
            owner._avoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._avoidVelocity = owner._transform.forward * owner._avoidVelocityMagnification;
        }

        public override void OnUpdate(PlayerState owner)
        {

            
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            owner._avoidTime++;
            MoveAvoid(owner);
            //Debug.Log(owner._rigidbody.velocity);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._isAvoiding = false;
            owner._avoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(PlayerState owner)
        {
            if(owner._avoidTime >= 30)
            {
                // スティック傾けていたらRunに
                if ((owner._leftStickHorizontal != 0 ||
                    owner._leftStickVertical != 0) && !owner._input._RBButtonDown)
                {
                    owner.ChangeState(_running);
                }

                if ((owner._leftStickHorizontal != 0 ||
                    owner._leftStickVertical != 0) && owner._input._RBButton)
                {
                    owner.ChangeState(_dash);
                }
            }

            if (owner._avoidTime >= owner._avoidMaxTime)
            {
                if (owner._leftStickHorizontal == 0 &&
                    owner._leftStickVertical == 0)
                {
                    owner.ChangeState(_idle);
                }
            }
        }

        // 回避処理
        private void MoveAvoid(PlayerState owner)
        {
            // 減速
            if (owner._avoidTime <= 10)
            {
                owner._rigidbody.velocity *= _deceleration;
            }
            // 一気に減速
            if (owner._avoidTime >= 30)
            {
                owner._rigidbody.velocity *= 0.8f;
            }

            
            // 最初の一フレームだけ加速
            if (!owner._isProcess) return;
            
            owner._rigidbody.AddForce(owner._avoidVelocity, ForceMode.Impulse);

            owner._isProcess = false;
            
        }
    }
}


