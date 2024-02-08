/*�I����ʂ̑�����*/

using UnityEngine;

public partial class SelectPlayerState
{
    public class SelectStateRunning : StateBase
    {
        public override void OnEnter(SelectPlayerState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._runMotion = true;
            owner._moveVelocityMagnification = owner._moveVelocityRunMagnification;
        }

        public override void OnUpdate(SelectPlayerState owner)
        {
            owner._moveVelocityMagnification = owner._moveVelocityRunMagnification;
        }

        public override void OnFixedUpdate(SelectPlayerState owner)
        {
            Move(owner);
            owner.RotateDirection();
        }

        public override void OnExit(SelectPlayerState owner, StateBase nextState)
        {
            owner._runMotion = false;
            owner._moveVelocityMagnification = owner._moveVelocityRunMagnification;
        }

        public override void OnChangeState(SelectPlayerState owner)
        {
            // �A�C�h�����.
            if ((owner._leftStickHorizontal == 0 &&
                owner._leftStickVertical == 0) ||
                owner._openMenu)
            {
                owner.ChangeState(_idle);
            }

            // �_�b�V�����.
            if (owner._input._RBButton)
            {
                owner.ChangeState(_dash);
            }
        }

        // �ړ�.
        private void Move(SelectPlayerState owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + 
                new Vector3(0.0f, owner._rigidbody.velocity.y, 0.0f);
            owner._currentRunSpeed = owner._rigidbody.velocity.magnitude / owner._moveVelocityMagnification;
        }
    }
}



