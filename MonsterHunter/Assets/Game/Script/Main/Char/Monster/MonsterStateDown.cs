/*モンスターの体力が0の時*/

using UnityEngine;

public partial class Monster
{
    public class MonsterStateDown : StateBase
    {
        public override void OnEnter(Monster owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(Monster owner)
        {
            owner._deathMotion = true;
        }

        public override void OnFixedUpdate(Monster owner)
        {
            
            // 死ぬと当たり判定を貫通させる.
            foreach (MeshCollider collider in owner._colliderChildren)
            {
                collider.isTrigger = true;
            }
            // 全ての攻撃当たり判定を消去.
            owner._biteCollisiton.SetActive(false);
            owner._rushCollisiton.SetActive(false);
            owner._wingLeftCollisiton.SetActive(false);
            owner._wingRightCollisiton.SetActive(false);
            for(int tailColNum = 0; tailColNum < owner._tailCollisiton.Length; tailColNum++)
            {
                owner._tailCollisiton[tailColNum].SetActive(false);
            }
            owner._rotateCollisiton.SetActive(false);

        }

        public override void OnExit(Monster owner, StateBase nextState)
        {
            owner._deathMotion = false;
        }

        public override void OnChangeState(Monster owner)
        {
            
        }
    }

}