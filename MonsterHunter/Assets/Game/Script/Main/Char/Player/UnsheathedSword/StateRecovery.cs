/*回復*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRecovery : StateBase
    {
        // 回復時間
        private int _recoveryTime = 0;
        // 一度処理を通したら次から通さない.
        //HACK:変数名を変更.
        private bool _test = false;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._isRecovery = true;
            owner._healMotion = true;
            _test = false;
        }

        public override void OnUpdate(PlayerState owner)
        {
            owner._rigidbody.velocity *= 0.8f;
            owner._currentRecoveryTime++;
            _recoveryTime++;
            // 回復薬を減らす.
            if (owner._stateTime >= 1.2f && !_test)
            {
                owner._cureMedicineNum--;
                _test = true;
            }
            // 回復するタイミング指定.
            if (owner._stateTime >= 1.2f && owner._stateTime <= 2.3f)
            {
                Recovery(owner);
            }
            // ごくごく音.
            owner.SEPlayTest(0.96f, (int)SEManager.HunterSE.DRINK);

        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._isRecovery = false;
            owner._currentRecoveryTime = 0;
            _recoveryTime = 0;
            owner._healMotion = false;
            _test = false;
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

            if(owner._currentHitPoint >= owner._maxHitPoint)
            {
                owner._currentHitPoint = owner._maxHitPoint;
            }
        }
    }

}