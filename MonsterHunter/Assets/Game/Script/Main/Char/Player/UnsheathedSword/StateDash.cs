﻿/*ダッシュ*/

using UnityEngine;

public partial class Player
{
    public class StateDash : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._dashMotion = true;
            owner._isDashing = true;
            owner._moveVelocityMagnification = owner._moveVelocityDashMagnigication;
        }

        public override void OnUpdate(Player owner)
        {

        }

        public override void OnFixedUpdate(Player owner)
        {
            Move(owner);
            owner.RotateDirection();

            owner._stamina -= owner._isDashStaminaCost;
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._dashMotion = false;
            owner._isDashing = false;
        }

        public override void OnChangeState(Player owner)
        {

            if(owner._openMenu)
            {
                // idle状態.
                if (owner._leftStickHorizontal == 0 &&
                    owner._leftStickVertical == 0)
                {
                    owner.ChangeState(_idle);
                }
                // run状態.
                else
                {
                    owner.ChangeState(_running);
                }
            }

            // idle状態.
            if(owner._leftStickHorizontal == 0 &&
                owner._leftStickVertical == 0)
            {
                owner.ChangeState(_idle);
            }
            // run状態.
            else if (owner._input._RBButtonUp)
            {
                owner.ChangeState(_running);
            }
            // avoid状態.
            else if (owner._input._AButtonDown && owner._stamina >= owner._maxStamina / 10)
            {
                owner.ChangeState(_avoid);
            }
            // fatigueDash状態.
            else if (owner._stamina <= owner._maxStamina / 5)
            {
                owner.ChangeState(_fatigueDash);
            }
            // 回復状態へ.
            // HACK:アイテムが選ばれている状態の条件も追加する.
            else if (owner._input._XButtonDown && !owner._unsheathedSword && owner._hitPoint != owner._maxHitPoint &&
                owner._cureMedicineNum > 0)
            {
                owner.ChangeState(_recovery);
            }
            // 踏み込み斬り.
            else if (owner._input._YButtonDown)
            {
                owner.ChangeState(_steppingSlash);
            }
            // 気刃斬り1.
            if (owner._input._RightTrigger >= 0.5f)
            {
                owner.ChangeState(_spiritBlade1);
            }
        }

        // 移動
        private void Move(Player owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + 
                new Vector3(0.0f, owner._rigidbody.velocity.y, 0.0f);
        }
    }
}