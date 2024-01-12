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
            owner._attackPower = 40;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 0;
            owner._currentRenkiGauge -= 20;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            //if (owner._stateFlame >= 10)
            //{
            //    owner._isCauseDamage = true;
            //}
            //if (owner._stateFlame >= 60)
            //{
            //    owner._isCauseDamage = false;
            //}

            // 一撃目.
            if(owner._stateFlame == 5)
            {
                owner._weaponActive = true;
            }
            else if(owner._stateFlame == 20)
            {
                owner._weaponActive = false;
            }
            // 二撃目.
            else if(owner._stateFlame == 25)
            {
                owner._isCauseDamage = true;
                owner._weaponActive = true;
            }
            else if(owner._stateFlame == 40)
            {
                owner._weaponActive = false;
            }
            // 三撃目.
            else if (owner._stateFlame == 60)
            {
                owner._isCauseDamage = true;
                owner._weaponActive = true;
                owner._attackPower = 100;
            }
            else if (owner._stateFlame == 80)
            {
                owner._weaponActive = false;
            }


            if (owner._stateFlame <= 60)
            {
                owner.ForwardStep(4);
            }
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritBlade3 = false;
            owner._weaponActive = false;
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
                owner._input._RightTrigger >= 0.5 && owner._currentRenkiGauge >= 25)
            {
                owner.ChangeState(_roundSlash);
            }

        }
    }
}


