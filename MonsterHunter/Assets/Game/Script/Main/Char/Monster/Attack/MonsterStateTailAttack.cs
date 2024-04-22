/*尻尾攻撃*/

public partial class MonsterState
{
    public class MonsterStateTailAttack : StateBase
    {
        // 攻撃判定発生タイミング.
        private const int _spawnColTiming = 30;
        // 攻撃判定消去タイミング.
        private const int _eraseColTiming = 170;
        // SEを鳴らすタイミング.
        private float[] _sePlayTiming = new float[2];
        // SEを鳴らすフラグ.
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
            // 攻撃判定生成.
            if(owner._stateFlame == _spawnColTiming) 
            {
                for(int colNum = 0; colNum < owner._tailCollisiton.Length; colNum++)
                {
                    owner._tailCollisiton[colNum].SetActive(true);
                }
            }
            // 攻撃判定消去.
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

        // パーティクルをモーションを行っている時間で生成する.
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


