/*アイドル状態から抜刀状態への移行*/

using UnityEngine;

public partial class PlayerState
{
    public class StateDrawnSwordTransition : StateBase
    {
        // デバッグ用変数
        private int MotionTransition = 0;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSwordMotion = true;
            owner._unsheathedSword = true;
            MotionTransition = 0;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            MotionTransition++;
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSwordMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // デバッグ用
            if(MotionTransition >= 60)
            {
                owner.ChangeState(_idleDrawnSword);
            }


        }


    }
}