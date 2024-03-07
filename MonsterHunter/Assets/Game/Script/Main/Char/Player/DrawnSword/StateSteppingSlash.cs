/*踏み込み斬り*/

using UnityEngine;

public partial class PlayerState
{
    public class StateSteppingSlash : StateBase
    {
        // 攻撃判定発生タイミング.
        private const float _spawnColTiming = 0.9f;
        // 攻撃判定消去タイミング.
        private const float _eraseColTiming = 1.1f;
        // 前進させるタイミング.
        private const float _forwardStopTiming = 1.1f;
        // 前進終了タイミング.
        private const float _forwardTiming = 0.1f;
        // 移動力.
        private const float _speedPower = 8;
        // 減速力.
        private const float _decelerationPower = 0.8f;
        // SEを鳴らすタイミング.
        private const float _sePlayTiming = 0.72f;
        // モーションキャンセル適応外の状態に遷移するタイミング.
        private const float _TransitionTime = 2.5f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSteppingSlash = true;
            owner._rigidbody.velocity = Vector3.zero;
            owner._unsheathedSword = true;
            owner.StateTransitionInitialization();
            owner._isCauseDamage = true;
            owner._attackPower = 81;
            owner._increaseAmountRenkiGauge = 7;
            owner._hitStopTime = 0.1f;
            owner._attackCol._isOneProcess = true;
            owner._isAttackProcess = false;
        }

        public override void OnUpdate(PlayerState owner)
        {
            // 攻撃判定の発生.
            if (owner._stateTime >= _spawnColTiming && !owner._isAttackProcess)
            {
                owner._weaponActive = true;
                owner._isAttackProcess = true;
            }

            // 前進させる.
            if (owner._stateTime <= _forwardStopTiming && owner._stateTime >= _forwardTiming)
            {
                owner.ForwardStep(_speedPower);
            }
            // 減速させる.
            else
            {
                owner._rigidbody.velocity *= _decelerationPower;
            }
            
            // 攻撃判定消去.
            if (owner._stateTime >= _eraseColTiming)
            {
                owner._weaponActive = false;
            }

            // 空振り効果音再生.
            //owner.SEPlay(60, (int)SEManager.HunterSE.MISSINGSLASH);
            owner.SEPlayTest(_sePlayTiming, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSteppingSlash = false;
            owner._weaponActive = false;
            owner._isAttackProcess = false;
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
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.STEPPINGSLASH]) return;

            // 前回避.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWAVOID], _avoidDrawnSword);
            // 右回避.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RIGHTAVOID], _rightAvoid);
            // 左回避.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.LEFTAVOID], _leftAvoid);
            // 後ろ回避.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.BACKAVOID], _backAvoid);
            // 必殺技の構え
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSTANCE], _stance);
            // 突き.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.PRICK] || owner._input._YButtonDown, _prick);
            // 気刃斬り1.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1], _spiritBlade1);
        }
    }
}