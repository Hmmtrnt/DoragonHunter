/*抜刀アイドル状態*/

using UnityEngine;

public partial class Player
{
    public class StateIdleDrawnSword : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnIdleMotion = true;
        }

        public override void OnUpdate(Player owner)
        {

        }

        public override void OnFixedUpdate(Player owner)
        {

        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnIdleMotion = false;
        }

        public override void OnChangeState(Player owner)
        {
            // 抜刀移動.
            if(owner._leftStickHorizontal != 0 || 
                owner._leftStickVertical != 0)
            {
                owner.ChangeState(_runDrawnSword);
            }

            if (owner._openMenu) return;

            // 踏み込み斬り.
            if (owner._input._YButtonDown)
            {
                owner.ChangeState(_steppingSlash);
            }
            // 突き.
            else if (owner._input._BButtonDown)
            {
                owner.ChangeState(_piercing);
            }
            // 気刃斬り1.
            else if (owner._input._RightTrigger >= 0.5)
            {
                owner.ChangeState(_spiritBlade1);
            }
            // のちに納刀ステートを入れる.
            else if (owner._input._XButtonDown || owner._input._RBButtonDown)
            {
                owner._unsheathedSword = false;
                owner.ChangeState(_sheathingSword);
            }

        }
    }
}

