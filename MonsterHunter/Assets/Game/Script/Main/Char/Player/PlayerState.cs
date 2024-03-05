/*プレイヤー行動全体の管理*/

using UnityEngine;

public partial class PlayerState : MonoBehaviour
{
    //--納刀状態--//
    private static readonly StateIdle                 _idle = new();                // アイドル.
    private static readonly StateAvoid                _avoid = new();               // 回避.
    private static readonly StateRunning              _running = new();             // 走る.
    private static readonly StateDash                 _dash = new();                // ダッシュ.
    private static readonly StateFatigueDash          _fatigueDash = new();         // 疲労時のダッシュ.
    private static readonly StateRecovery             _recovery = new();            // 回復.

    //--抜刀状態--//
    private static readonly StateDrawnSwordTransition _drawSwordTransition = new(); // 抜刀する.
    private static readonly StateIdleDrawnSword       _idleDrawnSword = new();      // アイドル.
    private static readonly StateRunDrawnSword        _runDrawnSword = new();       // 走る.
    private static readonly StateAvoidDrawSword       _avoidDrawnSword = new();     // 抜刀回避.
    private static readonly StateRightAvoidDrawSword  _rightAvoid = new();          // 攻撃後の右回避.
    private static readonly StateLeftAvoidDrawSword   _leftAvoid = new();           // 攻撃後の左回避.
    private static readonly StateBackAvoidDrawSword   _backAvoid = new();           // 攻撃後の後ろ回避.
    private static readonly StateSheathingSword       _sheathingSword = new();      // 納刀する.
    private static readonly StateSteppingSlash        _steppingSlash = new();       // 踏み込み斬り.
    private static readonly StatePiercing             _piercing = new();            // 突き.
    private static readonly StateSlashUp              _slashUp = new();             // 斬り上げ.
    private static readonly StateSpiritBlade1         _spiritBlade1 = new();        // 気刃斬り1.
    private static readonly StateSpiritBlade2         _spiritBlade2 = new();        // 気刃斬り2.
    private static readonly StateSpiritBlade3         _spiritBlade3 = new();        // 気刃斬り3.
    private static readonly StateRoundSlash           _roundSlash = new();          // 気刃大回転斬り.
    private static readonly StateGreatAttackStance    _stance = new();              // 必殺技の構え.
    private static readonly StateGreatAttackSuccess   _stanceSuccess = new();       // 必殺技成功.

    //--共通状態--//
    private static readonly StateDamage               _damage = new();              // ダメージを受けた.
    private static readonly StateDead                 _dead = new();                // やられた.

    // 現在のState.
    private StateBase                                 _currentState = _idle;

    void Start()
    {
        Initialization();
        _currentState.OnEnter(this, null);
    }

    void Update()
    {
        StateTransitionFlag();

        GetStickInput();
        AnimTransition();

        _currentState.OnUpdate(this);
        if(!_mainSceneManager.GetPauseStop())
        {
            _currentState.OnChangeState(this);
        }
        viewAngle();
        StateFlameManager();
        StateTime();
    }

    private void FixedUpdate()
    {
        
        SubstituteVariableFixedUpdate();
        _currentState.OnFixedUpdate(this);

        CameraFollowUpdate();

        // スタミナ.
        LimitStop(ref _stamina, ref _maxStamina);
        // 練気ゲージ.
        LimitStop(ref _currentRenkiGauge, ref _maxRenkiGauge);
        // 練気ゲージ赤.
        LimitStop(ref _currentRedRenkiGauge, ref _maxRedRenkiGauge);

        // 体力が0以下の時.
        if(_hitPoint <= 0)
        {
            OnDead();
        }

        // スタミナを回復させるタイミング指定.
        if(_currentState != _dash &&
            _currentState != _avoid &&
            _currentState != _fatigueDash)
        {
            AutoRecoveryStamina();
        }

        RenkiNaturalConsume();
        MaintainElapsedTimeRenkiGauge();
        StickDirection();
        ApplyRedRenkiGauge();
        OpenMenu();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // モンスターに当たっても浮かないようにする.
        if (collision.transform.tag == "Monster")
        {
            _transform.position = new Vector3 (_transform.position.x, 0.1f, _transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ダメージを受けつける.
        if(other.gameObject.tag == "MonsterAtCol" && _currentState != _damage)
        {
            // カウンターの受付をしていない時はダメージを受ける.
            if (!_counterValid)
            {
                OnDamage();
            }
            else if(_counterValid)
            {
                _counterSuccess = true;
            }
            
        }
    }
}