/*ダメージを受けた時*/

using UnityEngine;

public partial class PlayerState
{
    public class StateDamage : StateBase
    {
        Vector3 knockBackVector = Vector3.zero;


        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._hitPoint = owner._hitPoint - owner._MonsterState.GetMonsterAttack();
            owner._damageMotion = true;
            owner._isProcess = true;
            owner._rigidbody.velocity = Vector3.zero;
            KnockBackVector(owner);
            //owner._seManager.HunterPlaySE((int)MainSceneSEManager.HunterSE.DAMAGE);
            owner._seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.HunterSE.DAMAGE);
        }

        public override void OnUpdate(PlayerState owner)
        {
            
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
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
            if (owner._stateFlame <= 90) return;

            if(owner._unsheathedSword)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            else if(!owner._unsheathedSword)
            {
                owner.ChangeState(_idle);
            }
        }

        // ノックバック先のベクトル取得.
        private void KnockBackVector(PlayerState owner)
        {
            knockBackVector = owner._transform.position - owner._Monster.transform.position;

            knockBackVector.Normalize();
        }

        // ノックバック
        private void KnockBack(PlayerState owner)
        {
            // 敵の中心点からベクトルを取得.
            //Vector3 dir = owner._transform.position - owner._Monster.transform.position;
            //dir = dir.normalized;
            //owner._rigidbody.AddForce(dir * 30, ForceMode.Impulse);
            if(owner._stateFlame <= 40)
            {
                owner._transform.position += knockBackVector * 0.15f;
            }
            else if(owner._stateFlame <= 80)
            {
                owner._transform.position += knockBackVector * 0.3f;
            }
            

            var rotation = Quaternion.LookRotation(-knockBackVector, Vector3.up);
            owner._transform.rotation = rotation;
            //owner._isProcess = false;
        }
    }

}
