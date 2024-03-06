/*気刃大回転斬り*/

public partial class PlayerState
{
    public class StateRoundSlash : StateBase
    {
        // 攻撃判定発生タイミング.
        private const float _spawnColTiming = 0.4f;
        // 攻撃判定消去タイミング.
        private const float _eraseColTiming = 0.6f;
        // 前進させるタイミング.
        private float[] _forwardStopTiming = new float[2];
        // 移動力.
        private float[] _speedPower = new float[2];
        // SEを鳴らすタイミング.
        private const float _sePlayTiming = 0.18f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritRoundSlash = true;
            owner._nextMotionFlame = 120;
            owner._deceleration = 0.9f;
            owner.StateTransitionInitialization();
            owner._attackPower = 150;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 20;
            owner._hitStopTime = 0.3f;
            owner._attackCol._isOneProcess = true;
            owner._isAttackProcess = false;
            owner._nextMotionTime = 2.0f;

            _forwardStopTiming[0] = 0.18f;
            _forwardStopTiming[1] = 0.88f;

            _speedPower[0] = 15;
            _speedPower[1] = 8;
        }

        public override void OnUpdate(PlayerState owner)
        {
            // 攻撃判定発生.
            if (owner._stateTime >= _spawnColTiming && !owner._isAttackProcess)
            {
                owner._weaponActive = true;
                owner._isAttackProcess = true;
            }
            // 攻撃判定消去.
            if (owner._stateTime >= _eraseColTiming)
            {
                owner._weaponActive = false;
            }
            // 前進させる.
            if (owner._stateTime <= _forwardStopTiming[0])
            {
                owner.ForwardStep(_speedPower[0]);
            }
            else if (owner._stateTime >= _forwardStopTiming[1])
            {
                owner.ForwardStep(_speedPower[1]);
            }

            // 空振り効果音再生.
            owner.SEPlayTest(_sePlayTiming, (int)SEManager.HunterSE.MISSINGROUNDSLASH);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritRoundSlash = false;
            owner._unsheathedSword = false;
        }

        public override void OnChangeState(PlayerState owner)
        {

            // 次の状態遷移を起こすタイミング.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.ROUNDSLASH]) return;

            // 納刀待機状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.IDLE], _idle);
            // 移動状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RUN], _running);
            // ダッシュ状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DASH], _dash);
        }
    }
}