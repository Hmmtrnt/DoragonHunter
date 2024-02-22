/*気刃斬り1*/

using UnityEngine;

public partial class Player
{
    public class StateSpiritBlade1 : StateBase
    {
        // 一度処理を通したら次から通さない.
        //HACK:変数名を変更.
        private bool _test = false;

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
            _test = false;
        }

        public override void OnUpdate(Player owner)
        {
            if (owner._stateTime >= 0.75f && !_test)
            {
                owner._weaponActive = true;
            }

            if (owner._stateTime >= 1.0f)
            {
                owner._weaponActive = false;
            }

            if (owner._stateTime <= 0.25f)
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
            if (owner._stateTime >= 2.25f)
            {
                owner.StateTransition(_idleDrawnSword);
            }
            // 回避.
            else if (owner._stateTime >= 1.1f &&
                owner._viewDirection[(int)viewDirection.FORWARD] &&
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_avoidDrawnSword);
            }
            // 右回避.
            else if (owner._stateTime >= 1.1f &&
                owner._viewDirection[(int)viewDirection.RIGHT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_rightAvoid);
            }
            // 左回避.
            else if (owner._stateTime >= 1.1f &&
                owner._viewDirection[(int)viewDirection.LEFT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_leftAvoid);
            }
            // 後ろ回避.
            else if (owner._stateTime >= 1.1f &&
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
            else if (owner._stateTime >= 1.1f && 
                owner._input._RightTrigger >= 0.5)
            {
                owner.StateTransition(_spiritBlade2);
            }

        }
    }
}


