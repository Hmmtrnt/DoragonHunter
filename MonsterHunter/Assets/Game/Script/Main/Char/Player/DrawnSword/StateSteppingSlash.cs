/*踏み込み斬り*/

using Unity.VisualScripting;
using UnityEngine;

public partial class Player
{
    public class StateSteppingSlash : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnSteppingSlash = true;
            owner._nextMotionFlame = 50;
            owner._rigidbody.velocity = Vector3.zero;
            owner._unsheathedSword = true;
            owner.StateTransitionInitialization();
            owner._isCauseDamage = true;
            owner._attackPower = 81;
            //owner._weaponActive = true;
            owner._increaseAmountRenkiGauge = 7;
            owner._hitStopTime = 0.05f;
        }

        public override void OnUpdate(Player owner)
        {
            
        }

        public override void OnFixedUpdate(Player owner)
        {
            if(owner._stateFlame == 40)
            {
                owner._weaponActive = true;
            }

            if(owner._stateFlame <= 40 && owner._stateFlame >= 10)
            {
                owner.ForwardStep(8);
            }
            else
            {
                owner._rigidbody.velocity *= 0.8f;
            }


            if(owner._stateFlame >= 60)
            {
                //owner._isCauseDamage = false;
                owner._weaponActive = false;
            }

            // 空振り効果音再生.
            owner.SEPlay(40, (int)SEManager.HunterSE.MISSINGSLASH);

        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnSteppingSlash = false;
            //owner._isCauseDamage = false;
            owner._weaponActive = false;
        }

        public override void OnChangeState(Player owner)
        {
            // アイドル.
            if(owner._stateFlame>= 120)
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
            else if(owner._stateFlame >= owner._nextMotionFlame && 
                owner._viewDirection[(int)viewDirection.LEFT] &&
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_leftAvoid);
            }
            // 後ろ回避.
            else if(owner._stateFlame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.BACKWARD] &&
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.StateTransition(_backAvoid);
            }
            // 突き.
            else if(owner._stateFlame >= owner._nextMotionFlame &&
                (owner._input._YButtonDown || owner._input._BButtonDown))
            {
                owner.StateTransition(_piercing);
            }
            // 気刃斬り1.
            else if(owner._stateFlame >= owner._nextMotionFlame &&
                owner._input._RightTrigger >= 0.5)
            {
                owner.StateTransition(_spiritBlade1);
            }
        }

        // 空振り音再生.
        private void MissingSlashSound(Player owner)
        {
            if(owner._stateFlame == 0)
            {
                //owner._seManager.HunterPlaySE((int)MainSceneSEManager.HunterSE.MISSINGSLASH);
            }
            
        }
    }
}