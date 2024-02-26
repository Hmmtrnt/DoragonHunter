/*ハンターの体力が0になった時の処理*/

using UnityEngine;

public partial class PlayerState
{
    public class StateDead : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            //Debug.Log("死");
        }

        public override void OnUpdate(PlayerState owner)
        {
            owner._downMotion = true;
        }
        public override void OnFixedUpdate(PlayerState owner)
        {
            
        }
    }
}


