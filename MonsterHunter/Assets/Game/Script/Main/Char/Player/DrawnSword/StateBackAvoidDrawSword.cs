/*�����̌����*/

using UnityEngine;

public partial class PlayerState
{
    public class StateBackAvoidDrawSword : StateBase
    {
        // ��ԑJ�ڂ��s���ۂɏ������炵�ď�����ʂ����߂̕ϐ�.
        private const float _delayTransition = 0.3f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnBackAvoidMotion = true;
            owner._isProcess = true;
            owner._avoidVelocity = -owner._transform.forward * owner._avoidVelocityMagnification;
            owner._nextMotionFlame = 100;
            owner._deceleration = 0.9f;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnUpdate(PlayerState owner)
        {
            owner._avoidTime++;
            owner.MoveAvoid();

        }
        
        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnBackAvoidMotion = false;
            owner._avoidTime = 0;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // ���̏�ԑJ�ڂ��N�����^�C�~���O.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.BACKAVOID]) return;

            // �����ړ����.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWRUN], _runDrawnSword);

            // ���̏�ԑJ�ڂ��N�����^�C�~���O.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.BACKAVOID] + _delayTransition) return;

            // �����ҋ@���.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWIDLE], _idleDrawnSword);
        }
    }
}