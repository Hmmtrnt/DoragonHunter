/*気刃斬り2*/

using UnityEngine;

public partial class PlayerState
{
    public class StateSpiritBlade2 : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritBlade2 = true;
            owner._nextMotionFlame = 40;
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            if (owner._stateFlame >= 10)
            {
                owner._isCauseDamage = true;
            }
            if (owner._stateFlame >= 60)
            {
                owner._isCauseDamage = false;
            }
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritBlade2 = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            //if (owner._attackFrame >= 60)
            //{
            //    owner.ChangeState(_idleDrawnSword);
            //}
            //// 切り上げ.
            //else if (owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            //{
            //    owner.ChangeState(_slashUp);
            //}
            //// 気刃斬り3.
            //else if (owner._attackFrame >= 40 && owner._input._RightTrigger >= 0.5)
            //{
            //    owner.ChangeState(_spiritBlade3);
            //}

            // アイドル.
            if (owner._stateFlame >= 120)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            // 回避.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.FORWARD] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.ChangeState(_avoidDrawnSword);
            }
            // 右回避.
            else if (owner._stateFlame >= owner._nextMotionFlame && 
                owner._viewDirection[(int)viewDirection.RIGHT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.ChangeState(_rightAvoid);
            }
            // 左回避.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.LEFT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.ChangeState(_leftAvoid);
            }
            //// 切り上げ.
            //else if (owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            //{
            //    owner.ChangeState(_slashUp);
            //}
            // 気刃斬り3.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._input._RightTrigger >= 0.5)
            {
                owner.ChangeState(_spiritBlade3);
            }
        }
    }
}


