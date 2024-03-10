/*走る*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRunning : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._runMotion = true;
            owner._moveVelocityMagnification = owner._moveVelocityRunMagnification;
        }

        public override void OnUpdate(PlayerState owner)
        {

            //owner.FixedRotate();

            owner._moveVelocityMagnification = owner._moveVelocityRunMagnification;
            owner.RotateDirection();
            Move(owner);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._runMotion = false;
            owner._moveVelocityMagnification = owner._moveVelocityRunMagnification;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.IDLE], _idle);

            if (owner._openMenu || owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.RUN]) return;

            // ダッシュ状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DASH] &&
                !owner._stateTransitionFlag[(int)StateTransitionKinds.FATIGUEDASH], _dash);
            // 疲労ダッシュ状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DASH] &&
                owner._stateTransitionFlag[(int)StateTransitionKinds.FATIGUEDASH], _fatigueDash);
            // 回避状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.AVOID], _avoid);
            // 回復状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RECOVERY], _recovery);
            // 踏み込み斬り.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.STEPPINGSLASH], _steppingSlash);
            // 気刃斬り1.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1], _spiritBlade1);
        }

        // 移動.
        private void Move(PlayerState owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + 
                new Vector3(0.0f, owner._rigidbody.velocity.y, 0.0f);
            // モーションの再生スピード代入.
            owner._currentRunSpeed = owner._rigidbody.velocity.magnitude / owner._moveVelocityMagnification;
        }
    }
}

