/*斬り上げ*/

public partial class PlayerState
{
    public class StateSlashUp : StateBase
    {
        // 攻撃判定発生タイミング.
        private const float _spawnColTiming = 0.15f;
        // 攻撃判定消去タイミング.
        private const float _eraseColTiming = 0.45f;
        // 前進させるタイミング.
        private const float _forwardStopTiming = 0.1f;
        // 移動力.
        private const float _speedPower = 3.5f;
        // SEを鳴らすタイミング.
        private const float _sePlayTiming = 0.12f;
        // モーションキャンセル適応外の状態に遷移するタイミング.
        private const float _TransitionTime = 2.1f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSlashUp = true;
            owner.StateTransitionInitialization();
            owner._attackPower = 73;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 5;
            owner._hitStopTime = 0.05f;
            owner._attackCol._isOneProcess = true;
            owner._isAttackProcess = false;
        }

        public override void OnUpdate(PlayerState owner)
        {
            // 攻撃判定を生成.
            if (owner._stateTime >= _spawnColTiming && !owner._isAttackProcess)
            {
                owner._isAttackProcess = false;
                owner._weaponActive = true;
            }
            // 攻撃判定を消去.
            if (owner._stateTime >= _eraseColTiming)
            {
                owner._weaponActive = false;
            }

            // 前進させる.
            if (owner._stateTime <= _forwardStopTiming)
            {
                owner.ForwardStep(_speedPower);
            }
            // 空振り効果音再生.
            owner.SEPlay(_sePlayTiming, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSlashUp = false;
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
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.SLASHUP]) return;

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
            // 突き状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.PRICK], _prick);
            // 気刃斬り1状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1], _spiritBlade1);
        }
    }
}

