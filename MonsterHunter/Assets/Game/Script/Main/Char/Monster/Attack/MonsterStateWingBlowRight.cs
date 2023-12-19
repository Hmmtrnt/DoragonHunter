/*右翼で攻撃*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateWingBlowRight : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._wingRightMotion = true;
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._wingRightMotion = false;
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


