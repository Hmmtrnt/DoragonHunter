/*�����U��*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateWingBlowLeft : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._wingLeftMotion = true;
            owner._currentWingAttackLeft = true;
            if (owner._mainSceneManager._hitPointMany)
            {
                owner._AttackPower = 10;
            }
            else
            {
                owner._AttackPower = 5;
            }
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            if(owner._stateFlame == 40 )
            {
                owner._wingLeftCollisiton.SetActive(true);
            }
            else if(owner._stateFlame == 100)
            {
                owner._wingLeftCollisiton.SetActive(false);
            }

            ParticleGenerateTime(owner);

            owner.SEPlay(0.4f, (int)SEManager.MonsterSE.FOOTSTEP);
            owner.PlayOneShotReset(0.5f);
            owner.SEPlay(1.1f, (int)SEManager.MonsterSE.ROTATE);
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._wingLeftMotion = false;
            owner._currentWingAttackLeft = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 135)
            {
                owner.ChangeState(_idle);
            }
        }

        // �p�[�e�B�N�������[�V�������s���Ă��鎞�ԂŐ�������.
        private void ParticleGenerateTime(MonsterState owner)
        {
            if (owner._stateFlame == 35)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.LEG, (int)footSmokePosition.WINGLEFT);
            }
            if (owner._stateFlame == 60)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.WING, (int)footSmokePosition.WINGLEFT);
            }
        }
    }
}



