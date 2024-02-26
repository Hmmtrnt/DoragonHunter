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
            MotionTransition++;
            if (owner._stateTime <= 0.33f)
            {
                owner.ForwardStep(10);
            }
            // 納刀効果音再生.
            owner.SEPlay(15, (int)SEManager.HunterSE.SHEATHINGSWORD);
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            

            
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSheathingSword = false;
            MotionTransition = 0;
        }

        public override void OnChangeState(PlayerState owner)
        {
            if (owner._stateTime <= 0.5f) return;
            if((owner._input._LeftStickHorizontal != 0 || owner._input._LeftStickVertical != 0) && owner._input._RBButton)
            {
                owner.StateTransition(_dash);
            }
            else if((owner._input._LeftStickHorizontal != 0 || owner._input._LeftStickVertical != 0) && !owner._input._RBButton)
            {
                owner.StateTransition(_running);
            }
            else
            {
                owner.StateTransition(_idle);
            }
        }
    }
}


