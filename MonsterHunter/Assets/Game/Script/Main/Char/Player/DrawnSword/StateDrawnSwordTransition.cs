/*アイドル状態から抜刀状態への移行*/

using UnityEngine;

public partial class PlayerState
{
    public class StateDrawnSwordTransition : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSwordMotion = true;
            owner._unsheathedSword = true;
            owner._motionFrame = 0;
        }

        public override void OnUpdate(PlayerState owner)
        {
            owner._rigidbody.velocity *= 0.8f;
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            owner._motionFrame++;

            // 前進させる.
            if (owner._motionFrame <= 20&& owner._motionFrame >= 5)
            {
                owner.ForwardStep(5);
            }
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSwordMotion = false;

            
        }

        public override void OnChangeState(PlayerState owner)
        {
            // デバッグ用
            if(owner._motionFrame >= 60)
            {
                owner.ChangeState(_idleDrawnSword);
            }


        }


    }
}