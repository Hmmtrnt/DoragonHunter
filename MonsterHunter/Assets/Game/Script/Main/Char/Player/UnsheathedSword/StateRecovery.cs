/*回復*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRecovery : StateBase
    {
        // 回復時間
        private int _recoveryTime = 0;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._isRecovery = true;
            owner._healMotion = true;
        }

        public override void OnUpdate(PlayerState owner)
        {
            
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            owner._rigidbody.velocity *= 0.8f;
            owner._currentRecoveryTime++;
            _recoveryTime++;

            if(_recoveryTime >= 50 && _recoveryTime <=110)
            {
                Recovery(owner);
            }
            owner.SEPlay(50, (int)SEManager.HunterSE.DRINK);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._isRecovery = false;
            owner._currentRecoveryTime = 0;
            _recoveryTime = 0;
            owner._healMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 状態遷移ができるかどうか
            bool isChange = owner._currentRecoveryTime >= owner._maxRecoveryTime;
            // 動いているかどうか
            bool isMove = owner._leftStickHorizontal != 0 || owner._leftStickVertical != 0;

            // アイドル状態へ
            if (isChange && !isMove)
            {
                owner.ChangeState(_idle);
            }
            else if(isChange && isMove)
            {
                owner.ChangeState(_running);
            }
            else if(owner._input._AButtonDown)
            {
                owner.ChangeState(_avoid);
            }
        }

        // 回復.
        private void Recovery(PlayerState owner)
        {
            //if (owner._hitPoint >= owner._maxHitPoint) return;

            owner._hitPoint += owner._recoveryAmount;

            if(owner._hitPoint >= owner._maxHitPoint)
            {
                owner._hitPoint = owner._maxHitPoint;
            }
        }
    }

}