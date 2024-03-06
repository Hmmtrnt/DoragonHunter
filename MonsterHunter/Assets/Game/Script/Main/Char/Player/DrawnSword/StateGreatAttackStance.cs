/*必殺技の構え*/

public partial class PlayerState
{
    public class StateGreatAttackStance : StateBase
    {
        // カウンター受付終了タイミング.
        private float _counterValidFinish = 1.5f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._greatAttackStanceMotion = true;
            owner._counterValid = true;
            owner._counterSuccess = false;
            owner._currentRenkiGauge = 0;
            
        }

        public override void OnUpdate(PlayerState owner)
        {
            // カウンター受付終了.
            if(owner._stateTime >= _counterValidFinish)
            {
                owner._counterValid = false;
            }
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._greatAttackStanceMotion = false;
            owner._counterSuccess = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 抜刀待機状態.
            if(owner._stateTime >= owner._stateTransitionTime[(int)StateTransitionKinds.GREATATTACKSTANCE])
            {
                owner.StateTransition(_idleDrawnSword);
            }
            // カウンター成功時.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSUCCESS], _stanceSuccess);
        }
    }
}



