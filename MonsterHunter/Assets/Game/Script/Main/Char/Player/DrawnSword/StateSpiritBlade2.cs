/*気刃斬り2*/

using UnityEngine;
using UnityEngine.Rendering;

public partial class Player
{
    public class StateSpiritBlade2 : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnSpiritBlade2 = true;
            owner._nextMotionFlame = 40;
            owner.StateTransitionInitialization();
            owner._attackPower = 114;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 10;
            //owner._currentRenkiGauge -= 15;
            owner._hitStopTime = 0.01f;
        }

        public override void OnUpdate(Player owner)
        {

        }

        public override void OnFixedUpdate(Player owner)
        {
            //if (owner._stateFlame >= 10)
            //{
            //    owner._isCauseDamage = true;
            //}
            //if (owner._stateFlame >= 60)
            //{
            //    owner._isCauseDamage = false;
            //}

            if(owner._stateFlame == 20)
            {
                owner._weaponActive = true;
            }
            else if(owner._stateFlame == 40)
            {
                owner._weaponActive = false;
            }

            // 空振り効果音再生.
            owner.SEPlay(15, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnSpiritBlade2 = false;
            owner._weaponActive = false;
        }

        public override void OnChangeState(Player owner)
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
            if (owner._stateFlame >= 100)
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
            //// 切り上げ.
            //else if (owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            //{
            //    owner.ChangeState(_slashUp);
            //}
            // 気刃斬り3.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._input._RightTrigger >= 0.5)
            {
                owner.StateTransition(_spiritBlade3);
            }
        }
    }
}


