/*抜刀走る*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRunDrawnSword : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnRunMotion = true;
            owner._moveVelocityMagnification = owner._moveVelocityRunMagnification;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            Move(owner);
            owner.RotateDirection();
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnRunMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 抜刀アイドル状態へ.
            if (owner._leftStickHorizontal == 0 &&
                owner._leftStickVertical == 0)
            {
                owner.StateTransition(_idleDrawnSword);
            }

            if (owner._openMenu) return;

            // 踏み込み斬り.
            if (owner._input._YButtonDown)
            {
                owner.StateTransition(_steppingSlash);
            }
            // 必殺技の構え
            else if (owner._input._LBButton && owner._input._BButtonDown && owner._applyRedRenkiGauge)
            {
                owner.StateTransition(_stance);
            }
            // 突き
            else if(owner._input._BButtonDown)
            {
                owner.StateTransition(_piercing);
            }
            // 気刃斬り1.
            else if (owner._input._RightTrigger >= 0.5f)
            {
                owner.StateTransition(_spiritBlade1);
            }
            // 回避.
            else if (owner._input._AButtonDown)
            {
                owner.StateTransition(_avoidDrawnSword);
            }
            // 納刀.
            else if(owner._input._XButtonDown || owner._input._RBButtonDown)
            {
                owner.StateTransition(_sheathingSword);
            }
        }

        // 移動
        private void Move(PlayerState owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + new Vector3(0.0f, owner._rigidbody.velocity.y, 0.0f);
        }
    }
}


