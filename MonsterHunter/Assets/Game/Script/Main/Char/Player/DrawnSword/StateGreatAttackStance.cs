/*必殺技の構え*/

using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.Cameras;

public partial class PlayerState
{
    public class StateGreatAttackStance : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._greatAttackStanceMotion = true;
            owner._counterValid = true;
            owner._counterSuccess = false;
            owner._currentRenkiGauge = 0;
            
        }

        public override void OnUpdate(PlayerState owner)
        {
            if(owner._stateTime >= 1.5f)
            {
                owner._counterValid = false;
            }

            //Debug.Log(owner._counterSuccess);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._greatAttackStanceMotion = false;
            owner._counterSuccess = false;
            
        }

        public override void OnChangeState(PlayerState owner)
        {
            if(owner._stateTime >= 2.0f)
            {
                owner.StateTransition(_idleDrawnSword);
            }
            // カウンター成功時.
            if (owner._counterSuccess)
            {
                owner.StateTransition(_stanceSuccess);
            }
        }
    }
}



