/*噛みつき*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateBite : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._biteMotion = true;
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            if(owner._stateFlame >= 20 && owner._stateFlame <=80)
            {
                owner._biteCollisiton.SetActive(true);
            }
            else
            {
                owner._biteCollisiton.SetActive(false);
            }
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._biteMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            //if (owner._collisionTag == "Player")
            if (owner._stateFlame >= 120)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


