/*モンスターのアイドル*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateIdle : StateBase
    {
        private int testTime = 0;

        

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            testTime = 0;
        }

        public override void OnUpdate(MonsterState owner)
        {
            
        }

        public override void OnFixedUpdate(MonsterState owner)
        {

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {

        }

        public override void OnChangeState(MonsterState owner)
        {
            if (owner._viewDirection[(int)viewDirection.FORWARD] && owner._isNearDistance)
            {
                owner.ChangeState(_bless);
            }
        }
    }
}



