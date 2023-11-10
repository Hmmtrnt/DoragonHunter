/*納刀状態に遷移*/

using UnityEngine;

public partial class PlayerState
{
    public class StateSheathingSword : StateBase
    {
        // デバッグ用変数
        private int MotionTransition = 0;
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSwordMotion = false;
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
        }

        public override void OnChangeState(PlayerState owner)
        {
            if(MotionTransition >= 60)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


