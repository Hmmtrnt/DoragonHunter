/*突き*/

using UnityEngine;

public partial class PlayerState
{
    public class StatePiercing : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnIdleMotion = true;

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
            owner._drawnIdleMotion = false;
            owner._attackFrame = 0;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            if (owner._attackFrame >= 60)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            // 切り上げ.
            else if (owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            {
                owner.ChangeState(_slashUp);
            }
            // 気刃斬り1.
            else if (owner._attackFrame >= 40 && owner._input._RightTrigger >= 0.5)
            {
                owner.ChangeState(_spiritBlade1);
            }

        }
    }
}

