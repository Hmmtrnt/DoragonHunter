/*カウンター成功時*/

using UnityEngine;

public partial class PlayerState
{
    public class StateGreatAttackSuccess : StateBase
    {
        // 攻撃判定発生タイミング.
        private const float _spawnColTiming = 0.1f;
        // 攻撃判定消去タイミング.
        private const float _eraseColTiming = 0.8f;
        // SEを鳴らすタイミング.
        private const float _sePlayTiming = 0.1f;

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
            if(owner._stateTime >= _spawnColTiming && !owner._isAttackProcess)
            {
                owner._weaponActive = true;
                owner._isAttackProcess = true;
            }
            if(owner._stateTime >= _eraseColTiming)
            {
                owner._weaponActive = false;
            }

            // 空振り効果音再生.
            owner.SEPlayTest(_sePlayTiming, (int)SEManager.HunterSE.MISSINGROUNDSLASH);
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
            if (owner._stateTime >= owner._stateTransitionTime[(int)StateTransitionKinds.GREATATTACKSUCCESS])
            {
                owner.StateTransition(_idleDrawnSword);
            }
        }
    }
}



