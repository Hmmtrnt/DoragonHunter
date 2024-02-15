/*ƒuƒŒƒX‚ðŒ‚‚Â‚Æ‚«‚Ìó‘Ô*/

using UnityEngine;

public partial class Monster
{
    public class MonsterStateBless : StateBase
    {
        public override void OnEnter(Monster owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._blessMotion = true;
            if (owner._mainSceneManager._hitPointMany)
            {
                owner._AttackPower = 20;
            }
            else
            {
                owner._AttackPower = 15;
            }
        }

        public override void OnUpdate(Monster owner)
        {

        }

        public override void OnFixedUpdate(Monster owner)
        {
            owner.TurnTowards(40);

            // ”­ŽË‚Ÿ.
            if (owner._stateFlame == 55)
            {
                Instantiate(owner._fireBall, new Vector3(owner._fireBallPosition.transform.position.x,
                owner._fireBallPosition.transform.position.y,
                owner._fireBallPosition.transform.position.z), Quaternion.identity);
            }

            owner.SEPlay(55, (int)SEManager.MonsterSE.BLESS);
        }

        public override void OnExit(Monster owner, StateBase nextState)
        {
            owner._blessMotion = false;
        }

        public override void OnChangeState(Monster owner)
        {
            if(owner._stateFlame >= 150)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


