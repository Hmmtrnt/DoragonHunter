/*気刃斬り1*/

using UnityEngine;

public partial class Player
{
    public class StateSpiritBlade1 : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnSpiritBlade1 = true;
            owner._nextMotionFlame = 60;
            owner._deceleration = 0.9f;
            owner._unsheathedSword = true;
            owner.StateTransitionInitialization();
            owner._attackPower = 102;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 8;
            //owner._currentRenkiGauge = 10;
            owner._hitStopTime = 0.01f;
            owner._attackCol._isOneProcess = true;
        }

        public override void OnUpdate(Player owner)
        {

            Debug.Log(owner._stateFlame);
            if (owner._stateFlame >= 10)
            {
                //owner._isCauseDamage = true;
                // 減速させる
                owner._rigidbody.velocity *= owner._deceleration;
            }
            if (owner._stateFlame >= 60)
            {
                //owner._isCauseDamage = false;
            }

            if (owner._stateFlame == 45)
            {
                owner._weaponActive = true;
            }
            else if (owner._stateFlame == 75)
            {
                owner._weaponActive = false;
            }

            if (owner._stateFlame <= 15)
            {
                owner.ForwardStep(3);
            }

            // 空振り効果音再生.
            owner.SEPlay(40, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnFixedUpdate(Player owner)
        {
            
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnSpiritBlade1 = false;
            owner._weaponActive = false;
        }

        public override void OnChangeState(Player owner)
        {
            // アイドル.
            if (owner._stateFlame >= 135)
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
            // 突きのちにつなげる.
            //else if (owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            //{
            //    owner.ChangeState(_piercing);
            //}
            // 気刃斬り2.
            else if (owner._stateFlame >= owner._nextMotionFlame && 
                owner._input._RightTrigger >= 0.5)
            {
                owner.StateTransition(_spiritBlade2);
            }

        }
    }
}


