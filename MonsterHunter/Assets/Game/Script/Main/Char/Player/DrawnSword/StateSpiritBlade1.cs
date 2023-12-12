/*気刃斬り1*/

using UnityEngine;

public partial class PlayerState
{
    public class StateSpiritBlade1 : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSpiritBlade1 = true;
            owner._attackFrame = 0;
            owner._nextMotionFlame = 50;
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
                // 減速させる
                owner._rigidbody.velocity *= owner._deceleration;
            }
            if (owner._attackFrame >= 60)
            {
                owner._isCauseDamage = false;
            }

            if(owner._attackFrame <= 10)
            {
                owner.ForwardStep(3);
            }
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSpiritBlade1 = false;
            owner._attackFrame = 0;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            if (owner._attackFrame >= 120)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            // 回避.
            else if (owner._attackFrame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.FORWARD] &&
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.ChangeState(_avoidDrawnSword);
            }
            // 右回避.
            else if (owner._attackFrame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.RIGHT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.ChangeState(_rightAvoid);
            }
            // 左回避.
            else if (owner._attackFrame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.LEFT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.ChangeState(_leftAvoid);
            }
            // 突きのちにつなげる.
            //else if (owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            //{
            //    owner.ChangeState(_piercing);
            //}
            // 気刃斬り1.
            else if (owner._attackFrame >= owner._nextMotionFlame && 
                owner._input._RightTrigger >= 0.5)
            {
                owner.ChangeState(_spiritBlade2);
            }

        }
    }
}


