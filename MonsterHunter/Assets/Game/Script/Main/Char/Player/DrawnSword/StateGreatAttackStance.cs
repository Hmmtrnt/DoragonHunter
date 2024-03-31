/*�K�E�Z�̍\��*/

public partial class PlayerState
{
    public class StateGreatAttackStance : StateBase
    {
        // �J�E���^�[��t�I���^�C�~���O.
        private float _counterValidFinish = 1.5f;
        // ���̔����^�C�~���O.
        private float _sePlayTiming = 0.02f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._greatAttackStanceMotion = true;
            owner._counterValid = true;
            owner._counterSuccess = false;
            owner._currentRenkiGauge = 0;
            
        }

        public override void OnUpdate(PlayerState owner)
        {
            // �J�E���^�[��t�I��.
            if(owner._stateTime >= _counterValidFinish)
            {
                owner._counterValid = false;
            }

            // �\���鎞�̉��Đ�.
            owner.SEPlay(_sePlayTiming, (int)SEManager.HunterSE.SHEATHINGSWORD);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._greatAttackStanceMotion = false;
            owner._counterSuccess = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // �����ҋ@���.
            if(owner._stateTime >= owner._stateTransitionTime[(int)StateTransitionKinds.GREATATTACKSTANCE])
            {
                owner.StateTransition(_idleDrawnSword);
            }
            // �J�E���^�[������.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSUCCESS], _stanceSuccess);
        }
    }
}



