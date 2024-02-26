/*ƒ‚ƒ“ƒXƒ^[‚ª‹¯‚Þ‚Æ‚«‚Ìˆ—*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateFalter : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner._falterMotion = true;
            
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._falterMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 300)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}