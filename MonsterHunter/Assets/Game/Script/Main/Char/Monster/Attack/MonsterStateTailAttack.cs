/*êKîˆçUåÇ*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateTailAttack : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._tailMotion = true;
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            
            if(owner._stateFlame == 30) 
            {
                //owner._tailObject.tag = "MonsterAtCol";

                for(int colNum = 0; colNum < owner._tailCollisiton.Length; colNum++)
                {
                    owner._tailCollisiton[colNum].SetActive(true);
                }
            }
            else if(owner._stateFlame == 170)
            {
                for (int colNum = 0; colNum < owner._tailCollisiton.Length; colNum++)
                {
                    owner._tailCollisiton[colNum].SetActive(false);
                }
            }

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._tailMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if (owner._stateFlame >= 240)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


