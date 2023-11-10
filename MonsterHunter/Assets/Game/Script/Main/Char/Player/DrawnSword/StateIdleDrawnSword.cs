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

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {

        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnIdleMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 抜刀移動
            if(owner._leftStickHorizontal != 0 || 
                owner._leftStickVertical != 0)
            {
                owner.ChangeState(_runDrawnSword);
            }
            // 踏み込み斬り
            else if (owner._input._YButtonDown)
            {
                owner.ChangeState(_steppingSlash);
            }
            // 突き.
            else if(owner._input._BButtonDown)
            {
                owner.ChangeState(_piercing);
            }
            // 気刃斬り1.
            else if(owner._input._RightTrigger >= 0.5)
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

