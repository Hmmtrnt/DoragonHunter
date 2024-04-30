/*噛みつき*/

public partial class MonsterState
{
    public class MonsterStateBite : StateBase
    {
        // 回転スピード.
        private const int _rotateSpeed = 40;
        // 攻撃判定発生タイミング.
        private const int _spawnColTiming = 40;
        // 攻撃判定消去タイミング.
        private const int _eraseColTiming = 70;
        // 前進させるタイミング.
        private const int _forwardStopTiming = 1;
        // 前進終了タイミング.
        private const int _forwardTiming = 15;
        // 移動力.
        private const float _speedPower = 0.4f;
        // SEを鳴らすタイミング.
        private float _sePlayTiming = 0.6f;

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._biteMotion = true;
            if (owner._huntingSceneManager._hitPointMany)
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
            owner.SEPlay(_sePlayTiming, (int)SEManager.MonsterSE.BITE);
        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            owner.TurnTowards(_rotateSpeed);

            if(owner._stateFlame >= _spawnColTiming && owner._stateFlame <= _eraseColTiming)
            {
                owner._biteCollisiton.SetActive(true);
            }
            else
            {
                owner._biteCollisiton.SetActive(false);
            }

            if(owner._stateFlame == _forwardStopTiming)
            {
                owner._forwardSpeed = _speedPower;
            }
            else if(owner._stateFlame == _forwardTiming)
            {
                owner._forwardSpeed = 0.0f;
            }

            owner._transform.position += owner._moveVelocity;
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._biteMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if (owner._stateTime >= owner._stateTransitionTime[(int)StateTransitionKinds.BITE])
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


