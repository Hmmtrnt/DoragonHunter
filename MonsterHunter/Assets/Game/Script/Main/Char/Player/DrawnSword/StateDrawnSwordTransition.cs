/*アイドル状態から抜刀状態への移行*/

using System.Buffers;
using UnityEngine;

public partial class Player
{
    public class StateDrawnSwordTransition : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnSwordMotion = true;
            owner._unsheathedSword = true;
            owner._motionFrame = 0;
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(Player owner)
        {
            owner._rigidbody.velocity *= 0.8f;
        }

        public override void OnFixedUpdate(Player owner)
        {
            owner._motionFrame++;

            // 前進させる.
            if (owner._motionFrame <= 20&& owner._motionFrame >= 5)
            {
                owner.ForwardStep(5);
            }

            // 抜刀効果音再生.
            owner.SEPlay(10, (int)SEManager.HunterSE.DRAWSWORD);
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnSwordMotion = false;
        }

        public override void OnChangeState(Player owner)
        {
            // デバッグ用
            if(owner._motionFrame >= 60)
            {
                owner.ChangeState(_idleDrawnSword);
            }
        }
    }
}