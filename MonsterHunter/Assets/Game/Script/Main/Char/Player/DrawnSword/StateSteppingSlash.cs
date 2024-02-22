/*踏み込み斬り*/

using Unity.VisualScripting;
using UnityEngine;

public partial class Player
{
    public class StateSteppingSlash : StateBase
    {
        // 一度処理を通したら次から通さない.
        //HACK:変数名を変更.
        private bool _test = false;

        public override void OnEnter(Player owner, StateBase prevState)
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
            owner._hitStopTime = 0.05f;
            owner._attackCol._isOneProcess = true;
            _test = false;
            owner._nextMotionTime = 1.13f;
        }

        public override void OnUpdate(Player owner)
        {
            //if (owner._stateFlame == 65)
            if (owner._stateTime >= 0.9f && !_test )
            {
                owner._weaponActive = true;
                _test = true;
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
            if (owner._stateTime >= 1.7f)
            {
                owner._weaponActive = false;
            }

            // 空振り効果音再生.
            owner.SEPlay(60, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnFixedUpdate(Player owner)
        {
            

        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnSteppingSlash = false;
            //owner._isCauseDamage = false;
            owner._weaponActive = false;
            _test = false;
        }

        public override void OnChangeState(Player owner)
        {
            // アイドル.
            //if(owner._stateFlame>= 150)
            if (owner._stateTime >= 2.5f)
            {
                owner.StateTransition(_idleDrawnSword);
            }
            // 回避.
            //else if (owner._stateFlame >= owner._nextMotionFlame && 
            //    owner._viewDirection[(int)viewDirection.FORWARD] && 
            //    owner.GetDistance() > 1 && 
            //    owner._input._AButtonDown)
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
        private void MissingSlashSound(Player owner)
        {
            if(owner._stateFlame == 0)
            {
                //owner._seManager.HunterPlaySE((int)MainSceneSEManager.HunterSE.MISSINGSLASH);
            }
            
        }
    }
}