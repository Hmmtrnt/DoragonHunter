/*抜刀移動状態*/

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
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWIDLE], _idleDrawnSword);

            // 次の状態遷移を起こすタイミング.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.DRAWRUN]) return;

            // メニュー画面を開いているときは処理をスキップする.
            if (owner._openMenu) return;

            // 踏み込み斬り.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.STEPPINGSLASH], _steppingSlash);
            // 必殺技の構え
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSTANCE], _stance);
            // 突き
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.PRICK], _prick);
            // 気刃斬り1.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1], _spiritBlade1);
            // 回避.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWAVOID], _avoidDrawnSword);
            // 納刀.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SHEATHINGSWORD], _sheathingSword);
        }

        // 移動
        private void Move(PlayerState owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + new Vector3(0.0f, owner._rigidbody.velocity.y, 0.0f);
        }
    }
}


