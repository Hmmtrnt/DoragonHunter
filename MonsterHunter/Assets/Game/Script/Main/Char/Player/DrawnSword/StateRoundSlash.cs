/*気刃大回転斬り*/

using UnityEngine;

public partial class PlayerState
{
    public class StateRoundSlash : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritRoundSlash = true;
            owner._attackFrame = 0;
            owner._nextMotionFlame = 90;
            owner._attackCol._col.enabled = true;
            owner._deceleration = 0.9f;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            owner._attackFrame++;
            if (owner._attackFrame >= 10)
            {
                owner._isCauseDamage = true;
            }
            if (owner._attackFrame >= 60)
            {
                owner._isCauseDamage = false;
            }
            if(owner._attackFrame <= 10)
            {
                owner.ForwardStep(20);
            }
            if(owner._attackFrame >= 10 && owner._attackFrame <= 50)
            {
                owner._rigidbody.velocity *= owner._deceleration;
            }
            else if(owner._attackFrame >= 50)
            {
                owner.ForwardStep(8);
            }
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritRoundSlash = false;
            owner._attackFrame = 0;
            owner._unsheathedSword = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 納刀アイドル.
            if (owner._attackFrame >= owner._nextMotionFlame)
            {
                owner.ChangeState(_idle);
            }
        }
    }
}


