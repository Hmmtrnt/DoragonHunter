/*ダッシュ*/

using UnityEngine;

public partial class PlayerState
{
    public class StateDash : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._dashMotion = true;
            owner._isDashing = true;
            owner._moveVelocityMagnification = owner._moveVelocityDashMagnigication;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            Move(owner);
            owner.RotateDirection();

            owner._stamina -= owner._isDashStaminaCost;
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._dashMotion = false;
            owner._isDashing = false;
        }

        public override void OnChangeState(PlayerState owner)
        {

            if(owner._openMenu)
            {
                // idle状態.
                if (owner._leftStickHorizontal == 0 &&
                    owner._leftStickVertical == 0)
                {
                    owner.StateTransition(_idle);
                }
                // run状態.
                else
                {
                    owner.StateTransition(_running);
                }
            }

            // idle状態.
            if(owner._leftStickHorizontal == 0 &&
                owner._leftStickVertical == 0)
            {
                owner.StateTransition(_idle);
            }
            // run状態.
            else if (owner._input._RBButtonUp)
            {
                owner.StateTransition(_running);
            }
            // avoid状態.
            else if (owner._input._AButtonDown && owner._stamina >= owner._maxStamina / 10)
            {
                owner.StateTransition(_avoid);
            }
            // fatigueDash状態.
            else if (owner._stamina <= owner._maxStamina / 5)
            {
                owner.StateTransition(_fatigueDash);
            }
            // 回復状態へ.
            // HACK:アイテムが選ばれている状態の条件も追加する.
            else if (owner._input._XButtonDown && !owner._unsheathedSword && owner._hitPoint != owner._maxHitPoint &&
                owner._cureMedicineNum > 0)
            {
                owner.StateTransition(_recovery);
            }
            // 踏み込み斬り.
            else if (owner._input._YButtonDown)
            {
                owner.StateTransition(_steppingSlash);
            }
            // 気刃斬り1.
            if (owner._input._RightTrigger >= 0.5f)
            {
                owner.StateTransition(_spiritBlade1);
            }
        }

        // 移動
        private void Move(PlayerState owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + 
                new Vector3(0.0f, owner._rigidbody.velocity.y, 0.0f);
        }
    }
}