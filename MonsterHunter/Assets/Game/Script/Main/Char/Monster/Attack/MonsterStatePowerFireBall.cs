/*大技火球*/

using UnityEngine;

public partial class Monster
{
    public class MonsterStatePowerFireBall : StateBase
    {
        public override void OnEnter(Monster owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(Monster owner)
        {

        }

        public override void OnFixedUpdate(Monster owner)
        {
            Debug.Log("大技火球");
        }

        public override void OnExit(Monster owner, StateBase nextState)
        {

        }

        public override void OnChangeState(Monster owner)
        {
            if (owner._collisionTag == "Player")
            {
                owner.ChangeState(_at);
            }
        }
    }
}


