﻿/*噛みつき*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateBite : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._biteMotion = true;
            if (owner._mainSceneManager._hitPointMany)
            {
                owner._AttackPower = 10;
            }
            else
            {
                owner._AttackPower = 5;
            }
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            owner.TurnTowards(40);

            if(owner._stateFlame >= 40 && owner._stateFlame <=70)
            {
                owner._biteCollisiton.SetActive(true);
            }
            else
            {
                owner._biteCollisiton.SetActive(false);
            }

            if(owner._stateFlame == 1)
            {
                //Debug.Log("通る");
                owner._forwardSpeed = 0.4f;
            }
            else if(owner._stateFlame == 15)
            {
                owner._forwardSpeed = 0.0f;
            }

            //Debug.Log(owner._forwardSpeed);

            owner._trasnform.position += owner._moveVelocity;

            owner.SEPlay(30, (int)SEManager.MonsterSE.BITE);
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._biteMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            
            if (owner._stateFlame >= 120)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


