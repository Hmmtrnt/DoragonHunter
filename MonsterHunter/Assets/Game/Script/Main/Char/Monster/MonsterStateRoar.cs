/*咆哮*/

public partial class MonsterState
{
    public class MonsterStateRoar : StateBase
    {
        // SEを鳴らすタイミング.
        private const float _sePlayTiming = 1.7f;

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner._isRoar = false;
            owner._roarMotion = true;
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner._isRoar = false;
            owner.SEPlay(_sePlayTiming, (int)SEManager.MonsterSE.ROAR);
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._roarMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateTime >= owner._stateTransitionTime[(int)StateTransitionKinds.ROAR])
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


