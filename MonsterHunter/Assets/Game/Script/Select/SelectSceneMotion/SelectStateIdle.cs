/*選択画面のアイドル状態*/

using UnityEngine;

public partial class SelectPlayerState
{
    public class SelectStateIdle : StateBase
    {
        public override void OnEnter(SelectPlayerState owner, StateBase prevState)
        {
            owner._idleMotion = true;
        }

        public override void OnUpdate(SelectPlayerState owner)
        {
        }

        public override void OnFixedUpdate(SelectPlayerState owner)
        {

        }

        public override void OnExit(SelectPlayerState owner, StateBase nextState)
        {
            owner._idleMotion = false;
        }

        public override void OnChangeState(SelectPlayerState owner)
        {
            // メニュー画面を開くとスキップ.
            if(owner._openMenu) { return; }

            // 移動.
            if((owner._leftStickHorizontal != 0.0f ||
                owner._leftStickVertical != 0.0f))
            {
                if (owner._input._RBButton && !owner._openMenu)
                {
                    // ダッシュ.
                    owner.ChangeState(_dash);
                }
                else
                {
                    // 移動.
                    owner.ChangeState(_running);
                }
            }
        }
    }
}



