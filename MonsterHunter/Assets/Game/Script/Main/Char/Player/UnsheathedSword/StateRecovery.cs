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
            owner.SEPlay(80, (int)SEManager.HunterSE.DRINK);
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
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
            // 状態遷移ができるかどうか
            //bool isChange = owner._currentRecoveryTime >= owner._maxRecoveryTime;
            bool isChange = owner._stateTime >= 3.3f;
            // 動いているかどうか
            bool isMove = owner._leftStickHorizontal != 0 || owner._leftStickVertical != 0;

            // アイドル状態へ
            if (isChange && !isMove)
            {
                owner.StateTransition(_idle);
            }
            else if(isChange && isMove)
            {
                owner.StateTransition(_running);
            }
            else if(owner._input._AButtonDown)
            {
                owner.StateTransition(_avoid);
            }
        }

        // 回復.
        private void Recovery(PlayerState owner)
        {
            owner._hitPoint += owner._recoveryAmount;

            if(owner._hitPoint >= owner._maxHitPoint)
            {
                owner._hitPoint = owner._maxHitPoint;
            }
        }
    }

}