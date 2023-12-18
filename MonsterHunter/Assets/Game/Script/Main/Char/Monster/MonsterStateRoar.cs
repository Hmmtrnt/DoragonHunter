/*™ôšK*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateRoar : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner._isRoar = false;
            owner._roarMotion = true;
            Debug.Log("’Ê‚é");
            owner.StateTransitionInitialization();
        }

        //public override void OnEnter(MonsterState owner, StateBase prevState)
        //{
        //    owner._isRoar = false;
        //    owner._roarMotion = true;
        //    Debug.Log("’Ê‚é");
        //    owner.StateTransitionInitialization();
        //}

        public override void OnUpdate(MonsterState owner)
        {
            owner._isRoar = false;
            //Debug.Log(owner._isRoar);
        }

        public override void OnFixedUpdate(MonsterState owner)
        {

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._roarMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 260)
            {
                owner.ChangeState(_idle);
                //Debug.Log("’Ê‚é");
            }
        }
    }
}


