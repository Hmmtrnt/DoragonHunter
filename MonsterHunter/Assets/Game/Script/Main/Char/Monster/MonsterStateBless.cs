/*ブレスを撃つときの状態*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateBless : StateBase
    {
        private int test = 0;

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            // デバッグ用ブレス

            int test = 0;

            owner._trasnform.forward = Vector3.Slerp(owner._trasnform.forward, owner.transform.position, Time.deltaTime * 1.0f);

            test++;

            if(test % 50 == 0)
            {
                Instantiate(owner._fireBall, new Vector3(owner._fireBallPosition.transform.position.x,
                owner._fireBallPosition.transform.position.y,
                owner._fireBallPosition.transform.position.z), Quaternion.identity);
            }
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {

        }

        public override void OnChangeState(MonsterState owner)
        {

        }

    }
}


