/*右回避*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRightAvoidDrawSword : StateBase
    {
        // 回避した後の減速
        private float _deceleration = 0.95f;

        

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnRightAvoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._avoidVelocity = owner._transform.right * owner._avoidVelocityMagnification;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            //owner._attackFrame++;
            //if (owner._attackFrame >= 10)
            //{
            //    owner._isCauseDamage = true;
            //}
            //if (owner._attackFrame >= 60)
            //{
            //    owner._isCauseDamage = false;
            //}

            owner._avoidTime++;
            //MoveAvoid(owner);
            MoveAvoid(owner);
            Debug.Log(owner._isCauseDamage);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            //if(owner._drawnSwordMotion)
            //{
            //    owner._drawnSwordMotion = false;
            //}
            //owner._drawnIdleMotion = false;
            owner._drawnRightAvoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            if (owner._avoidTime >= 120)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            

        }

        private void MoveAvoid(PlayerState owner)
        {
            if (owner._avoidTime <= 10)
            {
                owner._rigidbody.velocity *= _deceleration;
            }

            if (owner._avoidTime >= 30)
            {
                //owner._rigidbody.velocity = new Vector3(0.0f,0.0f,0.0f);
                owner._rigidbody.velocity *= 0.8f;
            }



            if (!owner._isProcess) return;

            owner._rigidbody.AddForce(owner._avoidVelocity, ForceMode.Impulse);

            owner._isProcess = false;
        }
    }
}