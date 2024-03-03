/*カウンター成功時*/

using UnityEngine;

public partial class PlayerState
{
    public class StateGreatAttackSuccess : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._attackPower = 1000;
            owner._hitStopTime = 0.5f;
            owner._greatAttackSuccess = true;
            owner._isAttackProcess = false;
            owner._isCauseDamage = true;
            owner._attackCol._isOneProcess = true;
        }

        public override void OnUpdate(PlayerState owner)
        {
            if(owner._stateTime >= 0.1f && !owner._isAttackProcess)
            {
                owner._weaponActive = true;
                owner._isAttackProcess = true;
            }
            if(owner._stateTime >= 0.8f)
            {
                owner._weaponActive = false;
            }
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._greatAttackSuccess = false;
            owner._counterValid = false;
            owner._weaponActive = false;
            owner._isAttackProcess = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            if (owner._stateTime >= 2.0f)
            {
                owner.StateTransition(_idleDrawnSword);
            }
        }
    }
}



