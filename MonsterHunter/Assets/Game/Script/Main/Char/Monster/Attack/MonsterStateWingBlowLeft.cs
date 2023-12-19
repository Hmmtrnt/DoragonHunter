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
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._wingLeftMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 165)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}



