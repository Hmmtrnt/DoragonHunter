/*全体のプレイヤーステート*/

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
    private static readonly StateSheathingSword       _sheathingSword = new();      // 納刀する.
    private static readonly StateSteppingSlash        _steppingSlash = new();       // 踏み込み斬り.
    private static readonly StatePiercing             _piercing = new();            // 突き.
    private static readonly StateSlashUp              _slashUp = new();             // 斬り上げ.
    private static readonly StateSpiritBlade1         _spiritBlade1 = new();        // 気刃斬り1.
    private static readonly StateSpiritBlade2         _spiritBlade2 = new();        // 気刃斬り2.
    private static readonly StateSpiritBlade3         _spiritBlade3 = new();        // 気刃斬り3.
    private static readonly StateRoundSlash           _roundSlash = new();          // 気刃大回転斬り.

    //--共通状態--//
    private static readonly StateDead                 _dead = new();                // やられた.
    private static readonly StateDamage               _damage = new();              // ダメージを受けた.

    // 現在のState.
    private StateBase                                 _currentState = _idle;

    public GameObject _debugSphere;// スティックのやつ.

    void Start()
    {
        Initialization();
        _currentState.OnEnter(this, null);
    }

    void Update()
    {
        GetStickInput();
        AnimTransition();

        _currentState.OnUpdate(this);
        _currentState.OnChangeState(this);

        viewAngle();
    }

    private void FixedUpdate()
    {
        //_stateFlame++;

        StateFlameManager();

        //Debug.Assert(_text != null);

        SubstituteVariableFixedUpdate();
        _currentState.OnFixedUpdate(this);

        CameraFollowUpdate();
        GroundPenetrationDisable();

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
        //RedRenkiNaturalConsume();
        StickDirection();
        ApplyRedRenkiGauge();
        OpenMenu();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Monster")
        {
            //Debug.Log("Monster");
            transform.position = new Vector3 (transform.position.x, 0.1f, transform.position.z);
            //Debug.Log(collision.transform.tag);
        }
        else if (collision.transform.tag == "MonsterAtCol")
        {
            //Debug.Log("MonsterAtCol");
        }

        

    }

    private void OnTriggerEnter(Collider other)
    {
        // ダメージを受けつける.
        if(other.gameObject.tag == "MonsterAtCol" /*&& !_flameAvoid*/ && _currentState != _damage)
        {
            OnDamage();
            //Debug.Log("MonsterAtCol");
        }


        //else if (other.transform.tag == "MonsterHead")
        //{
        //    //Debug.Log("MonsterHead");
        //}
        //else if (other.transform.tag == "MonsterWingRight")
        //{
        //    //Debug.Log("MonsterWingRight");
        //}
        //else if (other.transform.tag == "MonsterWingLeft")
        //{
        //    //Debug.Log("MonsterWingLeft");
        //}
        //else if (other.transform.tag == "MonsterTail")
        //{
        //    //Debug.Log("MonsterTail");
        //}
        //else if (other.transform.tag == "MonsterBody")
        //{
        //    //Debug.Log("MonsterBody");
        //}
        
    }
}