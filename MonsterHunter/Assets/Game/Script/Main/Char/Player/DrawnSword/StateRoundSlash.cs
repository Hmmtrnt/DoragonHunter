/*気刃大回転斬り*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRoundSlash : StateBase
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
            // 納刀アイドル.
            if (owner._attackFrame >= 60)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


