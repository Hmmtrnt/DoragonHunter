/*気刃斬り2*/

using UnityEngine;
using UnityEngine.Rendering;

public partial class PlayerState
{
    public class StateSpiritBlade2 : StateBase
    {
        // 攻撃判定発生タイミング.
        private const float _spawnColTiming = 0.45f;
        // 攻撃判定消去タイミング.
        private const float _eraseColTiming = 0.6f;
        // SEを鳴らすタイミング.
        private const float _sePlayTiming = 0.3f;
        // モーションキャンセル適応外の状態に遷移するタイミング.
        private const float _TransitionTime = 2;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritBlade2 = true;
            owner._nextMotionFlame = 55;
            owner.StateTransitionInitialization();
            owner._attackPower = 114;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 10;
            owner._hitStopTime = 0.1f;
            owner._attackCol._isOneProcess = true;
            owner._isAttackProcess = false;
            owner._nextMotionTime = 0.65f;
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

            // 空振り効果音再生.
            owner.SEPlayTest(_sePlayTiming, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritBlade2 = false;
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
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.SPIRITBLADE2]) return;

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
            // 気刃斬り3状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE3], _spiritBlade3);
        }
    }
}


