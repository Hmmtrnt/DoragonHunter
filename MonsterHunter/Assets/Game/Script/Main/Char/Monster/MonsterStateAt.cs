/*デバッグ用アタック*/

using UnityEngine;

public partial class MonsterState 
{
    public class MonsterStateAt : StateBase
    {
        private int testTime = 0;

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            testTime = 0;
            owner._indicateAttackCol = true;
        }

        public override void OnUpdate(MonsterState owner)
        {
        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            testTime++;

            if(testTime >= 2)
            {
                owner._indicateAttackCol = false;
            }
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            
        }

        public override void OnChangeState(MonsterState owner)
        {
            if (testTime >= 120.0f)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


