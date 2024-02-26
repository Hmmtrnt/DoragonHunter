/*™ôšK*/

using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public partial class MonsterState
{
    public class MonsterStateRoar : StateBase
    {
        public override void OnEnter(MonsterState owner, StateBase prevState)
        {
            owner._isRoar = false;
            owner._roarMotion = true;
            owner.StateTransitionInitialization();
        }

        //public override void OnEnter(Monster owner, StateBase prevState)
        //{
        //    owner._isRoar = false;
        //    owner._roarMotion = true;
        //    Debug.Log("’Ê‚é");
        //    owner.StateTransitionInitialization();
        //}

        public override void OnUpdate(MonsterState owner)
        {
            owner._isRoar = false;
            //Debug.Log(owner._isRoar);
        }

        public override void OnFixedUpdate(MonsterState owner)
        {
            //RoarSound(owner);
            owner.SEPlay(90, (int)SEManager.MonsterSE.ROAR);
        }

        public override void OnExit(MonsterState owner, StateBase nextState)
        {
            owner._roarMotion = false;
        }

        public override void OnChangeState(MonsterState owner)
        {
            if(owner._stateFlame >= 260)
            {
                owner.ChangeState(_idle);
                //Debug.Log("’Ê‚é");
            }
        }

        // ™ôšK‚Ì‰¹‚ð—¬‚·.
        //private void RoarSound(Monster owner)
        //{
        //    if(owner._stateFlame == 90)
        //    {
        //        owner._seManager.MonsterPlaySE((int)MainSceneSEManager.MonsterSE.ROAR);
        //    }
            
        //}
    }
}


