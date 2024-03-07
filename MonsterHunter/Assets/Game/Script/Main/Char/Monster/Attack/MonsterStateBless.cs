/*�u���X�����Ƃ��̏��*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateBless : StateBase
    {
        // ��]�X�s�[�h.
        private const int _rotateSpeed = 40;
        // �U�����蔭���^�C�~���O.
        private const int _spawnColTiming = 55;
        // SE��炷�^�C�~���O.
        private float _sePlayTiming = 0.8f;

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._blessMotion = true;
            if (owner._mainSceneManager._hitPointMany)
            {
                owner._AttackPower = 20;
            }
            else
            {
                owner._AttackPower = 15;
            }
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(_sePlayTiming, (int)SEManager.MonsterSE.BLESS);
        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            owner.TurnTowards(_rotateSpeed);

            // ����.
            if (owner._stateFlame == _spawnColTiming)
            {
                Instantiate(owner._fireBall, new Vector3(owner._fireBallPosition.transform.position.x,
                owner._fireBallPosition.transform.position.y,
                owner._fireBallPosition.transform.position.z), Quaternion.identity);
            }
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._blessMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateTime >= owner._stateTransitionTime[(int)StateTransitionKinds.BLESS])
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


