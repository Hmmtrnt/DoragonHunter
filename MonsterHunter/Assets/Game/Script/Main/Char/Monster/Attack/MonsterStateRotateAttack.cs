/*回転攻撃*/

public partial class MonsterState
{
    public class MonsterStateRotateAttack : StateBase
    {
        // 攻撃判定発生タイミング.
        private const int _spawnColTiming = 90;
        // 攻撃判定消去タイミング.
        private const int _eraseColTiming = 135;
        // SEを鳴らすタイミング.
        private float[] _sePlayTiming = new float[3];
        // SEを鳴らすフラグ.
        private float[] _playOneShotResetTiming = new float[2];

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._rotateMotion = true;
            owner._currentRotateAttack = true;
            
            if(owner._huntingSceneManager._hitPointMany)
            {
                owner._AttackPower = 15;
            }
            else
            {
                owner._AttackPower = 10;
            }

            _sePlayTiming[0] = 0.4f;
            _sePlayTiming[1] = 0.5f;
            _sePlayTiming[2] = 1.8f;
            _playOneShotResetTiming[0] = 0.45f;
            _playOneShotResetTiming[1] = 1.7f;
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(_sePlayTiming[0], (int)SEManager.MonsterSE.GROAN);
            owner.PlayOneShotReset(_playOneShotResetTiming[0]);
            owner.SEPlay(_sePlayTiming[1], (int)SEManager.MonsterSE.FOOTSMALLSTEP);
            owner.PlayOneShotReset(_playOneShotResetTiming[1]);
            owner.SEPlay(_sePlayTiming[2], (int)SEManager.MonsterSE.ROTATE);
        }


        public override void OnFixedUpdate(MonsterState owner)
        {
            if(owner._stateFlame == _spawnColTiming)
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
            else if(owner._stateFlame == _eraseColTiming)
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
            if(owner._stateTime >= owner._stateTransitionTime[(int)StateTransitionKinds.ROTATE])
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


