/*プレイヤーステート*/

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
    private static readonly StateDrawnSwordTransition _drawSwordTransition = new(); // 抜刀している.
    private static readonly StateIdleDrawnSword       _idleDrawnSword = new();      // アイドル.
    private static readonly StateRunDrawnSword        _runDrawnSword = new();       // 走る.
    private static readonly StateAvoidDrawSword       _avoidDrawnSword = new();     // 抜刀回避.
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

    void Start()
    {
        Initialization();
        _AtCol.SetActive(false);
        _currentState.OnEnter(this, null);
    }

    void Update()
    {
        GetStickInput();
        AnimTransition();
        
        _currentState.OnUpdate(this);
        _currentState.OnChangeState(this);

        //Debug.Log(_unsheathedSword);

        if(_input._LeftStickHorizontal == 0.0f && _input._LeftStickVertical == 0.0f)
        {
            _rigidbody.velocity *= 0.8f;
        }
    }

    private void FixedUpdate()
    {
        SubstituteVariable();
        _currentState.OnFixedUpdate(this);

        // スタミナの上限、下限を超えないようにする
        if(_stamina >= _maxStamina)
        {
            _stamina = _maxStamina;
        }
        if(_stamina < 0)
        {
            _stamina = 0;
        }

        if(_hitPoint <= 0)
        {
            OnDead();
        }

        if(_currentState != _dash &&
            _currentState != _avoid &&
            _currentState != _fatigueDash)
        {
            AutoRecoveryStamina();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.transform.tag == "Monster")
        //{
        //    OnDamage();
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MonsterAtCol")
        {
            OnDamage();
        }
    }

    // ステート変更.
    private void ChangeState(StateBase nextState)
    {
        _currentState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentState);
        _currentState = nextState;
    }

    // プレイヤー情報の初期化.
    private void Initialization()
    {
        _input = GameObject.FindWithTag("Manager").GetComponent<ControllerManager>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _AtCol = GameObject.Find("AttackCol");
        _Monster = GameObject.FindWithTag("Monster");
        _MonsterState = GameObject.FindWithTag("Monster").GetComponent<MonsterState>();
    }

    // アニメーション遷移.
    private void AnimTransition()
    {
        if (_animator == null) return;

        /*納刀*/
        // float
        _animator.SetFloat("Speed", _currentRunSpeed);

        // bool
        _animator.SetBool("Idle", _idleMotion);
        _animator.SetBool("Run", _runMotion);
        _animator.SetBool("Dash", _dashMotion);
        _animator.SetBool("Avoid", _avoidMotion);
        _animator.SetBool("Heal", _healMotion);

        /*抜刀*/
        // bool
        _animator.SetBool("DrawnSword", _drawnSwordMotion);
        _animator.SetBool("DrawnIdle", _drawnIdleMotion);
        _animator.SetBool("SteppingSlash", _drawnSteppingSlash);

        /*共通*/
        // bool
        _animator.SetBool("Damage", _damageMotion);
    }

    // 情報の代入.
    private void SubstituteVariable()
    {
        // 動く方向代入.
        _moveDirection = new Vector3(_leftStickHorizontal, 0.0f, _leftStickVertical);
        _moveDirection.Normalize();

        // カメラの正面.
        _cameraForward = Vector3.Scale(_camera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;

        /*カメラの向きから移動方向取得*/
        // 正面.
        Vector3 moveForward = _cameraForward * _leftStickVertical;
        // 横.
        Vector3 moveSide = _camera.transform.right * _leftStickHorizontal;
        // 速度の代入.
        _moveVelocity = moveForward + moveSide;
        _avoidVelocity = _transform.forward * _avoidVelocityMagnification;

    }

    // スタミナの自動回復.
    private void AutoRecoveryStamina()
    {
        _stamina += _autoRecaveryStamina;
    }

    // ダメージを受けた時に遷移.
    private void OnDamage()
    {
        if(_hitPoint <= 0) return;

        _hitPoint = _hitPoint - _MonsterState.GetMonsterAttack();

        ChangeState(_damage);
    }

    // 体力が0になった時に呼び出す.
    private void OnDead()
    {
        ChangeState(_dead);
    }

    // 左スティックの入力情報取得.
    private void GetStickInput()
    {
        // 入力情報代入.
        _leftStickHorizontal = _input._LeftStickHorizontal;
        _leftStickVertical = _input._LeftStickVertical;
    }

    // 移動時の回転処理.
    private void RotateDirection()
    {
        transform.forward = Vector3.Slerp(transform.forward, _moveVelocity, Time.deltaTime * _rotateSpeed);
    }

    // ダッシュしているかどうかの情報取得.
    public bool GetIsDashing() { return _isDashing; }

    // 回避フレームの数を取得.
    public int GetAvoidTime() { return _avoidTime; }

    // 回避しているかどうかの情報取得.
    public bool GetIsAvoiding() { return _isAvoiding; }

    // 回復している時間取得.
    public int GetRecoveryTime() { return _currentRecoveryTime; }

    // 回復しているかどうかの情報取得.
    public bool GetIsRecovery() { return _isRecovery; }

    // 残り体力.
    public float GetHitPoint() { return _hitPoint; }
    // 体力最大値.
    public float GetMaxHitPoint() { return _maxHitPoint; }
    // 残りスタミナ.
    public float GetStamina() { return _stamina; }
    // スタミナ最大値.
    public float GetMaxStamina() { return _maxStamina; }

    // ダメージを与えた時の値.
    public float GetHunterAttack() { return _AttackPower; }

}