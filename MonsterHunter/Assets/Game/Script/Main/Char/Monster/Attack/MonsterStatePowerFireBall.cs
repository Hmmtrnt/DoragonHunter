/*大技火球*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStatePowerFireBall : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {

        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            Debug.Log("大技火球");
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {

        }

        public override void OnChangeState(MonsterState owner)
        {
            if (owner._collisionTag == "Player")
            {
                owner.ChangeState(_at);
            }
        }
    }
}


