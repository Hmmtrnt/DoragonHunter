/*突き*/

public partial class PlayerState
{
    public class StatePrick : StateBase
    {
        // 攻撃判定発生タイミング.
        private const float _spawnColTiming = 0.13f;
        // 攻撃判定消去タイミング.
        private const float _eraseColTiming = 0.25f;
        // 前進させるタイミング.
        private const float _forwardStopTiming = 0.3f;
        // 移動力.
        private const float _speedPower = 2;
        // SEを鳴らすタイミング.
        private const float _sePlayTiming = 0.12f;
        // モーションキャンセル適応外の状態に遷移するタイミング.
        private const float _TransitionTime = 1.2f;

        public override void OnEnter(PlayerState owner, StateBase prevState)
        {
            owner._drawnThrustSlash = true;
            owner._nextMotionFlame = 35;
            owner.StateTransitionInitialization();
            owner._attackPower = 55;
            owner._isCauseDamage = true;
            owner._increaseAmountRenkiGauge = 3;
            owner._hitStopTime = 0.05f;
            owner._attackCol._isOneProcess = true;
            owner._nextMotionTime = 0.45f;
            owner._isAttackProcess = false;
        }

        public override void OnUpdate(PlayerState owner)
        {
            if (owner._stateTime >= _spawnColTiming && !owner._isAttackProcess)
            {
                owner._isAttackProcess = true;
                owner._weaponActive = true;
            }
            else if (owner._stateTime >= _eraseColTiming)
            {
                owner._weaponActive = false;
            }

            // 前進させる.
            if (owner._stateTime <= _forwardStopTiming)
            {
                owner.ForwardStep(_speedPower);
            }

            // 空振り効果音再生.
            //owner.SEPlay(10, (int)SEManager.HunterSE.MISSINGSLASH);
            owner.SEPlayTest(_sePlayTiming, (int)SEManager.HunterSE.MISSINGSLASH);
        }

        public override void OnExit(PlayerState owner, StateBase nextState)
        {
            owner._drawnThrustSlash = false;
            owner._stateFlame = 0;
            owner._weaponActive = false;
        }

        public override void OnChangeState(PlayerState owner)
        {
            // アイドル.
            //if (owner._stateTime >= 1.2f)
            //{
            //    owner.StateTransition(_idleDrawnSword);
            //}

            // モーションキャンセル適応外の遷移先.
            if (owner._stateTime >= _TransitionTime)
            {
                // 抜刀待機状態.
                owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWIDLE], _idleDrawnSword);
                // 抜刀移動状態.
                owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWRUN], _runDrawnSword);
            }

            // 次の状態遷移を起こすタイミング.
            if (owner._stateTime <= owner._stateTransitionTime[(int)StateTransitionKinds.PRICK]) return;

            // 回避.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.DRAWAVOID], _avoidDrawnSword);
            //else if (owner._stateTime >= owner._nextMotionTime &&
            //    owner._viewDirection[(int)viewDirection.FORWARD] && 
            //    owner.GetDistance() > 1 &&
            //    owner._input._AButtonDown)
            //{
            //    owner.StateTransition(_avoidDrawnSword);
            //}
            // 右回避.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.RIGHTAVOID], _rightAvoid);
            //else if (owner._stateTime >= owner._nextMotionTime &&
            //    owner._viewDirection[(int)viewDirection.RIGHT] && 
            //    owner.GetDistance() > 1 &&
            //    owner._input._AButtonDown)
            //{
            //    owner.StateTransition(_rightAvoid);
            //}
            // 左回避.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.LEFTAVOID], _leftAvoid);
            //else if (owner._stateTime >= owner._nextMotionTime &&
            //    owner._viewDirection[(int)viewDirection.LEFT] && 
            //    owner.GetDistance() > 1 
            //    && owner._input._AButtonDown)
            //{
            //    owner.StateTransition(_leftAvoid);
            //}
            // 後ろ回避.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.BACKAVOID], _backAvoid);
            //else if (owner._stateTime >= owner._nextMotionTime &&
            //    owner._viewDirection[(int)viewDirection.BACKWARD] &&
            //    owner.GetDistance() > 1 &&
            //    owner._input._AButtonDown)
            //{
            //    owner.StateTransition(_backAvoid);
            //}
            // 斬り上げ.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SLASHUP], _slashUp);
            //else if (owner._stateTime >= owner._nextMotionTime &&
            //    (owner._input._YButtonDown || owner._input._BButtonDown))
            //{
            //    owner.StateTransition(_slashUp);
            //}
            // 必殺技の構え
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSTANCE], _stance);
            //else if (owner._input._LBButton && owner._input._BButtonDown && owner._applyRedRenkiGauge)
            //{
            //    owner.StateTransition(_stance);
            //}
            // 気刃斬り1.
            owner.TransitionState(owner._stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1], _spiritBlade1);
            //else if (owner._stateTime >= owner._nextMotionTime &&
            //    owner._input._RightTrigger >= 0.5)
            //{
            //    owner.StateTransition(_spiritBlade1);
            //}
        }
    }
}

