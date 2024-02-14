/*回避*/

using UnityEngine;

public partial class Player
{
    public class StateAvoid : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._isAvoiding = true;
            owner._avoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._rigidbody.velocity = Vector3.zero;
            owner._avoidVelocity = owner._transform.forward * owner._avoidVelocityMagnification;
            owner._deceleration = 0.9f;
            owner._flameAvoid = true;
        }

        public override void OnUpdate(Player owner)
        {

            
        }

        public override void OnFixedUpdate(Player owner)
        {
            owner._avoidTime++;
            MoveAvoid(owner);
            

            if(owner._avoidTime == 6)
            {
                owner._flameAvoid = false;
            }

        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._isAvoiding = false;
            owner._avoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(Player owner)
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
                    owner._leftStickVertical != 0) && owner._input._RBButton && !owner._openMenu)
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
        private void MoveAvoid(Player owner)
        {
            // 減速
            if (owner._avoidTime <= 10)
            {
                owner._rigidbody.velocity *= owner._deceleration;
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


