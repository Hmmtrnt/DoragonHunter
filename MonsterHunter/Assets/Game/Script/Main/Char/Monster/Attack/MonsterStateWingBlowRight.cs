﻿/*右翼で攻撃*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateWingBlowRight : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._wingRightMotion = true;
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            if (owner._stateFlame == 40)
            {
                owner._wingRightCollisiton.SetActive(true);
            }
            else if (owner._stateFlame == 100)
            {
                owner._wingRightCollisiton.SetActive(false);
            }
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._wingRightMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 155)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


