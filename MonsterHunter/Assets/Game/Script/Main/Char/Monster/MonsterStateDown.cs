/*�����X�^�[�̗̑͂�0�̎�*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateDown : StateBase
    {
        // SE��炷�^�C�~���O.
        private const float _sePlayTiming = 1;

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(_sePlayTiming, (int)SEManager.MonsterSE.DOWN);

            owner._deathMotion = true;

            // ���ʂƓ����蔻����ђʂ�����.
            foreach (MeshCollider collider in owner._colliderChildren)
            {
                collider.isTrigger = true;
            }
            // �S�Ă̍U�������蔻�������.
            owner._biteCollisiton.SetActive(false);
            owner._rushCollisiton.SetActive(false);
            owner._wingLeftCollisiton.SetActive(false);
            owner._wingRightCollisiton.SetActive(false);
            for (int tailColNum = 0; tailColNum < owner._tailCollisiton.Length; tailColNum++)
            {
                owner._tailCollisiton[tailColNum].SetActive(false);
            }
            owner._rotateCollisiton.SetActive(false);
        }
    }
}