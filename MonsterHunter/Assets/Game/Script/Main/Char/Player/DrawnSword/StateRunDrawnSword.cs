/*抜刀走る*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRunDrawnSword : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnIdleMotion = true;
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
            owner._drawnIdleMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 抜刀アイドル状態へ.
            if(owner._leftStickHorizontal == 0 &&
                owner._leftStickVertical == 0)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            // 踏み込み斬り
            else if (owner._input._YButtonDown)
            {
                owner.ChangeState(_steppingSlash);
            }
            // 回避
            else if(owner._input._AButtonDown)
            {
                owner.ChangeState(_avoidDrawnSword);
            }
        }

        // 移動
        private void Move(PlayerState owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + new Vector3(0.0f, owner._gravity, 0.0f);
        }
    }
}


