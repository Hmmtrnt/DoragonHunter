/*気刃大回転斬り*/

using Unity.VisualScripting;
using UnityEngine;

public partial class PlayerState
{
    public class StateRoundSlash : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritRoundSlash = true;
            owner._nextMotionFlame = 90;
            owner._deceleration = 0.9f;
            owner.StateTransitionInitialization();
            owner._attackPower = 150;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 20;
            //owner._currentRenkiGauge -= 25;
            owner._hitStopTime = 0.1f;
        }

        public override void OnUpdate(PlayerState owner)
        {

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

            if(owner._stateFlame == 15)
            {
                owner._weaponActive = true;
            }
            else if(owner._stateFlame == 35)
            {
                owner._weaponActive = false;
            }

            if(owner._stateFlame <= 10)
            {
                owner.ForwardStep(20);
            }
            if(owner._stateFlame >= 10 && owner._stateFlame <= 50)
            {
                owner._rigidbody.velocity *= owner._deceleration;
            }
            else if(owner._stateFlame >= 50)
            {
                owner.ForwardStep(8);
            }

            owner.SEPlay(15, (int)SEManager.HunterSE.MISSINGROUNDSLASH);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritRoundSlash = false;
            owner._unsheathedSword = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 納刀待機.
            if (owner._stateFlame >= owner._nextMotionFlame &&
                (owner._leftStickHorizontal == 0 || owner._leftStickVertical == 0))
            {
                owner.ChangeState(_idle);
            }
            // 移動.
            else if(owner._stateFlame >= owner._nextMotionFlame && 
                (owner._leftStickHorizontal != 0 || owner._leftStickVertical != 0) && 
                !owner._input._RBButton)
            {
                owner.ChangeState(_running);
            }
            // ダッシュ.
            else if(owner._stateFlame >= owner._nextMotionFlame &&
                (owner._leftStickHorizontal != 0 || owner._leftStickVertical != 0) &&
                owner._input._RBButton)
            {
                owner.ChangeState(_dash);
            }
        }
    }
}


