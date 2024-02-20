/*回復*/

using UnityEngine;

public partial class Player
{
    public class StateRecovery : StateBase
    {
        // 回復時間
        private int _recoveryTime = 0;

        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._isRecovery = true;
            owner._healMotion = true;
        }

        public override void OnUpdate(Player owner)
        {
            owner._rigidbody.velocity *= 0.8f;
            owner._currentRecoveryTime++;
            _recoveryTime++;
            // 回復薬を減らす.
            if (_recoveryTime == 80)
            {
                owner._cureMedicineNum--;
            }
            // 回復するタイミング指定.
            if (_recoveryTime >= 80 && _recoveryTime <= 180)
            {
                Recovery(owner);
            }
            // ごくごく音.
            owner.SEPlay(80, (int)SEManager.HunterSE.DRINK);
        }

        public override void OnFixedUpdate(Player owner)
        {
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._isRecovery = false;
            owner._currentRecoveryTime = 0;
            _recoveryTime = 0;
            owner._healMotion = false;
        }

        public override void OnChangeState(Player owner)
        {
            // 状態遷移ができるかどうか
            bool isChange = owner._currentRecoveryTime >= owner._maxRecoveryTime;
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
        private void Recovery(Player owner)
        {
            owner._hitPoint += owner._recoveryAmount;

            if(owner._hitPoint >= owner._maxHitPoint)
            {
                owner._hitPoint = owner._maxHitPoint;
            }
        }
    }

}