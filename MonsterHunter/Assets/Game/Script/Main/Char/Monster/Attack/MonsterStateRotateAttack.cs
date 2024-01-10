/*回転攻撃*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateRotateAttack : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._rotateMotion = true;
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            if(owner._stateFlame == 80)
            {
                owner._biteCollisiton.SetActive(true);
                owner._rushCollisiton.SetActive(true);
                owner._wingRightCollisiton.SetActive(true);
                owner._wingLeftCollisiton.SetActive(true);
                for (int colNum = 0; colNum < owner._tailCollisiton.Length; colNum++)
                {
                    owner._tailCollisiton[colNum].SetActive(true);
                }
                owner._rotateCollisiton.SetActive(true);
            }
            else if(owner._stateFlame == 170)
            {
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
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._rotateMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 200)
            {
                owner.ChangeState(_idle);
            }
            
        }
    }
}


