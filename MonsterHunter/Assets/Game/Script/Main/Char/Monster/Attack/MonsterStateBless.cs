/*ブレスを撃つときの状態*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateBless : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._blessMotion = true;
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            owner.TurnTowards(40);

            // 発射ぁ.
            if (owner._stateFlame == 60)
            {
                Instantiate(owner._fireBall, new Vector3(owner._fireBallPosition.transform.position.x,
                owner._fireBallPosition.transform.position.y,
                owner._fireBallPosition.transform.position.z), Quaternion.identity);
            }
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._blessMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 150)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


