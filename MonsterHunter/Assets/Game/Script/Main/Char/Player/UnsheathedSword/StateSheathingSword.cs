/*納刀状態に遷移*/

using System.Buffers;
using UnityEngine;

public partial class Player
{
    public class StateSheathingSword : StateBase
    {
        // デバッグ用変数
        private int MotionTransition = 0;
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnSheathingSword = true;
            owner._unsheathedSword = false;
            MotionTransition = 0;
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(Player owner)
        {

        }

        public override void OnFixedUpdate(Player owner)
        {
            MotionTransition++;
            if(owner._stateFlame <= 10)
            {
                owner.ForwardStep(10);
            }

            // 納刀効果音再生.
            owner.SEPlay(10,(int)SEManager.HunterSE.SHEATHINGSWORD);
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnSheathingSword = false;
            MotionTransition = 0;
        }

        public override void OnChangeState(Player owner)
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
    }
}


