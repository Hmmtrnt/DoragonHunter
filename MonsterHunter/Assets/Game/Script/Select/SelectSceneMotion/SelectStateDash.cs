/*�I����ʂ̃_�b�V��*/

using UnityEngine;

public partial class SelectPlayerState
{
    public class SelectStateDash : StateBase
    {
        public override void OnEnter(SelectPlayerState owner, StateBase prevState)
        {
            owner._dashMotion = true;
            owner._moveVelocityMagnification = owner._moveVelocityDashMagnigication;
        }

        public override void OnUpdate(SelectPlayerState owner)
        {

        }

        public override void OnFixedUpdate(SelectPlayerState owner)
        {
            Move(owner);
            owner.RotateDirection();
        }

        public override void OnExit(SelectPlayerState owner, StateBase nextState)
        {
            owner._dashMotion = false;
        }

        public override void OnChangeState(SelectPlayerState owner)
        {
            // idle���.
            if ((owner._leftStickHorizontal == 0 &&
                owner._leftStickVertical == 0) ||
                owner._openMenu)
            {
                owner.ChangeState(_idle);
            }
            // run���.
            else if (owner._input._RBButtonUp)
            {
                owner.ChangeState(_running);
            }
        }

        // �ړ�.
        private void Move(SelectPlayerState owner)
        {
            owner._rigidbody.velocity = owner._moveVelocity * owner._moveVelocityMagnification + 
                new Vector3(0.0f, owner._rigidbody.velocity.y, 0.0f);
        }
    }
}
