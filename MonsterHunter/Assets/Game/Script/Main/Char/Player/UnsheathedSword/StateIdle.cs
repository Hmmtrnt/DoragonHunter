﻿/*待機状態*/

using UnityEngine;

public partial class Player
{
    public class StateIdle : StateBase
    {
        public override void OnEnter(Player owner, StateBase prevState)
        {
            // アニメーション開始.
            owner._idleMotion = true;
        }

        public override void OnUpdate(Player owner)
        {
            
        }

        public override void OnFixedUpdate(Player owner)
        {
            
        }

        public override void OnExit(Player owner, StateBase nextState)
        {
            owner._idleMotion = false;
        }

        public override void OnChangeState(Player owner)
        {
            // 移動.
            owner.TransitionRun();

            if (owner._openMenu) return;

            // HACK:のちにアイテムが何を選ばれているか.
            if (owner._input._XButtonDown && !owner._unsheathedSword && owner._hitPoint != owner._maxHitPoint && 
                owner._cureMedicineNum > 0)
            {
                // 回復.
                owner.StateTransition(_recovery);
            }

            // 抜刀する.
            if (owner._input._YButtonDown)
            {
                owner.StateTransition(_drawSwordTransition);
            }
            // 気刃斬り1.
            if (owner._input._RightTrigger >= 0.5f)
            {
                owner.StateTransition(_spiritBlade1);
            }
        }

        // 移動処理の分岐
        
    }
}


