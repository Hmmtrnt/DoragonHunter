/*気刃大回転斬り*/

using Unity.VisualScripting;
using UnityEngine;

public partial class PlayerState
{
    public class StateRoundSlash : StateBase
    {
        // 一度処理を通したら次から通さない.
        //HACK:変数名を変更.
        private bool _test = false;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritRoundSlash = true;
            owner._nextMotionFlame = 120;
            owner._deceleration = 0.9f;
            owner.StateTransitionInitialization();
            owner._attackPower = 150;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 20;
            //owner._currentRenkiGauge -= 25;
            owner._hitStopTime = 0.1f;
            owner._attackCol._isOneProcess = true;
            _test = false;
            owner._nextMotionTime = 2.0f;
        }

        public override void OnUpdate(PlayerState owner)
        {
            if (owner._stateTime >= 0.4f && !_test)
            {
                owner._weaponActive = true;
                _test = true;
            }
            if (owner._stateTime >= 0.6f)
            {
                owner._weaponActive = false;
            }

            if (owner._stateTime <= 0.18f)
            {
                owner.ForwardStep(20);
            }
            if (owner._stateTime >= 0.18f && owner._stateTime <= 0.88f)
            {
                owner._rigidbody.velocity *= owner._deceleration;
            }
            else if (owner._stateTime >= 0.88f)
            {
                owner.ForwardStep(8);
            }

            owner.SEPlay(15, (int)SEManager.HunterSE.MISSINGROUNDSLASH);
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
            owner._drawnSpiritRoundSlash = false;
            owner._unsheathedSword = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 納刀待機.
            if (owner._stateTime >= owner._nextMotionTime &&
                (owner._leftStickHorizontal == 0 || owner._leftStickVertical == 0))
            {
                owner.StateTransition(_idle);
            }
            // 移動.
            else if(owner._stateTime >= owner._nextMotionTime && 
                (owner._leftStickHorizontal != 0 || owner._leftStickVertical != 0) && 
                !owner._input._RBButton)
            {
                owner.StateTransition(_running);
            }
            // ダッシュ.
            else if(owner._stateTime >= owner._nextMotionTime &&
                (owner._leftStickHorizontal != 0 || owner._leftStickVertical != 0) &&
                owner._input._RBButton)
            {
                owner.StateTransition(_dash);
            }
        }
    }
}