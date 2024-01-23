/*納刀状態に遷移*/

using System.Buffers;
using UnityEngine;

public partial class PlayerState
{
    public class StateSheathingSword : StateBase
    {
        // デバッグ用変数
        private int MotionTransition = 0;
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSheathingSword = true;
            owner._unsheathedSword = false;
            MotionTransition = 0;
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            MotionTransition++;
            if(owner._stateFlame <= 10)
            {
                owner.ForwardStep(10);
            }

            SheathingSwordSound(owner);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSheathingSword = false;
            MotionTransition = 0;
        }

        public override void OnChangeState(PlayerState owner)
        {
            if (MotionTransition <= 25) return;
            if((owner._input._LeftStickHorizontal != 0 || owner._input._LeftStickVertical != 0) && owner._input._RBButton)
            {
                owner.ChangeState(_dash);
            }
            else if((owner._input._LeftStickHorizontal != 0 || owner._input._LeftStickVertical != 0) && !owner._input._RBButton)
            {
                owner.ChangeState(_running);
            }
            else
            {
                owner.ChangeState(_idle);
            }
        }

        // 納刀するときの音を再生.
        private void SheathingSwordSound(PlayerState owner)
        {
            if (owner._stateFlame == 10)
            {
                owner._seManager.HunterPlaySE((int)SEManager.HunterSE.SHEATHINGSWORD);
            }
        }
    }
}


