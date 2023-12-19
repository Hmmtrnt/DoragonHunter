/*モンスターの体力が0の時*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateDown : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
        }

        public override void OnUpdate(MonsterState owner)
        {
            owner._deathMotion = true;
        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            
            // 死ぬと当たり判定を貫通させる.
            foreach (MeshCollider collider in owner._colliderChildren)
            {
                collider.isTrigger = true;
            }
            // 全ての攻撃当たり判定を消去.
            owner._biteCollisiton.SetActive(false);

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._deathMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            
        }
    }

}