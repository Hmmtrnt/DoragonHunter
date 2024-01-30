/*アイドル*/

using UnityEngine;

public partial class PlayerState
{
    /// <summary>
    /// アイドル状態.
    /// </summary>
    public class StateIdle : StateBase
    {
        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._idleMotion = true;
        }

        public override void OnUpdate(PlayerState owner)
        {

        }

        public override void OnFixedUpdate(PlayerState owner)
        {

        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._idleMotion = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // 移動.
            if (owner._leftStickHorizontal != 0.0f ||
                owner._leftStickVertical != 0.0f)
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

            if (owner._openMenu) return;

            // HACK:のちにアイテムが何を選ばれているか.
            if (owner._input._XButtonDown && !owner._unsheathedSword && owner._hitPoint != owner._maxHitPoint)
            {
                // 回復.
                owner.ChangeState(_recovery);
            }

            // 抜刀する.
            if (owner._input._YButtonDown)
            {
                owner.ChangeState(_drawSwordTransition);
            }
            // 気刃斬り1.
            if (owner._input._RightTrigger >= 0.5f)
            {
                owner.ChangeState(_spiritBlade1);
            }
        }

        
    }
}


