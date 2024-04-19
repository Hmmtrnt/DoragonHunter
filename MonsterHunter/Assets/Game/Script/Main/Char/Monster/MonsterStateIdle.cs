/*モンスターのアイドル*/

using UnityEngine.SceneManagement;

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

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._idleMotion = false;
            owner._weakenMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
//#if UNITY_EDITOR
            // 行動を起こさないようにする処理(デバッグ用).
            if (owner._isAction) return;
//#endif

            // 次の行動を起こすインターバル.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.IDLE]) return;

            // チュートリアルシーンによってAiを変更.
            if(owner._tutorialState)
            {
                owner.TutorialAI();
            }
            else
            {
                owner.AttackStateAi();
            }
        }
    }
}



