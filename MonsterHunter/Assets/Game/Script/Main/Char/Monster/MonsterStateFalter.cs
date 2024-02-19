/*ƒ‚ƒ“ƒXƒ^[‚ª‹¯‚Þ‚Æ‚«‚Ìˆ—*/

using UnityEngine;

public partial class Monster
{
    public class MonsterStateFalter : StateBase
    {
        public override void OnEnter(Monster owner, StateBase prevState)
        {
            owner._falterMotion = true;
            
        }

        public override void OnUpdate(Monster owner)
        {

        }

        public override void OnFixedUpdate(Monster owner)
        {

        }

        public override void OnExit(Monster owner, StateBase nextState)
        {
            owner._falterMotion = false;
        }

        public override void OnChangeState(Monster owner)
        {
            if(owner._stateFlame >= 300)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}