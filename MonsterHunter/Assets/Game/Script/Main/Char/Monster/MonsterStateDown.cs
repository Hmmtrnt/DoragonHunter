/*�����X�^�[�̗̑͂�0�̎�*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateDown : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            
            Debug.Log("�ʂ�");
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner._deathMotion = true;
        }

        public override void OnFixedUpdate(MonsterState owner)
        {

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