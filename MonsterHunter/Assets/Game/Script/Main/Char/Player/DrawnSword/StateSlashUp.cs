/*斬り上げ*/

using UnityEngine;

public partial class PlayerState
{
    public class StateSlashUp : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSlashUp = true;
            owner._nextMotionFlame = 30;
            owner.StateTransitionInitialization();
            owner._attackPower = 73;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 5;
            owner._hitStopTime = 0.02f;
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

            if(owner._stateFlame == 10)
            {
                owner._weaponActive = true;
            }
            else if(owner._stateFlame == 25)
            {
                owner._weaponActive = false;
            }

            // 前進させる.
            if(owner._stateFlame <= 20)
            {
                owner.ForwardStep(2);
            }
            // 空振り効果音再生.
            owner.SEPlay(10, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSlashUp = false;
            owner._weaponActive = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            if (owner._stateFlame >= 90)
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
            // 突き.
            else if (owner._stateFlame >= owner._nextMotionFlame - 5 && 
                (owner._input._YButtonDown || owner._input._BButtonDown))
            {
                owner.ChangeState(_piercing);
            }
            // 気刃斬り1.
            else if (owner._stateFlame >= owner._nextMotionFlame && 
                owner._input._RightTrigger >= 0.5)
            {
                owner.ChangeState(_spiritBlade1);
            }
        }
    }
}

