/*ダメージを受けた時*/

using UnityEngine;

public partial class PlayerState
{
    public class StateDamage : StateBase
    {
        // ノックバックされる方向ベクトル.
        Vector3 _knockBackVector = Vector3.zero;
        // 初速をかけるタイミング.
        private const float _initialVelocityTiming = 0.8f;
        // 地面についてから速度をかけるタイミング.
        private const float _landingVelocityTiming = 1.7f;
        // 初速の速度倍率.
        private const float _initialVelocitySpeed = 0.07f;
        // 地面についてからの速度倍率.
        private const float _landingVelocitySpeed = 0.18f;

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
        }

        public override void OnChangeState(PlayerState owner)
        {
            
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.DAMAGE]) return;

            // 納刀かそうじゃないかで遷移先を変更
            if (owner._unsheathedSword)
            {
                owner.StateTransition(_idleDrawnSword);
            }
            else if(!owner._unsheathedSword)
            {
                owner.StateTransition(_idle);
            }
        }

        // ノックバック先のベクトル取得.
        private void KnockBackVector(PlayerState owner)
        {
            _knockBackVector = owner._transform.position - owner._Monster.transform.position;

            _knockBackVector.Normalize();
        }

        // ノックバック
        private void KnockBack(PlayerState owner)
        {
            
            if(owner._stateTime <= _initialVelocityTiming)
            {
                owner._transform.position += _knockBackVector * _initialVelocitySpeed;
            }
            else if(owner._stateTime <= _landingVelocityTiming)
            {
                owner._transform.position += _knockBackVector * _landingVelocitySpeed;
            }

            Quaternion rotation = Quaternion.LookRotation(-_knockBackVector, Vector3.up);
            owner._transform.rotation = rotation;
        }
    }

}
