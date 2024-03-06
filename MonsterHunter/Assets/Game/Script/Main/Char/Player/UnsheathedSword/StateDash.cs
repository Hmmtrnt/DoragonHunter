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
            owner.FixedRotate();
            Move(owner);
            owner.RotateDirection();
            owner.ConsumeStamina();
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._dashMotion = false;
            owner._isDashing = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 待機状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.IDLE], _idle);

            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.DASH]) return;

            // 走る状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RUN], _running);
            // 回避状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.AVOID], _avoid);
            // 疲労ダッシュ状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DASH] &&
                owner._stateTransitionFlag[(int)StateTransitionKinds.FATIGUEDASH], _fatigueDash);
            // 回復状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RECOVERY], _recovery);
            // 踏み込み斬り状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.STEPPINGSLASH], _steppingSlash);
            // 気刃斬り1状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1], _spiritBlade1);
        }

        // 移動
        private void Move(PlayerState owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + 
                new Vector3(0.0f, owner._rigidbody.velocity.y, 0.0f);
        }
    }
}