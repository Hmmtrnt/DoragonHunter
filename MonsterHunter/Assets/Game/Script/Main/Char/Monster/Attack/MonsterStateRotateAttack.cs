﻿/*回転攻撃*/

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
            
            if(owner._mainSceneManager._hitPointMany)
            {
                owner._AttackPower = 15;
            }
            else
            {
                owner._AttackPower = 10;
            }
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(0.4f, (int)SEManager.MonsterSE.GROAN);
            owner.SEPlay(0.5f, (int)SEManager.MonsterSE.FOOTSMALLSTEP);
            owner.SEPlay(1.8f, (int)SEManager.MonsterSE.ROTATE);
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

            

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._rotateMotion = false;
            owner._currentRotateAttack = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateTime >= 4)
            {
                owner.ChangeState(_idle);
            }
        }

        // パーティクルをモーションを行っている時間で生成する.
        private void ParticleGenerateTime(MonsterState owner)
        {
            if (owner._stateTime >= 1.8f && owner._stateTime <=1.9f)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.TAIL, (int)footSmokePosition.TAIL);
            }

            owner._footSmokePrehub[(int)footSmokeEffect.TAIL].transform.position = 
                owner._footSmokePosition[(int)footSmokePosition.TAIL].transform.position;
        }
    }
}


