/*ハンターの体力が0になった時の処理*/

public partial class PlayerState
{
    public class StateDead : StateBase
    {
        // モーションのみ再生.
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._downMotion = true;
        }
    }
}


