/*回復*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRecovery : StateBase
    {
        // 回復薬を減らしたかどうか.
        private bool _medicineConsume = false;
        // 回復薬を減らすタイミング.
        private const float _medicineConsumeTiming = 1.2f;
        // 回復終了タイミング.
        private const float _recoveryFinishTiming = 2.3f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._healMotion = true;
        }

        public override void OnUpdate(PlayerState owner)
        {
            owner._rigidbody.velocity *= 0.8f;
            // 回復薬を減らす.
            if (owner._stateTime >= _medicineConsumeTiming && !_medicineConsume)
            {
                owner._cureMedicineNum--;
                _medicineConsume = true;
            }
            // 回復するタイミング指定.
            if (owner._stateTime >= _medicineConsumeTiming && owner._stateTime <= _recoveryFinishTiming)
            {
                Recovery(owner);
            }
            // ごくごく音.
            owner.SEPlayTest(0.96f, (int)SEManager.HunterSE.DRINK);

        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._healMotion = false;
            _medicineConsume = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 回避状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.AVOID], _avoid);

            // 次の状態遷移を起こすタイミング.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.RECOVERY])
            {
                return;
            }

            // 待機状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.IDLE], _idle);
            // 走る状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RUN], _running);
            // ダッシュ状態.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DASH], _dash);
        }

        // 回復.
        private void Recovery(PlayerState owner)
        {
            owner._currentHitPoint += owner._recoveryAmount;

            // 体力を上限突破しないようにする.
            if(owner._currentHitPoint >= owner._maxHitPoint)
            {
                owner._currentHitPoint = owner._maxHitPoint;
            }
        }
    }

}