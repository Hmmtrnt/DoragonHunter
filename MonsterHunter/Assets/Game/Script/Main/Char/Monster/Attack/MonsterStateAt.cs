/*デバッグ用アタック*/

using UnityEngine;

public partial class Monster 
{
    public class MonsterStateAt : StateBase
    {
        private int testTime = 0;

        public override void OnEnter(Monster owner, StateBase prevState)
        {
            testTime = 0;
        }

        public override void OnUpdate(Monster owner)
        {
        }

        public override void OnFixedUpdate(Monster owner)
        {
            testTime++;

            
        }

        public override void OnExit(Monster owner, StateBase nextState)
        {
            
        }

        public override void OnChangeState(Monster owner)
        {
            if (testTime >= 120.0f)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


