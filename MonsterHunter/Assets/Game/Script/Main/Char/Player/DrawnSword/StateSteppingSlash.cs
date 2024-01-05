/*踏み込み斬り*/

using UnityEngine;

public partial class PlayerState
{
    public class StateSteppingSlash : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnSteppingSlash = true;
            owner._nextMotionFlame = 50;
            owner._rigidbody.velocity = Vector3.zero;
            owner._unsheathedSword = true;
            owner.StateTransitionInitialization();
            owner._isCauseDamage = true;
            owner._AttackPower = 81;
        }

        public override void OnUpdate(PlayerState owner)
        {
            
        }

        public override void OnFixedUpdate(PlayerState owner)
        {
            if(owner._stateFlame <= 40 && owner._stateFlame >= 10)
            {
                owner.ForwardStep(8);
            }
            else
            {
                owner._rigidbody.velocity *= 0.8f;
            }


            if(owner._stateFlame >= 60)
            {
                owner._isCauseDamage = false;
            }

        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnSteppingSlash = false;
            owner._isCauseDamage = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            if(owner._stateFlame>= 120)
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
            else if(owner._stateFlame >= owner._nextMotionFlame && 
                owner._viewDirection[(int)viewDirection.LEFT] &&
                owner.GetDistance() > 1 &&
                owner._input._AButtonDown)
            {
                owner.ChangeState(_leftAvoid);
            }
            // 突き.
            else if(owner._stateFlame >= owner._nextMotionFlame &&
                (owner._input._YButtonDown || owner._input._BButtonDown))
            {
                owner.ChangeState(_piercing);
            }
            // 気刃斬り1.
            else if(owner._stateFlame >= owner._nextMotionFlame &&
                owner._input._RightTrigger >= 0.5)
            {
                owner.ChangeState(_spiritBlade1);
            }
        }
    }
}