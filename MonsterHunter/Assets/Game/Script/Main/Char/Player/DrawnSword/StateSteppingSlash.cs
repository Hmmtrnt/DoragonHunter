/*踏み込み斬り*/

using UnityEngine;

public partial class PlayerState
{
    public class StateSteppingSlash : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSteppingSlash = true;
            owner._attackFrame = 0;
        }

        public override void OnUpdate(PlayerState owner)
        {
            
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            owner._attackFrame++;
            if(owner._attackFrame >= 10)
            {
                owner._isCauseDamage = true;
            }
            if(owner._attackFrame >= 60)
            {
                owner._isCauseDamage = false;
            }

            //Debug.Log(owner._isCauseDamage);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            //if(owner._drawnSwordMotion)
            //{
            //    owner._drawnSwordMotion = false;
            //}
            //owner._drawnIdleMotion = false;
            owner._drawnSteppingSlash = false;
            owner._attackFrame = 0;
            owner._isCauseDamage = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            if(owner._attackFrame >= 120)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            // 回避.
            else if (owner._attackFrame >= 40 && 
                owner._viewDirection[(int)viewDirection.FORWARD] && owner.GetDistance() > 1 && 
                owner._input._AButtonDown)
            {
                owner.ChangeState(_avoidDrawnSword);
            }
            // 右回避.
            else if (owner._attackFrame >= 40 
                && owner._viewDirection[(int)viewDirection.RIGHT] && owner.GetDistance() > 1 && 
                owner._input._AButtonDown)
            {
                owner.ChangeState(_rightAvoid);
            }
            // 左回避.
            else if(owner._attackFrame >= 40 && 
                owner._viewDirection[(int)viewDirection.LEFT] && owner.GetDistance() > 1 
                && owner._input._AButtonDown)
            {
                owner.ChangeState(_leftAvoid);
            }
            // 突き.
            else if(owner._attackFrame >= 40 && (owner._input._YButtonDown || owner._input._BButtonDown))
            {
                owner.ChangeState(_piercing);
            }
            // 気刃斬り1.
            else if(owner._attackFrame >= 40 && owner._input._RightTrigger >= 0.5)
            {
                owner.ChangeState(_spiritBlade1);
            }
            
        }
    }
}