/*抜刀待機状態*/

using UnityEngine;

public partial class PlayerState
{
    public class StateIdleDrawnSword : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnIdleMotion = true;
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnIdleMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.DRAWIDLE]) return;

            // 抜刀移動.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWRUN], _runDrawnSword);

            if (owner._openMenu) return;

            // 踏み込み斬り状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.STEPPINGSLASH], _steppingSlash);
            // 突き状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.PRICK], _prick);
            // 気刃斬り1.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1], _spiritBlade1);
            // 納刀.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SHEATHINGSWORD], _sheathingSword);
            // 必殺技の構え
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSTANCE], _stance);
        }
    }
}

