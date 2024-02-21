/*斬り上げ*/

using UnityEngine;

public partial class Player
{
    public class StateSlashUp : StateBase
    {

        // 一度処理を通したら次から通さない.
        //HACK:変数名を変更.
        private bool _test = false;
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnSlashUp = true;
            owner._nextMotionFlame = 30;
            owner.StateTransitionInitialization();
            owner._attackPower = 73;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 5;
            owner._hitStopTime = 0.02f;
            owner._attackCol._isOneProcess = true;
            _test = false;
        }

        public override void OnUpdate(Player owner)
        {
            //if (owner._stateFlame == 15)
            if (owner._stateTime >= 0.15f && !_test)
            {
                _test = false;
                owner._weaponActive = true;
            }
            if (owner._stateTime >= 0.45f)
            {
                owner._weaponActive = false;
            }

            // 前進させる.
            if (owner._stateTime <= 0.1f)
            {
                owner.ForwardStep(3.5f);
            }
            // 空振り効果音再生.
            owner.SEPlay(10, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnFixedUpdate(Player owner)
        {
            
            
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnSlashUp = false;
            owner._weaponActive = false;
        }

        public override void OnChangeState(Player owner)
        {
            // アイドル.
            if (owner._stateTime >= 2.1f)
            {
                owner.StateTransition(_idleDrawnSword);
            }
            // 回避.
            else if (owner._stateTime >= 0.5f &&
                owner._viewDirection[(int)viewDirection.FORWARD] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_avoidDrawnSword);
            }
            // 右回避.
            else if (owner._stateTime >= 0.5f &&
                owner._viewDirection[(int)viewDirection.RIGHT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_rightAvoid);
            }
            // 左回避.
            else if (owner._stateTime >= 0.5f &&
                owner._viewDirection[(int)viewDirection.LEFT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_leftAvoid);
            }
            // 後ろ回避.
            else if (owner._stateTime >= 0.5f &&
                owner._viewDirection[(int)viewDirection.BACKWARD] &&
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_backAvoid);
            }
            // 突き.
            else if (owner._stateTime >= 0.5f && 
                (owner._input._YButtonDown || owner._input._BButtonDown))
            {
                owner.StateTransition(_piercing);
            }
            // 気刃斬り1.
            else if (owner._stateTime >= 0.5f && 
                owner._input._RightTrigger >= 0.5)
            {
                owner.StateTransition(_spiritBlade1);
            }
        }
    }
}

