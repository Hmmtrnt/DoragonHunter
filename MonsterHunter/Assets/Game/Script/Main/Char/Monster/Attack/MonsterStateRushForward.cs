/*突進攻撃*/

using System.Buffers;
using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateRushForward : StateBase
    {
        // 回転スピード.
        private const int _rotateSpeed = 40;
        // 攻撃判定発生タイミング.
        private const int _spawnColTiming = 65;
        // 攻撃判定消去タイミング.
        private const int _eraseColTiming = 130;
        // 前進させるタイミング.
        private int[] _forwardStopTiming = new int[2];
        // 移動力.
        private float[] _speedPower = new float[2];
        // 減速するタイミング.
        private const int _slowDownStart = 60;
        // 減速を終了するタイミング.
        private const int _slowDownFinish = 140;
        // 減速力.
        private const float _slowDownPower = 0.001f;
        // SEを鳴らすタイミング.
        private float[] _sePlayTiming = new float[5];
        // SEを鳴らすフラグ.
        private float[] _playOneShotResetTiming = new float[4];

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._rushMotion = true;
            if (owner._mainSceneManager._hitPointMany)
            {
                owner._AttackPower = 22;
            }
            else
            {
                owner._AttackPower = 17;
            }
            _forwardStopTiming[0] = 60;
            _forwardStopTiming[1] = 100;
            _speedPower[0] = 0.7f;
            _speedPower[1] = 0.15f;

            _sePlayTiming[0] = 1.1f;
            _sePlayTiming[1] = 1.5f;
            _sePlayTiming[2] = 1.7f;
            _sePlayTiming[3] = 2.1f;
            _sePlayTiming[4] = 2.5f;
            _playOneShotResetTiming[0] = 1.4f;
            _playOneShotResetTiming[1] = 1.6f;
            _playOneShotResetTiming[2] = 2.0f;
            _playOneShotResetTiming[3] = 2.4f;
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(_sePlayTiming[0], (int)SEManager.MonsterSE.FOOTSTEP);
            owner.PlayOneShotReset(_playOneShotResetTiming[0]);
            owner.SEPlay(_sePlayTiming[1], (int)SEManager.MonsterSE.FOOTSTEP);
            owner.PlayOneShotReset(_playOneShotResetTiming[1]);
            owner.SEPlay(_sePlayTiming[2], (int)SEManager.MonsterSE.FOOTSTEP);
            owner.PlayOneShotReset(_playOneShotResetTiming[2]);
            owner.SEPlay(_sePlayTiming[3], (int)SEManager.MonsterSE.FOOTSTEP);
            owner.PlayOneShotReset(_playOneShotResetTiming[3]);
            owner.SEPlay(_sePlayTiming[4], (int)SEManager.MonsterSE.FOOTSTEP);
        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            owner.TurnTowards(_rotateSpeed);

            if (owner._stateFlame == _forwardStopTiming[0])
            {
                owner._forwardSpeed = _speedPower[0];
            }
            else if (owner._stateFlame == _forwardStopTiming[1])
            {
                owner._forwardSpeed = _speedPower[1];
            }

            // 減速.
            if(owner._stateFlame >= _slowDownStart && owner._stateFlame <= _slowDownFinish)
            {
                owner._forwardSpeed -= _slowDownPower;
            }

            if(owner._stateFlame == _spawnColTiming)
            {
                owner._rushCollisiton.SetActive(true);
                owner._wingRightCollisiton.SetActive(true);
                owner._wingLeftCollisiton.SetActive(true);
            }
            else if(owner._stateFlame == _eraseColTiming)
            {
                owner._rushCollisiton.SetActive(false);
                owner._wingRightCollisiton.SetActive(false);
                owner._wingLeftCollisiton.SetActive(false);
            }

            ParticleGenerateTime(owner);

            owner._trasnform.position += owner._moveVelocity;
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._rushMotion = false;
            // スピードを0にする.
            owner._forwardSpeed = 0.0f;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateTime >= 3)
            {
                owner.ChangeState(_idle);
            }
        }

        // パーティクルをモーションを行っている時間で生成する.
        private void ParticleGenerateTime(MonsterState owner)
        {
            if(owner._stateFlame == 60 || owner._stateFlame == 90 || owner._stateFlame == 130)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.LEG, (int)footSmokePosition.WINGRIGHT);
            }
            if(owner._stateFlame == 80 || owner._stateFlame == 110)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.LEG, (int)footSmokePosition.WINGLEFT);
            }
        }
    }
}