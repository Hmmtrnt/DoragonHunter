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
            owner._currentRotateAttack = true;
            owner._AttackPower = 10;
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            if(owner._stateFlame == 90)
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
            else if(owner._stateFlame == 135)
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

            ParticleGenerateTime(owner);

            owner.SEPlay(20, (int)SEManager.MonsterSE.GROAN);
            owner.SEPlay(25, (int)SEManager.MonsterSE.FOOTSMALLSTEP);
            owner.SEPlay(90, (int)SEManager.MonsterSE.ROTATE);

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._rotateMotion = false;
            owner._currentRotateAttack = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 200)
            {
                owner.ChangeState(_idle);
            }
        }

        // パーティクルをモーションを行っている時間で生成する.
        private void ParticleGenerateTime(MonsterState owner)
        {
            if (owner._stateFlame == 90)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.TAIL, (int)footSmokePosition.TAIL);
            }

            owner._footSmokePrehub[(int)footSmokeEffect.TAIL].transform.position = 
                owner._footSmokePosition[(int)footSmokePosition.TAIL].transform.position;
        }
    }
}


