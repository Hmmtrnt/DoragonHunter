/*右翼で攻撃*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateWingBlowRight : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._wingRightMotion = true;
            owner._currentWingAttackRight = true;
            owner._AttackPower = 5;
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

            ParticleGenerateTime(owner);

            owner.SEPlay(20, (int)SEManager.MonsterSE.FOOTSTEP);
            owner.SEPlay(30, (int)SEManager.MonsterSE.ROTATE);
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._wingRightMotion = false;
            owner._currentWingAttackRight = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 135)
            {
                owner.ChangeState(_idle);
            }
        }

        // パーティクルをモーションを行っている時間で生成する.
        private void ParticleGenerateTime(MonsterState owner)
        {
            if (owner._stateFlame == 35)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.LEG, (int)footSmokePosition.WINGRIGHT);
            }
            if (owner._stateFlame == 60)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.WING, (int)footSmokePosition.WINGRIGHT);
            }
        }
    }
}


