/*¶—ƒUŒ‚*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateWingBlowLeft : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._wingLeftMotion = true;
            owner._currentWingAttackLeft = true;
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            if(owner._stateFlame == 40 )
            {
                owner._wingLeftCollisiton.SetActive(true);
            }
            else if(owner._stateFlame == 100)
            {
                owner._wingLeftCollisiton.SetActive(false);
            }
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._wingLeftMotion = false;
            owner._currentWingAttackLeft = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 155)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}



