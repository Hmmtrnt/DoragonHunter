/*気刃斬り3*/

using UnityEngine;

public partial class PlayerState
{
    public class StateSpiritBlade3 : StateBase
    {
        // 一度処理を通したら次から通さない.
        //HACK:変数名を変更.
        private bool _test = false;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritBlade3 = true;
            owner._nextMotionFlame = 130;
            owner.StateTransitionInitialization();
            owner._attackPower = 40;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 5;
            //owner._currentRenkiGauge -= 20;
            owner._hitStopTime = 0.05f;
            owner._attackCol._isOneProcess = true;
            _test = false;
            owner._nextMotionTime = 1.6f;
        }

        public override void OnUpdate(PlayerState owner)
        {
            // 一撃目.
            if ((owner._stateTime >= 0.15f && owner._stateTime <= 0.28f) && !_test)
            {
                owner._weaponActive = true;
                _test = true;
            }
            if ((owner._stateTime >= 0.29f && owner._stateTime <= 0.3f) && _test)
            {
                owner._weaponActive = false;
                _test = false;
            }

            //bool flag = (owner._stateTime >= 0.31f && owner._stateTime <= 0.7f) && !_medicineConsume;

            // 二撃目.
            if ((owner._stateTime >= 0.31f && owner._stateTime <= 0.7f) && !_test)
            {
                owner._isCauseDamage = true;
                owner._weaponActive = true;
                owner._attackCol._isOneProcess = true;
                _test = true;
                //Debug.Log("と");

            }

            //Debug.Log(flag);

            if ((owner._stateTime >= 0.75f && owner._stateTime <= 0.8f) && _test)
            {
                owner._weaponActive = false;
                _test = false;
            }

            // 三撃目.
            if ((owner._stateTime >= 1.2f && owner._stateTime <= 1.6f) && !_test)
            {
                owner._isCauseDamage = true;
                owner._weaponActive = true;
                owner._attackCol._isOneProcess = true;
                owner._attackPower = 100;
                _test = true;
            }

            if (owner._stateTime >= 1.65f && _test)
            {
                owner._weaponActive = false;
                _test = false;
            }

            if (owner._stateTime <= 1.0f)
            {
                owner.ForwardStep(4);
            }

            //Debug.Log(owner._stateTime);

            // 空振り効果音再生.
            owner.SEPlay(10, 30, 80, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritBlade3 = false;
            owner._weaponActive = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            if (owner._stateTime >= 3.0f)
            {
                owner.StateTransition(_idleDrawnSword);
            }
            // 回避.
            else if (owner._stateTime >= owner._nextMotionTime &&
                owner._viewDirection[(int)viewDirection.FORWARD] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_avoidDrawnSword);
            }
            // 右回避.
            else if (owner._stateTime >= owner._nextMotionTime &&
                owner._viewDirection[(int)viewDirection.RIGHT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_rightAvoid);
            }
            // 左回避.
            else if (owner._stateTime >= owner._nextMotionTime &&
                owner._viewDirection[(int)viewDirection.LEFT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_leftAvoid);
            }
            // 後ろ回避.
            else if (owner._stateTime >= owner._nextMotionTime &&
                owner._viewDirection[(int)viewDirection.BACKWARD] &&
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_backAvoid);
            }
            // 必殺技の構え
            else if (owner._input._LBButton && owner._input._BButtonDown && owner._applyRedRenkiGauge)
            {
                owner.StateTransition(_stance);
            }
            //// 突き.
            //else if (owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            //{
            //    owner.ChangeState(_piercing);
            //}
            // 気刃大回転斬り.
            else if (owner._stateTime >= owner._nextMotionTime && 
                owner._input._RightTrigger >= 0.5)
            {
                owner.StateTransition(_roundSlash);
            }

        }
    }
}


