﻿/*抜刀回避*/

using UnityEngine;

public partial class PlayerState
{
    public class StateAvoidDrawSword : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnAvoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._avoidVelocity = owner._transform.forward * owner._avoidVelocityMagnification;
            owner._deceleration = 0.9f;
            owner._flameAvoid = true;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            owner._avoidTime++;
            MoveAvoid(owner);
            if (owner._stateFlame == 6)
            {
                owner._flameAvoid = false;
            }
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnAvoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(PlayerState owner)
        {
            if (owner._avoidTime >= 30)
            {
                // スティック傾けていたらRunに.
                if ((owner._leftStickHorizontal != 0 ||
                    owner._leftStickVertical != 0) && !owner._input._RBButtonDown)
                {
                    owner.ChangeState(_runDrawnSword);
                }
            }

            if (owner._avoidTime >= owner._avoidMaxTime)
            {
                if (owner._leftStickHorizontal == 0 &&
                    owner._leftStickVertical == 0)
                {
                    owner.ChangeState(_idleDrawnSword);
                }
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
                //owner._rigidbody.velocity = new Vector3(0.0f,0.0f,0.0f);
                owner._rigidbody.velocity *= 0.8f;
            }



            if (!owner._isProcess) return;

            owner._rigidbody.AddForce(owner._avoidVelocity, ForceMode.Impulse);

            owner._isProcess = false;

        }
    }
}


