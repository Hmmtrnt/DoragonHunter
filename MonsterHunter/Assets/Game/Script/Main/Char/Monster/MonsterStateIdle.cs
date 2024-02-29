/*モンスターのアイドル*/

using DG.Tweening.Core.Easing;
using UnityEngine;

public partial class MonsterState
{
    public class MonsterStateIdle : StateBase
    {

        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner.StateTransitionInitialization();
            // アニメーション開始.
            // 弱っている時とそうでないときでモーションを変更.
            if (owner._weakenState)
            {
                owner._weakenMotion = true;
            }
            else
            {
                owner._idleMotion = true;
            }
            

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
            owner._weakenMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            //if (owner._stateTime <= 3) return;
            if (owner._stateFlame <= 100) return;

            // デバッグ用.
            // 近距離.
            //if (owner._isNearDistance)
            //{
            //    // 正面(主にかみつき).
            //    if (owner._viewDirection[(int)viewDirection.FORWARD])
            //    {
            //        owner.ChangeState(_rotate);
            //    }
            //}
            //// 遠距離.
            //else
            //{
            //    // 正面(主にかみつき).
            //    if (owner._viewDirection[(int)viewDirection.FORWARD])
            //    {
            //        owner.ChangeState(_rotate);
            //    }
            //}

            // デバッグ用.
            // 行動パターン.
            if (owner._viewDirection[(int)viewDirection.FORWARD])
            {
                //owner.ChangeState(_falter);
            }

            if (owner._stateIgnore) return;

            // 近距離.
            if (owner._isNearDistance)
            {
                // 正面(主にかみつき).
                if (owner._viewDirection[(int)viewDirection.FORWARD])
                {
                    if (owner._randomNumber <= 30)
                    {
                        owner.ChangeState(_bite);
                    }
                    else if (owner._randomNumber <= 70)
                    {
                        owner.ChangeState(_rush);
                    }
                    else
                    {
                        owner.ChangeState(_rotate);
                    }

                    // デバッグ用状態遷移.
                    //owner.ChangeState(_bite);
                }
                // 後ろ.
                else if (owner._viewDirection[(int)viewDirection.BACKWARD])
                {
                    if (owner._randomNumber <= 30)
                    {
                        owner.ChangeState(_rotate);
                    }
                    else if (owner._randomNumber <= 60)
                    {
                        owner.ChangeState(_tail);
                    }
                    else
                    {
                        owner.ChangeState(_bite);
                    }


                    // デバッグ用状態遷移.
                    //owner.ChangeState(_tail);
                }
                // 左.
                else if (owner._viewDirection[(int)viewDirection.LEFT])
                {
                    if (owner._randomNumber <= 20)
                    {
                        owner.ChangeState(_rotate);
                    }
                    else if (owner._randomNumber <= 60)
                    {
                        owner.ChangeState(_wingBlowLeft);
                    }
                    else
                    {
                        owner.ChangeState(_bite);
                    }

                    //デバッグ用状態遷移.
                    //owner.ChangeState(_rotate);
                }
                // 右.
                else if (owner._viewDirection[(int)viewDirection.RIGHT])
                {
                    if (owner._randomNumber <= 20)
                    {
                        owner.ChangeState(_rotate);
                    }
                    else if (owner._randomNumber <= 60)
                    {
                        owner.ChangeState(_wingBlowRight);
                    }
                    else
                    {
                        owner.ChangeState(_bite);
                    }

                    // デバッグ用状態遷移.
                    //owner.ChangeState(_rotate);
                }
            }
            // 遠距離.
            else
            {
                // 正面.
                if (owner._viewDirection[(int)viewDirection.FORWARD])
                {
                    if (owner._randomNumber <= 60)
                    {
                        owner.ChangeState(_rush);
                    }
                    else
                    {
                        owner.ChangeState(_bless);
                    }

                    // デバッグ用モーション遷移.
                    //owner.ChangeState(_rush);
                }
                // 背後.
                else if (owner._viewDirection[(int)viewDirection.BACKWARD])
                {
                    if (owner._randomNumber <= 40)
                    {
                        owner.ChangeState(_rush);
                    }
                    else
                    {
                        owner.ChangeState(_bless);
                    }

                    //owner.ChangeState(_rush);
                }
                // 左.
                else if (owner._viewDirection[(int)viewDirection.LEFT])
                {
                    owner.ChangeState(_bless);
                }
                // 右,
                else if (owner._viewDirection[(int)viewDirection.RIGHT])
                {
                    owner.ChangeState(_bless);
                }
            }
        }
    }
}



