/*気刃大回転斬り*/

using Unity.VisualScripting;
using UnityEngine;

public partial class Player
{
    public class StateRoundSlash : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
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
        }

        public override void OnUpdate(Player owner)
        {
            if (owner._stateFlame == 25)
            {
                owner._weaponActive = true;
            }
            else if (owner._stateFlame == 49)
            {
                owner._weaponActive = false;
            }

            if (owner._stateFlame <= 10)
            {
                owner.ForwardStep(20);
            }
            if (owner._stateFlame >= 10 && owner._stateFlame <= 50)
            {
                owner._rigidbody.velocity *= owner._deceleration;
            }
            else if (owner._stateFlame >= 50)
            {
                owner.ForwardStep(8);
            }

            owner.SEPlay(15, (int)SEManager.HunterSE.MISSINGROUNDSLASH);
        }

        public override void OnFixedUpdate(Player owner)
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

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnSpiritRoundSlash = false;
            owner._unsheathedSword = false;
        }

        public override void OnChangeState(Player owner)
        {
            // 納刀待機.
            if (owner._stateFlame >= owner._nextMotionFlame &&
                (owner._leftStickHorizontal == 0 || owner._leftStickVertical == 0))
            {
                owner.StateTransition(_idle);
            }
            // 移動.
            else if(owner._stateFlame >= owner._nextMotionFlame && 
                (owner._leftStickHorizontal != 0 || owner._leftStickVertical != 0) && 
                !owner._input._RBButton)
            {
                owner.StateTransition(_running);
            }
            // ダッシュ.
            else if(owner._stateFlame >= owner._nextMotionFlame &&
                (owner._leftStickHorizontal != 0 || owner._leftStickVertical != 0) &&
                owner._input._RBButton)
            {
                owner.StateTransition(_dash);
            }
        }
    }
}


