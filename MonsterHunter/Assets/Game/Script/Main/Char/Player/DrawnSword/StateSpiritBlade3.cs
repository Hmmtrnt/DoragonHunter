/*気刃斬り3*/

using UnityEngine;

public partial class Player
{
    public class StateSpiritBlade3 : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnSpiritBlade3 = true;
            owner._nextMotionFlame = 120;
            owner.StateTransitionInitialization();
            owner._attackPower = 40;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 5;
            //owner._currentRenkiGauge -= 20;
            owner._hitStopTime = 0.01f;
        }

        public override void OnUpdate(Player owner)
        {
            // 一撃目.
            if (owner._stateFlame == 10)
            {
                owner._weaponActive = true;
            }
            else if (owner._stateFlame == 25)
            {
                owner._weaponActive = false;
            }
            // 二撃目.
            else if (owner._stateFlame == 30)
            {
                owner._isCauseDamage = true;
                owner._weaponActive = true;
            }
            else if (owner._stateFlame == 45)
            {
                owner._weaponActive = false;
            }
            // 三撃目.
            else if (owner._stateFlame == 80)
            {
                owner._isCauseDamage = true;
                owner._weaponActive = true;
                owner._attackPower = 100;
            }
            else if (owner._stateFlame == 100)
            {
                owner._weaponActive = false;
            }


            if (owner._stateFlame <= 60)
            {
                owner.ForwardStep(4);
            }

            // 空振り効果音再生.
            owner.SEPlay(10, 30, 80, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnFixedUpdate(Player owner)
        {
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnSpiritBlade3 = false;
            owner._weaponActive = false;
        }

        public override void OnChangeState(Player owner)
        {
            // アイドル.
            if (owner._stateFlame >= 180)
            {
                owner.StateTransition(_idleDrawnSword);
            }
            // 回避.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.FORWARD] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_avoidDrawnSword);
            }
            // 右回避.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.RIGHT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_rightAvoid);
            }
            // 左回避.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.LEFT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_leftAvoid);
            }
            // 後ろ回避.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.BACKWARD] &&
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_backAvoid);
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
                owner.StateTransition(_roundSlash);
            }

        }
    }
}


