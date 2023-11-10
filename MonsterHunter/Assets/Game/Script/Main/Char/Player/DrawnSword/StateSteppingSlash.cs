/*踏み込み斬り*/

using UnityEngine;

public partial class PlayerState
{
    public class StateSteppingSlash : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            if(prevState == _running)
            {
                owner._drawnSwordMotion = true;
            }
            else
            {
                owner._drawnIdleMotion = true;
            }
            owner._attackFrame = 0;

        }

        public override void OnUpdate(PlayerState owner)
        {
            
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            owner._attackFrame++;
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            if(owner._drawnSwordMotion)
            {
                owner._drawnSwordMotion = false;
            }
            owner._drawnIdleMotion = false;
            owner._attackFrame = 0;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            if(owner._attackFrame >= 60)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            // 突き.
            else if(owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            {
                owner.ChangeState(_piercing);
            }
            // 気刃斬り1.
            else if(owner._attackFrame >= 40 && owner._input._RightTrigger >= 0.5)
            {
                owner.ChangeState(_spiritBlade1);
            }
            
        }
    }
}


