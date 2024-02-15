/*左翼攻撃*/

using UnityEngine;

public partial class Monster
{
    public class MonsterStateWingBlowLeft : StateBase
    {
        public override void OnEnter(Monster owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._wingLeftMotion = true;
            owner._currentWingAttackLeft = true;
            if (owner._mainSceneManager._hitPointMany)
            {
                owner._AttackPower = 10;
            }
            else
            {
                owner._AttackPower = 5;
            }
        }

        public override void OnUpdate(Monster owner)
        {

        }

        public override void OnFixedUpdate(Monster owner)
        {
            if(owner._stateFlame == 40 )
            {
                owner._wingLeftCollisiton.SetActive(true);
            }
            else if(owner._stateFlame == 100)
            {
                owner._wingLeftCollisiton.SetActive(false);
            }

            ParticleGenerateTime(owner);

            owner.SEPlay(20, (int)SEManager.MonsterSE.FOOTSTEP);
            owner.SEPlay(30, (int)SEManager.MonsterSE.ROTATE);
        }

        public override void OnExit(Monster owner, StateBase nextState)
        {
            owner._wingLeftMotion = false;
            owner._currentWingAttackLeft = false;
        }

        public override void OnChangeState(Monster owner)
        {
            if(owner._stateFlame >= 135)
            {
                owner.ChangeState(_idle);
            }
        }

        // パーティクルをモーションを行っている時間で生成する.
        private void ParticleGenerateTime(Monster owner)
        {
            if (owner._stateFlame == 35)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.LEG, (int)footSmokePosition.WINGLEFT);
            }
            if (owner._stateFlame == 60)
            {
                owner.FootSmokeSpawn((int)footSmokeEffect.WING, (int)footSmokePosition.WINGLEFT);
            }
        }
    }
}



