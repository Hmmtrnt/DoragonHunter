/*ƒ‚ƒ“ƒXƒ^[‚ª‹¯‚Þ‚Æ‚«‚Ìˆ—*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateFalter : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._falterMotion = true;
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(0.2f, (int)SEManager.MonsterSE.FALTER);
        }

        public override void OnFixedUpdate(MonsterState owner)
        {

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._falterMotion = false;
            owner._forwardSpeed = 0;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateTime >= 3.6f)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}