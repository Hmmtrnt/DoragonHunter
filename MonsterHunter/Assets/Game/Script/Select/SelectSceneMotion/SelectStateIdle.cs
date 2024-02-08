/*�I����ʂ̃A�C�h�����*/

using UnityEngine;

public partial class SelectPlayerState
{
    public class SelectStateIdle : StateBase
    {
        public override void OnEnter(SelectPlayerState owner, StateBase prevState)
        {
            owner._idleMotion = true;
        }

        public override void OnUpdate(SelectPlayerState owner)
        {
        }

        public override void OnFixedUpdate(SelectPlayerState owner)
        {

        }

        public override void OnExit(SelectPlayerState owner, StateBase nextState)
        {
            owner._idleMotion = false;
        }

        public override void OnChangeState(SelectPlayerState owner)
        {
            // ���j���[��ʂ��J���ƃX�L�b�v.
            if(owner._openMenu) { return; }

            // �ړ�.
            if((owner._leftStickHorizontal != 0.0f ||
                owner._leftStickVertical != 0.0f))
            {
                if (owner._input._RBButton && !owner._openMenu)
                {
                    // �_�b�V��.
                    owner.ChangeState(_dash);
                }
                else
                {
                    // �ړ�.
                    owner.ChangeState(_running);
                }
            }
        }
    }
}



