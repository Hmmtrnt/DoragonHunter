/*回避後の後ろ回避*/

using UnityEngine;

public partial class Player
{
    public class StateBackAvoidDrawSword : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            owner._drawnBackAvoidMotion = true;
            owner._stamina -= owner._avoidStaminaCost;
            owner._isProcess = true;
            owner._avoidVelocity = -owner._transform.forward * owner._avoidVelocityMagnification;
        }

        public override void OnUpdate(Player owner)
        {
        }

        public override void OnFixedUpdate(Player owner)
        {
            
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._drawnBackAvoidMotion = false;
        }

        public override void OnChangeState(Player owner)
        {
            
        }
    }
}