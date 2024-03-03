/*ƒ‚ƒ“ƒXƒ^[‚ª‹¯‚Ş‚Æ‚«‚Ìˆ—*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateFalter : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._falterMotion = true;
            owner._biteCollisiton.SetActive(false);
            owner._rushCollisiton.SetActive(false);
            owner._wingRightCollisiton.SetActive(false);
            owner._wingLeftCollisiton.SetActive(false);
            for (int colNum = 0; colNum < owner._tailCollisiton.Length; colNum++)
            {
                owner._tailCollisiton[colNum].SetActive(false);
            }
            owner._rotateCollisiton.SetActive(false);
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(0.2f, (int)SEManager.MonsterSE.FALTER);
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