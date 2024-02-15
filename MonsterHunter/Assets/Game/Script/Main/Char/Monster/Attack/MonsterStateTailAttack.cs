/*尻尾攻撃*/

using UnityEngine;

public partial class Monster
{
    public class MonsterStateTailAttack : StateBase
    {
        public override void OnEnter(Monster owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._tailMotion = true;
            if (owner._mainSceneManager._hitPointMany)
            {
                owner._AttackPower = 13;
            }
            else
            {
                owner._AttackPower = 8;
            }
        }

        public override void OnUpdate(Monster owner)
        {

        }

        public override void OnFixedUpdate(Monster owner)
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
            ParticleGenerateTime(owner);
        }

        public override void OnExit(Monster owner, StateBase nextState)
        {
            owner._tailMotion = false;
        }

        public override void OnChangeState(Monster owner)
        {
            if (owner._stateFlame >= 240)
            {
                owner.ChangeState(_idle);
            }
        }

        // パーティクルをモーションを行っている時間で生成する.
        private void ParticleGenerateTime(Monster owner)
        {
            if (owner._stateFlame == 60)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.LEG, (int)footSmokePosition.TAIL);
            }
            if (owner._stateFlame == 110)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.LEG, (int)footSmokePosition.TAIL);
            }
        }
    }
}


