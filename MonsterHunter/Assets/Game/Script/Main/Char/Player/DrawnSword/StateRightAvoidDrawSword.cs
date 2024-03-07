/*�E���*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRightAvoidDrawSword : StateBase
    {
        // ��ԑJ�ڂ��s���ۂɏ������炵�ď�����ʂ����߂̕ϐ�.
        private const float _delayTransition = 0.1f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnRightAvoidMotion = true;
            owner._isProcess = true;
            owner._avoidVelocity = owner._transform.right * owner._avoidVelocityMagnification;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnUpdate(PlayerState owner)
        {
            owner.MoveAvoid();
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnRightAvoidMotion = false;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // ���̏�ԑJ�ڂ��N�����^�C�~���O.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.RIGHTAVOID]) return;

            // �����ړ����.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWRUN], _runDrawnSword);

            // ���̏�ԑJ�ڂ��N�����^�C�~���O.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.RIGHTAVOID] + _delayTransition) return;

            // �����ҋ@���.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWIDLE], _idleDrawnSword);
        }
    }
}