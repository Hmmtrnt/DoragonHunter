﻿/*突き*/

using UnityEngine;

public partial class Player
{
    public class StatePiercing : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnThrustSlash = true;
            owner._nextMotionFlame = 35;
            owner.StateTransitionInitialization();
            owner._attackPower = 55;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 3;
            owner._hitStopTime = 0.01f;
            owner._attackCol._isOneProcess = true;
        }

        public override void OnUpdate(Player owner)
        {
            if (owner._stateFlame == 15)
            {
                owner._weaponActive = true;
            }
            else if (owner._stateFlame == 25)
            {
                owner._weaponActive = false;
            }



            // 前進させる.
            if (owner._stateFlame <= 20)
            {
                owner.ForwardStep(2);
            }

            // 空振り効果音再生.
            owner.SEPlay(10, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnFixedUpdate(Player owner)
        {
            
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnThrustSlash = false;
            owner._stateFlame = 0;
            //owner._isCauseDamage = false;
            owner._weaponActive = false;
        }

        public override void OnChangeState(Player owner)
        {
            // アイドル.
            if (owner._stateFlame >= 70)
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
                owner.GetDistance() > 1 
                && owner._input._AButtonDown)
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
            // 斬り上げ.
            else if (owner._stateFlame >= owner._nextMotionFlame - 10 &&
                (owner._input._YButtonDown || owner._input._BButtonDown))
            {
                owner.StateTransition(_slashUp);
            }
            // 気刃斬り1.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._input._RightTrigger >= 0.5)
            {
                owner.StateTransition(_spiritBlade1);
            }
        }
    }
}

