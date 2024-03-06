/*アイドル状態から抜刀状態への移行*/

using System.Buffers;
using UnityEngine;

public partial class PlayerState
{
    public class StateDrawnSwordTransition : StateBase
    {
        // 前進させるスピード.
        private const float _forwardSpeed = 0.8f;


        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSwordMotion = true;
            owner._unsheathedSword = true;
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(PlayerState owner)
        {
            owner._rigidbody.velocity *= _forwardSpeed;

            owner._motionFrame++;

            // 前進させる.
            if (owner._stateTime >= 0.2f && owner._stateTime <= 0.5f)
            {
                owner.ForwardStep(5.5f);
            }

            // 抜刀効果音再生.
            //owner.SEPlay(20, (int)SEManager.HunterSE.DRAWSWORD);
            owner.SEPlayTest(0.24f, (int)SEManager.HunterSE.DRAWSWORD);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSwordMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 待機状態.
            if(owner._stateTime >= 1.3f)
            {
                if(owner._input._LeftStickHorizontal != 0 || owner._input._LeftStickVertical != 0)
                {
                    owner.StateTransition(_runDrawnSword);
                }
                else if(owner._input._LeftStickHorizontal == 0 && owner._input._LeftStickVertical == 0)
                {
                    owner.StateTransition(_idleDrawnSword);
                }
            }
        }
    }
}