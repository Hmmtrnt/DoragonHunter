/*踏み込み斬り*/

using Unity.VisualScripting;
using UnityEngine;

public partial class PlayerState
{
    public class StateSteppingSlash : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSteppingSlash = true;
            owner._nextMotionFlame = 80;
            owner._rigidbody.velocity = Vector3.zero;
            owner._unsheathedSword = true;
            owner.StateTransitionInitialization();
            owner._isCauseDamage = true;
            owner._attackPower = 81;
            //owner._weaponActive = true;
            owner._increaseAmountRenkiGauge = 7;
            owner._hitStopTime = 0.1f;
            owner._attackCol._isOneProcess = true;
            owner._isAttackProcess = false;
            owner._nextMotionTime = 1.13f;
        }

        public override void OnUpdate(PlayerState owner)
        {
            //if (owner._stateFlame == 65)
            if (owner._stateTime >= 0.9f && !owner._isAttackProcess)
            {
                owner._weaponActive = true;
                owner._isAttackProcess = true;
            }

            //if (owner._stateFlame <= 70 && owner._stateFlame >= 10)
            if (owner._stateTime <= 1.1f && owner._stateTime >= 0.1f)
            {
                owner.ForwardStep(8);
            }
            else
            {
                owner._rigidbody.velocity *= 0.8f;
            }
            //if (owner._stateFlame >= 100)
            if (owner._stateTime >= 1.6f)
            {
                owner._weaponActive = false;
            }

            // 空振り効果音再生.
            owner.SEPlay(60, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSteppingSlash = false;
            //owner._isCauseDamage = false;
            owner._weaponActive = false;
            owner._isAttackProcess = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            //if(owner._stateFlame>= 150)
            if (owner._stateTime >= 2.5f)
            {
                owner.StateTransition(_idleDrawnSword);
            }
            // 前回避.
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
            else if(owner._stateTime >= owner._nextMotionTime && 
                owner._viewDirection[(int)viewDirection.LEFT] &&
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_leftAvoid);
            }
            // 後ろ回避.
            else if(owner._stateTime >= owner._nextMotionTime &&
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
            // 突き.
            else if(owner._stateTime >= owner._nextMotionTime &&
                (owner._input._YButtonDown || owner._input._BButtonDown))
            {
                owner.StateTransition(_piercing);
            }
            // 気刃斬り1.
            else if(owner._stateTime >= owner._nextMotionTime &&
                owner._input._RightTrigger >= 0.5)
            {
                owner.StateTransition(_spiritBlade1);
            }
        }

        // 空振り音再生.
        private void MissingSlashSound(PlayerState owner)
        {
            if(owner._stateFlame == 0)
            {
                //owner._seManager.HunterPlaySE((int)MainSceneSEManager.HunterSE.MISSINGSLASH);
            }
            
        }
    }
}