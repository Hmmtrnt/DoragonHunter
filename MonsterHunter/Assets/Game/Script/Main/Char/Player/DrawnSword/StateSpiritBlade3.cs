/*気刃斬り3*/

using UnityEngine;

public partial class PlayerState
{
    public class StateSpiritBlade3 : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritBlade3 = true;
            owner._nextMotionFlame = 90;
            owner.StateTransitionInitialization();
            owner._attackDamage = 40;
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

            if(owner._stateFlame <= 60)
            {
                owner.ForwardStep(4);
            }
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritBlade3 = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
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
            //// 突き.
            //else if (owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            //{
            //    owner.ChangeState(_piercing);
            //}
            // 気刃大回転斬り.
            else if (owner._stateFlame >= owner._nextMotionFlame && 
                owner._input._RightTrigger >= 0.5)
            {
                owner.ChangeState(_roundSlash);
            }

        }
    }
}


