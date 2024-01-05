/*突き*/

using UnityEngine;

public partial class PlayerState
{
    public class StatePiercing : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnThrustSlash = true;
            owner._nextMotionFlame = 35;
            owner.StateTransitionInitialization();
            owner._AttackPower = 65;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            if (owner._stateFlame >= 10)
            {
                owner._isCauseDamage = true;
            }
            if (owner._stateFlame >= 60)
            {
                owner._isCauseDamage = false;
            }

            // 前進させる.
            if(owner._stateFlame <= 20)
            {
                owner.ForwardStep(2);
            }
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnThrustSlash = false;
            owner._stateFlame = 0;
            owner._isCauseDamage = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            if (owner._stateFlame >= 80)
            {
                owner.ChangeState(_idleDrawnSword);
            }
            // 回避.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.FORWARD] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.ChangeState(_avoidDrawnSword);
            }
            // 右回避.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.RIGHT] && 
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.ChangeState(_rightAvoid);
            }
            // 左回避.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._viewDirection[(int)viewDirection.LEFT] && 
                owner.GetDistance() > 1 
                && owner._input._AButtonDown)
            {
                owner.ChangeState(_leftAvoid);
            }
            // 斬り上げ.
            else if (owner._stateFlame >= owner._nextMotionFlame - 10 &&
                (owner._input._YButtonDown || owner._input._BButtonDown))
            {
                owner.ChangeState(_slashUp);
            }
            // 気刃斬り1.
            else if (owner._stateFlame >= owner._nextMotionFlame &&
                owner._input._RightTrigger >= 0.5)
            {
                owner.ChangeState(_spiritBlade1);
            }
        }
    }
}

