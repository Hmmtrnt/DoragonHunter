﻿/*走る*/

using UnityEngine;

public partial class Player
{
    public class StateRunning : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._runMotion = true;
            owner._moveVelocityMagnification = owner._moveVelocityRunMagnification;
        }

        public override void OnUpdate(Player owner)
        {
            owner._moveVelocityMagnification = owner._moveVelocityRunMagnification;
        }

        public override void OnFixedUpdate(Player owner)
        {
            owner.RotateDirection();
            Move(owner);

            //owner.SEPlay(10, (int)SEManager.HunterSE.FOOTSTEPLEFT);
            //owner.SEPlay(30, (int)SEManager.HunterSE.FOOTSTEPRIGHT);
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._runMotion = false;
            owner._isDashing = false;
            owner._moveVelocityMagnification = owner._moveVelocityRunMagnification;
        }

        public override void OnChangeState(Player owner)
        {
            // アイドル状態.
            if (owner._leftStickHorizontal == 0.0f &&
                owner._leftStickVertical == 0.0f)
            {
                owner.StateTransition(_idle);
            }

            if (owner._openMenu) return;

            // ダッシュ状態.
            if (owner._input._RBButton && owner._stamina >= owner._maxStamina / 5)
            {
                owner.StateTransition(_dash);
            }
            // 疲労ダッシュ状態.
            else if (owner._input._RBButton && owner._stamina <= owner._maxStamina / 5)
            {
                owner.StateTransition(_fatigueDash);
            }
            // 回避状態.
            else if (owner._input._AButtonDown && owner._stamina >= owner._maxStamina / 10)
            {
                owner.StateTransition(_avoid);
            }
            // 回復状態.
            // HACK:アイテムが選ばれている状態の条件も追加する
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
        private void Move(Player owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + 
                new Vector3(0.0f, owner._rigidbody.velocity.y, 0.0f);
            owner._currentRunSpeed = owner._rigidbody.velocity.magnitude / owner._moveVelocityMagnification;
        }
    }
}

