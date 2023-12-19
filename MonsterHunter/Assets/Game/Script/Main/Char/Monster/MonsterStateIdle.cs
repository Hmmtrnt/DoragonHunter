﻿/*モンスターのアイドル*/

using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateIdle : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            owner._idleMotion = true;
        }

        public override void OnUpdate(MonsterState owner)
        {
            
        }

        public override void OnFixedUpdate(MonsterState owner)
        {

        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._idleMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if (owner._stateFlame <= 100) return;

            // 近距離.
            if(owner._isNearDistance)
            {
                // 正面.
                if(owner._viewDirection[(int)viewDirection.FORWARD])
                {
                    owner.ChangeState(_bite);
                }
                // 後ろ.
                else if (owner._viewDirection[(int)viewDirection.BACKWARD])
                {
                    owner.ChangeState(_rotate);
                }
                // 左.
                else if (owner._viewDirection[(int)viewDirection.LEFT])
                {
                    owner.ChangeState(_idle);
                }
                // 右.
                else if (owner._viewDirection[(int)viewDirection.RIGHT])
                {
                    owner.ChangeState(_idle);
                }
            }
            // 遠距離.
            else
            {
                // 正面.
                if (owner._viewDirection[(int)viewDirection.FORWARD])
                {
                    //if (owner._randomNumber < 61)
                    //{
                    //    owner.ChangeState(_rush);
                    //}
                    //else
                    //{
                    //    owner.ChangeState(_bless);
                    //}

                    // デバッグ用モーション遷移.
                    owner.ChangeState(_rotate);

                }
                else if (owner._viewDirection[(int)viewDirection.BACKWARD])
                {
                    if (owner._randomNumber < 41)
                    {
                        owner.ChangeState(_rush);
                    }
                    else
                    {
                        owner.ChangeState(_bless);
                    }
                }
                else if (owner._viewDirection[(int)viewDirection.LEFT])
                {
                    owner.ChangeState(_bless);
                }
                else if (owner._viewDirection[(int)viewDirection.RIGHT])
                {
                    owner.ChangeState(_bless);
                }
            }
        }
    }
}



