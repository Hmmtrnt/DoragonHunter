/*ダメージを受けた時*/

using UnityEngine;

public partial class PlayerState
{
    public class StateDamage : StateBase
    {
        // ノックバックを受けている時間
        private int _testTime;
        

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            _testTime = 0;
            owner._hitPoint = owner._hitPoint - owner._MonsterState.GetMonsterAttack();
            owner._damageMotion = true;
            owner._isProcess = true;
        }

        public override void OnUpdate(PlayerState owner)
        {
            
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            _testTime++;
            //Debug.Log(_testTime);
            if (!owner._isProcess) return;
            //Debug.Log("通った");
            KnockBack(owner);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._damageMotion = false;
            owner._isProcess = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 納刀かそうじゃないかで遷移先を変更
            if(owner._unsheathedSword && _testTime >= 30)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            else if(!owner._unsheathedSword && _testTime >= 30)
            {
                owner.ChangeState(_idle);
            }
        }

        // ノックバック
        private void KnockBack(PlayerState owner)
        {
            // 敵の中心点からベクトルを取得
            Vector3 dir = owner._transform.position - owner._Monster.transform.position;
            dir = dir.normalized;
            owner._rigidbody.AddForce(dir * 20, ForceMode.Impulse);
            owner._isProcess = false;
        }
    }

}
