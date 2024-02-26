/*回避*/

using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public partial class PlayerState
{
    public class StateAvoid : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._isAvoiding = true;
            owner._avoidMotion = true;
            //owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._rigidbody.velocity = Vector3.zero;
            owner._avoidVelocity = owner._transform.forward * owner._avoidVelocityMagnification;
            owner._deceleration = 0.9f;
            owner._flameAvoid = true;
            owner._rotateSpeed = 40.0f;

            if(prevState == _avoid)
            {
                owner._animator.Play("Avoid", 0, 0);
            }

            owner.RotateDirection();
        }

        public override void OnUpdate(PlayerState owner)
        {
            owner._avoidTime++;

            if (owner._avoidTime == 6)
            {
                owner._flameAvoid = false;
            }

            //Debug.Log(owner._avoidTime);
            //Debug.Log(owner._avoidAdvanceInput);

            owner.GetAvoidAdvenceInput(30);


        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            
            MoveAvoid(owner);
            

            

        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._isAvoiding = false;
            owner._avoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
            owner._avoidAdvanceInput = false;
            owner._rotateSpeed = 10.0f;
        }

        public override void OnChangeState(PlayerState owner)
        {
            //if(owner._avoidTime >= 50)
            //{
            //    if (owner._avoidAdvanceInput)
            //    {
            //        owner.StateTransition(_avoid);
            //    }
            //}

            //if(owner._avoidTime >= 55)
            if (owner._stateTime >= 1.0f)
            {
                // スティック傾けていたらRunに
                //if ((owner._leftStickHorizontal != 0 ||
                //    owner._leftStickVertical != 0) && !owner._input._RBButtonDown)
                //{
                //    owner.StateTransition(_running);
                //}

                //if ((owner._leftStickHorizontal != 0 ||
                //    owner._leftStickVertical != 0) && owner._input._RBButton && !owner._openMenu)
                //{
                //    owner.StateTransition(_dash);
                //}

                owner.TransitionRun();
            }

            if (owner._stateTime >= 1.2f)
            {
                if (owner._leftStickHorizontal == 0 &&
                    owner._leftStickVertical == 0)
                {
                    owner.StateTransition(_idle);
                }
            }
        }

        // 回避処理
        private void MoveAvoid(PlayerState owner)
        {
            // 減速
            //if (owner._stateTime <= 0.1f)
            //{
            //    owner._rigidbody.velocity *= owner._deceleration;
            //}
            //// 一気に減速
            //if (owner._stateTime >= 0.9f)
            //{
            //    owner._rigidbody.velocity *= 0.8f;
            //}

            // 減速
            if (owner._stateTime <= 0.1f)
            {
                owner._rigidbody.velocity *= owner._deceleration;
            }
            // 一気に減速
            if (owner._stateTime >= 0.9f)
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


