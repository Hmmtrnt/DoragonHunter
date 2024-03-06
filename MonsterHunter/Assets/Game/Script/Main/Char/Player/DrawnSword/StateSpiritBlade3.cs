/*気刃斬り3*/

public partial class PlayerState
{
    public class StateSpiritBlade3 : StateBase
    {
        // 攻撃判定発生タイミング.
        private float[] _spawnColTiming = new float[3];
        // 攻撃判定発生の処理の終了タイミング.
        private float[] _spawnEndColTiming = new float[3];
        // 攻撃判定消去タイミング.
        private float[] _eraseColTiming = new float[3];
        // 攻撃判定消去の処理の終了タイミング.
        private float[] _eraseEndColTiming = new float[2];
        // 前進させるタイミング.
        private const float _forwardStopTiming = 1;
        // 移動力.
        private const float _speedPower = 4;
        // SEを鳴らすタイミング.
        private float[] _sePlayTiming = new float[3];
        // SEを鳴らすフラグ.
        private float[] _playOneShotResetTiming = new float[2];

        // モーションキャンセル適応外の状態に遷移するタイミング.
        private const float _TransitionTime = 2.1f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritBlade3 = true;
            owner._nextMotionFlame = 130;
            owner.StateTransitionInitialization();
            owner._attackPower = 40;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 5;
            owner._hitStopTime = 0.05f;
            owner._attackCol._isOneProcess = true;
            owner._isAttackProcess = false;
            owner._nextMotionTime = 1.6f;

            _spawnColTiming[0] = 0.15f;
            _spawnColTiming[1] = 0.3f;
            _spawnColTiming[2] = 1.25f;

            _spawnEndColTiming[0] = 0.25f;
            _spawnEndColTiming[1] = 0.7f;
            _spawnEndColTiming[2] = 1.6f;

            _eraseColTiming[0] = 0.26f;
            _eraseColTiming[1] = 0.75f;
            _eraseColTiming[2] = 1.65f;

            _eraseEndColTiming[0] = 0.28f;
            _eraseEndColTiming[1] = 0.8f;

            _sePlayTiming[0] = 0.12f;
            _sePlayTiming[1] = 0.36f;
            _sePlayTiming[2] = 1.0f;
            _playOneShotResetTiming[0] = 0.2f;
            _playOneShotResetTiming[1] = 0.4f;
        }

        public override void OnUpdate(PlayerState owner)
        {
            // 一撃目.
            if ((owner._stateTime >= _spawnColTiming[0] && owner._stateTime <= _spawnEndColTiming[0]) && !owner._isAttackProcess)
            {
                owner._weaponActive = true;
                owner._isAttackProcess = true;
            }
            if ((owner._stateTime >= _eraseColTiming[0] && owner._stateTime <= _eraseEndColTiming[0]) && owner._isAttackProcess)
            {
                owner._weaponActive = false;
                owner._isAttackProcess = false;
            }

            // 二撃目.
            if ((owner._stateTime >= _spawnColTiming[1] && owner._stateTime <= _spawnEndColTiming[1]) && !owner._isAttackProcess)
            {
                owner._isCauseDamage = true;
                owner._weaponActive = true;
                owner._attackCol._isOneProcess = true;
                owner._isAttackProcess = true;

            }
            if ((owner._stateTime >= _eraseColTiming[1] && owner._stateTime <= _eraseEndColTiming[1]) && owner._isAttackProcess)
            {
                owner._weaponActive = false;
                owner._isAttackProcess = false;
            }

            // 三撃目.
            if ((owner._stateTime >= _spawnColTiming[2] && owner._stateTime <= _spawnEndColTiming[2]) && !owner._isAttackProcess)
            {
                owner._isCauseDamage = true;
                owner._weaponActive = true;
                owner._attackCol._isOneProcess = true;
                owner._attackPower = 100;
                owner._isAttackProcess = true;
            }

            if (owner._stateTime >= _eraseColTiming[2] && owner._isAttackProcess)
            {
                owner._weaponActive = false;
                owner._isAttackProcess = false;
            }

            if (owner._stateTime <= _forwardStopTiming)
            {
                owner.ForwardStep(_speedPower);
            }

            // 空振り効果音再生.
            owner.SEPlayTest(_sePlayTiming[0], (int)SEManager.HunterSE.MISSINGSLASH);
            owner.PlayOneShotReset(_playOneShotResetTiming[0]);
            owner.SEPlayTest(_sePlayTiming[1], (int)SEManager.HunterSE.MISSINGSLASH);
            owner.PlayOneShotReset(_playOneShotResetTiming[1]);
            owner.SEPlayTest(_sePlayTiming[2], (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritBlade3 = false;
            owner._weaponActive = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // モーションキャンセル適応外の遷移先.
            if (owner._stateTime >= _TransitionTime)
            {
                // 抜刀待機状態.
                owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWIDLE], _idleDrawnSword);
                // 抜刀移動状態.
                owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWRUN], _runDrawnSword);
            }

            // 次の状態遷移を起こすタイミング.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.SPIRITBLADE3]) return;

            // 回避状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWAVOID], _avoidDrawnSword);
            // 右回避状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RIGHTAVOID], _rightAvoid);
            // 左回避状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.LEFTAVOID], _leftAvoid);
            // 後ろ回避状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.BACKAVOID], _backAvoid);
            // 必殺技の構え状態
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSTANCE], _stance);
            // 気大回転斬り状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.ROUNDSLASH], _roundSlash);
        }
    }
}