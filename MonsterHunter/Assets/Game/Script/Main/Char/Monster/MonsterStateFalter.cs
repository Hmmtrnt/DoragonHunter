/*モンスターが怯むときの処理*/

public partial class MonsterState
{
    public class MonsterStateFalter : StateBase
    {
        // SEを鳴らすタイミング.
        private const float _sePlayTiming = 0.2f;

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._falterMotion = true;
            owner._biteCollisiton.SetActive(false);
            owner._rushCollisiton.SetActive(false);
            owner._wingRightCollisiton.SetActive(false);
            owner._wingLeftCollisiton.SetActive(false);
            for (int colNum = 0; colNum < owner._tailCollisiton.Length; colNum++)
            {
                owner._tailCollisiton[colNum].SetActive(false);
            }
            owner._rotateCollisiton.SetActive(false);
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(_sePlayTiming, (int)SEManager.MonsterSE.FALTER);
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._falterMotion = false;
            owner._forwardSpeed = 0;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateTime >= owner._stateTransitionTime[(int)StateTransitionKinds.FALTER])
            {
                owner.ChangeState(_idle);
            }
        }
    }
}