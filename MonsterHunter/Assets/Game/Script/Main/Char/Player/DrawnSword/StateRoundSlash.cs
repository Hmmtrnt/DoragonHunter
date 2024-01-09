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

            if(owner._stateFlame == 10)
            {
                owner._weaponActive = true;
            }
            else if(owner._stateFlame == 60)
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
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritRoundSlash = false;
            owner._unsheathedSword = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 納刀アイドル.
            if (owner._stateFlame >= owner._nextMotionFlame)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


