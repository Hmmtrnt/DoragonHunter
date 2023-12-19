/*�����X�^�[�̗̑͂�0�̎�*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateDown : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner._deathMotion = true;
        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            
            // ���ʂƓ����蔻����ђʂ�����.
            foreach (MeshCollider collider in owner._colliderChildren)
            {
                collider.isTrigger = true;
            }
            // �S�Ă̍U�������蔻�������.
            owner._biteCollisiton.SetActive(false);

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._deathMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            
        }
    }

}