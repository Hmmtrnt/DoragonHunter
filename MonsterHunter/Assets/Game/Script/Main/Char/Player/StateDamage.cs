/*ダメージを受けた時*/

using UnityEngine;

public partial class PlayerState
{
    public class StateDamage : StateBase
    {
        // ノックバックされる方向ベクトル.
        Vector3 _knockBackVector = Vector3.zero;
        // 吹っ飛ばされた時の速度倍率.
        private const float _blowAwayVelocitySpeed = 10;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._currentHitPoint = owner._currentHitPoint - owner._MonsterState.GetMonsterAttack();
            owner._damageMotion = true;
            owner._isProcess = true;
            owner._rigidbody.velocity = Vector3.zero;
            KnockBackVector(owner);
            owner._seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.HunterSE.DAMAGE);
        }

        public override void OnUpdate(PlayerState owner)
        {
            if (!owner._isProcess) return;

            KnockBack(owner);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._damageMotion = false;
            owner._isProcess = false;
            owner._rigidbody.velocity = Vector3.zero;
        }

        public override void OnChangeState(PlayerState owner)
        {
            
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.DAMAGE]) return;

            // 納刀かそうじゃないかで状態遷移先を変更.
            if (owner._unsheathedSword)
            {
                owner.StateTransition(_idleDrawnSword);
            }
            else if(!owner._unsheathedSword)
            {
                owner.StateTransition(_idle);
            }
        }

        /// <summary>
        /// ノックバック先のベクトル取得.
        /// </summary>
        /// <param name="owner">アクセスするための参照</param>
        private void KnockBackVector(PlayerState owner)
        {
            _knockBackVector = owner._transform.position - owner._Monster.transform.position;

            _knockBackVector.Normalize();
        }

        /// <summary>
        /// ノックバック.
        /// </summary>
        /// <param name="owner">アクセスするための参照</param>
        private void KnockBack(PlayerState owner)
        {
            owner._rigidbody.velocity = new Vector3(_knockBackVector.x * _blowAwayVelocitySpeed, 0.0f, _knockBackVector.z * _blowAwayVelocitySpeed);

            Quaternion rotation = Quaternion.LookRotation(-new Vector3(_knockBackVector.x, 0.0f, _knockBackVector.z), Vector3.up);
            owner._transform.rotation = rotation;
        }
    }

}
