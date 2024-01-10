/*突進攻撃*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateRushForward : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._rushMotion = true;
            //owner._trasnform.LookAt(owner._hunter.transform.position);
        }

        public override void OnUpdate(MonsterState owner)
        {

        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            owner.TurnTowards(40);

            if (owner._stateFlame == 0)
            {
                //owner._trasnform.position -= Vector3.forward * 0.15f;
                owner._forwardSpeed = 0.15f;
            }
            else if (owner._stateFlame == 60)
            {
                //owner._trasnform.position -= Vector3.forward * 0.5f;
                owner._forwardSpeed = 0.7f;
            }
            else if (owner._stateFlame == 140)
            {
                //owner._trasnform.position += Vector3.forward * 0.15f;
                //owner._forwardSpeed = -0.15f;
                owner._forwardSpeed = 0.15f;
            }

            if(owner._stateFlame >= 60 && owner._stateFlame <= 140)
            {
                owner._forwardSpeed -= 0.001f;
            }

            if(owner._stateFlame == 65)
            {
                owner._rushCollisiton.SetActive(true);
                owner._wingRightCollisiton.SetActive(true);
                owner._wingLeftCollisiton.SetActive(true);
            }
            else if(owner._stateFlame == 130)
            {
                owner._rushCollisiton.SetActive(false);
                owner._wingRightCollisiton.SetActive(false);
                owner._wingLeftCollisiton.SetActive(false);
            }

            owner._trasnform.position += owner._moveVelocity;
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._rushMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 150)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}