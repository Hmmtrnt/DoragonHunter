/*ハンターの体力が0になった時の処理*/

using UnityEngine;

public partial class Player
{
    public class StateDead : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            //Debug.Log("死");
        }

        public override void OnUpdate(Player owner)
        {
            owner._downMotion = true;
        }
        public override void OnFixedUpdate(Player owner)
        {
            
        }
    }
}


