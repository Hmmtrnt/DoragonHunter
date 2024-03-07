/*モンスターの体力が0の時*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateDown : StateBase
    {
        // SEを鳴らすタイミング.
        private const float _sePlayTiming = 1;

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner.SEPlay(_sePlayTiming, (int)SEManager.MonsterSE.DOWN);

            owner._deathMotion = true;

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
            for (int tailColNum = 0; tailColNum < owner._tailCollisiton.Length; tailColNum++)
            {
                owner._tailCollisiton[tailColNum].SetActive(false);
            }
            owner._rotateCollisiton.SetActive(false);
        }
    }
}