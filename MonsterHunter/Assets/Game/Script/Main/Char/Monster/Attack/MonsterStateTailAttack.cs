/*�K���U��*/

public partial class MonsterState
{
    public class MonsterStateTailAttack : StateBase
    {
        // �U�����蔭���^�C�~���O.
        private const int _spawnColTiming = 30;
        // �U����������^�C�~���O.
        private const int _eraseColTiming = 170;
        // SE��炷�^�C�~���O.
        private float[] _sePlayTiming = new float[2];
        // SE��炷�t���O.
        private float _playOneShotResetTiming = 1.5f;

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._tailMotion = true;
            if (owner._huntingSceneManager._hitPointMany)
            {
                owner._AttackPower = 13;
            }
            else
            {
                owner._AttackPower = 8;
            }

            _sePlayTiming[0] = 1;
            _sePlayTiming[1] = 2;
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(_sePlayTiming[0], (int)SEManager.MonsterSE.FOOTSTEP);
            owner.PlayOneShotReset(_playOneShotResetTiming);
            owner.SEPlay(_sePlayTiming[1], (int)SEManager.MonsterSE.FOOTSTEP);
        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            // �U�����萶��.
            if(owner._stateFlame == _spawnColTiming) 
            {
                for(int colNum = 0; colNum < owner._tailCollisiton.Length; colNum++)
                {
                    owner._tailCollisiton[colNum].SetActive(true);
                }
            }
            // �U���������.
            else if (owner._stateFlame == _eraseColTiming)
            {
                for (int colNum = 0; colNum < owner._tailCollisiton.Length; colNum++)
                {
                    owner._tailCollisiton[colNum].SetActive(false);
                }
            }
            ParticleGenerateTime(owner);
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._tailMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if (owner._stateTime >= owner._stateTransitionTime[(int)StateTransitionKinds.TAIL])
            {
                owner.ChangeState(_idle);
            }
        }

        // �p�[�e�B�N�������[�V�������s���Ă��鎞�ԂŐ�������.
        private void ParticleGenerateTime(MonsterState owner)
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


