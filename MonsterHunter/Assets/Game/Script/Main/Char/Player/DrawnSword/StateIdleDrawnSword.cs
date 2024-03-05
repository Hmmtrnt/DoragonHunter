/*抜刀アイドル状態*/

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
            // 抜刀移動.
            if(owner._leftStickHorizontal != 0 || 
                owner._leftStickVertical != 0)
            {
                owner.StateTransition(_runDrawnSword);
            }

            if (owner._openMenu) return;

            // 踏み込み斬り.
            if (owner._input._YButtonDown)
            {
                owner.StateTransition(_steppingSlash);
            }
            // 突き.
            else if (owner._input._BButtonDown && !owner._input._LBButton)
            {
                owner.StateTransition(_piercing);
            }
            // 気刃斬り1.
            else if (owner._input._RightTrigger >= 0.5)
            {
                owner.StateTransition(_spiritBlade1);
            }
            // 納刀.
            else if (owner._input._XButtonDown || owner._input._RBButtonDown)
            {
                owner._unsheathedSword = false;
                owner.StateTransition(_sheathingSword);
            }
            // 必殺技の構え
            else if (owner._input._LBButton && owner._input._BButtonDown && owner._applyRedRenkiGauge)
            {
                owner.StateTransition(_stance);
            }

        }
    }
}

