/*�i����o��*/

using UnityEngine;

public partial class PlayerState
{
    public class StateClimbSteps : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            
        }

        public override void OnUpdate(PlayerState owner)
        {
            
            owner._transform.position += new Vector3(0.0f, 0.01f, 0.0f);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            
        }

        public override void OnChangeState(PlayerState owner)
        {
            // ���̏�ԑJ�ڂ��N�����^�C�~���O.
            //if (owner._stateTime <= 1)
            //{
            //    return;
            //}
            //// �ҋ@���.
            //owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.IDLE], _idle);
            //// ������.
            //owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RUN], _running);
            //// �_�b�V�����.
            //owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DASH], _dash);
        }
    }
}