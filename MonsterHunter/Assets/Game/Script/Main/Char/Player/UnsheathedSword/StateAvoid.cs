/*回避*/

using UnityEngine;

public partial class PlayerState
{
    public class StateAvoid : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._isAvoiding = true;
            owner._avoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._rigidbody.velocity = Vector3.zero;
            owner._avoidVelocity = owner._transform.forward * owner._avoidVelocityMagnification;
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

            owner.GetAvoidAdvenceInput(30);

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
            if (owner._stateTime >= 1.0f)
            {
                // スティック傾けていたら移動状態に.
                owner.TransitionMove();
                //owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.AVOID], _avoid);
                owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.IDLE], _idle);
            }
        }

        // 回避処理
        private void MoveAvoid(PlayerState owner)
        {
            // 最初の一回だけ加速.
            if (!owner._isProcess) return;
            
            owner._rigidbody.AddForce(owner._avoidVelocity, ForceMode.Impulse);

            owner._isProcess = false;
            
        }

        
    }
}


