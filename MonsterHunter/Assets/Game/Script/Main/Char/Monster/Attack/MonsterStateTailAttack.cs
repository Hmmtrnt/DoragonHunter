/*K”öUŒ‚*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateTailAttack : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._tailMotion = true;
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            
            if(owner._stateFlame >= 30) 
            {
                //owner._tailObject.tag = "MonsterAtCol";
            }
            else if(owner._stateFlame >= 200)
            {
                //owner._tailObject.tag = "MonsterTail";
            }

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._tailMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if (owner._stateFlame >= 240)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


