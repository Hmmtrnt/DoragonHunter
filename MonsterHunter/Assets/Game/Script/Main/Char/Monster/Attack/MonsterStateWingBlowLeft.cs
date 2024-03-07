/*左翼攻撃*/

public partial class MonsterState
{
    public class MonsterStateWingBlowLeft : StateBase
    {
        // 攻撃判定発生タイミング.
        private const int _spawnColTiming = 40;
        // 攻撃判定消去タイミング.
        private const int _eraseColTiming = 100;
        // SEを鳴らすタイミング.
        private float[] _sePlayTiming = new float[2];
        // SEを鳴らすフラグ.
        private float _playOneShotResetTiming = 0.5f;

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
            _sePlayTiming[0] = 0.4f;
            _sePlayTiming[1] = 1.1f;
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(_sePlayTiming[0], (int)SEManager.MonsterSE.FOOTSTEP);
            owner.PlayOneShotReset(_playOneShotResetTiming);
            owner.SEPlay(_sePlayTiming[1], (int)SEManager.MonsterSE.ROTATE);
        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            if(owner._stateFlame == _spawnColTiming)
            {
                owner._wingLeftCollisiton.SetActive(true);
            }
            else if(owner._stateFlame == _eraseColTiming)
            {
                owner._wingLeftCollisiton.SetActive(false);
            }

            ParticleGenerateTime(owner);
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._wingLeftMotion = false;
            owner._currentWingAttackLeft = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateTime >= owner._stateTransitionTime[(int)StateTransitionKinds.WINGBLOWRIGHT])
            {
                owner.ChangeState(_idle);
            }
        }

        // パーティクルをモーションを行っている時間で生成する.
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



