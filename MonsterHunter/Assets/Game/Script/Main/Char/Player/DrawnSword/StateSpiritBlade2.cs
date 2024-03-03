/*気刃斬り2*/

using UnityEngine;
using UnityEngine.Rendering;

public partial class PlayerState
{
    public class StateSpiritBlade2 : StateBase
    {
        // 一度処理を通したら次から通さない.
        //HACK:変数名を変更.
        private bool _test = false;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritBlade2 = true;
            owner._nextMotionFlame = 55;
            owner.StateTransitionInitialization();
            owner._attackPower = 114;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 10;
            //owner._currentRenkiGauge -= 15;
            owner._hitStopTime = 0.1f;
            owner._attackCol._isOneProcess = true;
            _test = false;
            owner._nextMotionTime = 0.65f;
        }

        public override void OnUpdate(PlayerState owner)
        {
            if (owner._stateTime >= 0.45f && !_test)
            {
                owner._weaponActive = true;
                _test = true;
            }

            if (owner._stateTime >= 0.6f)
            {
                owner._weaponActive = false;
            }

            // 空振り効果音再生.
            owner.SEPlay(25, (int)SEManager.HunterSE.MISSINGSLASH);
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

            
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritBlade2 = false;
            owner._weaponActive = false;
        }

        public override void OnChangeState(PlayerState owner)
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
            if (owner._stateTime >= 2.0f)
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
            //// 切り上げ.
            //else if (owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            //{
            //    owner.ChangeState(_slashUp);
            //}
            // 気刃斬り3.
            else if (owner._stateTime >= owner._nextMotionTime &&
                owner._input._RightTrigger >= 0.5)
            {
                owner.StateTransition(_spiritBlade3);
            }
        }
    }
}


