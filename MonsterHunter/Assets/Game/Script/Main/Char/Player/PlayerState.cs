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
    private static readonly StateRightAvoidDrawSword  _rightAvoid = new();          // 攻撃後の右回避.
    private static readonly StateLeftAvoidDrawSword  _leftAvoid = new();            // 攻撃後の左回避.
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
        // 現在のステート情報.
        //Debug.Log(_currentState);
        //Debug.Log(_rigidbody.velocity.magnitude);

        SubstituteVariable();
        _currentState.OnFixedUpdate(this);

        // スタミナの上限、下限を超えないようにする.
        if(_stamina >= _maxStamina)
        {
            _stamina = _maxStamina;
        }
        if(_stamina < 0)
        {
            _stamina = 0;
        }
        // 乙処理.
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

        StickDirection();
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
        _Monster = GameObject.FindWithTag("Monster");
        _MonsterState = GameObject.FindWithTag("Monster").GetComponent<MonsterState>();
        _attackCol = GameObject.Find("AttackCollider").GetComponent<AttackCol>();
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
        _animator.SetBool("Fatigue", _fatigueMotion);
        _animator.SetBool("Avoid", _avoidMotion);
        _animator.SetBool("Heal", _healMotion);

        /*抜刀*/
        // bool
        _animator.SetBool("DrawnSword", _drawnSwordMotion);
        _animator.SetBool("DrawnIdle", _drawnIdleMotion);
        _animator.SetBool("DrawnRun", _drawnRunMotion);
        _animator.SetBool("DrawAvoid", _drawnAvoidMotion);
        _animator.SetBool("DrawRAvoid", _drawnRightAvoidMotion);

        _animator.SetBool("DrawLAvoid", _drawnLeftAvoidMotion);
        _animator.SetBool("SheathingSword", _drawnSheathingSword);
        _animator.SetBool("SteppingSlash", _drawnSteppingSlash);
        _animator.SetBool("Thrust", _drawnThrustSlash);
        _animator.SetBool("SlashUp", _drawnSlashUp);

        _animator.SetBool("SpiritBlade1", _drawnSpiritBlade1);
        _animator.SetBool("SpiritBlade2", _drawnSpiritBlade2);
        _animator.SetBool("SpiritBlade3", _drawnSpiritBlade3);
        _animator.SetBool("SpiritRoundSlash", _drawnSpiritRoundSlash);

        /*共通*/
        // bool
        _animator.SetBool("Damage", _damageMotion);
        _animator.SetBool("Down", _downMotion);
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
        //_avoidVelocity = _transform.forward * _avoidVelocityMagnification;

        _debugSphere.transform.position = new Vector3(transform.position.x + _moveVelocity.x * 5, transform.position.y, transform.position.z + _moveVelocity.z * 5);

    }

    private void GetStickDirection()
    {
        _stickDirection = transform.position - _debugSphere.transform.position;
    }

    // スティックがハンターから見てどの向きを向いているか
    private void StickDirection()
    {
        // 正面
        if (_viewDirection[(int)viewDirection.FORWARD])
        {
            _text.text = "正面";
        }
        // 背後
        else if (_viewDirection[(int)viewDirection.BACKWARD])
        {
            _text.text = "背後";
        }
        // 右
        else if (_viewDirection[(int)viewDirection.RIGHT])
        {
            _text.text = "右";
        }
        // 左
        else if (_viewDirection[(int)viewDirection.LEFT])
        {
            _text.text = "左";
        }
    }


    // プレイヤーの視野角
    private void viewAngle()
    {
        Vector3 direction = _debugSphere.transform.position - _transform.position;
        // ハンターとデバッグ用キューブのベクトルのなす角
        // デバッグ用キューブの正面.
        float forwardAngle = Vector3.Angle(direction, _transform.forward);
        // オブジェクトの側面,
        float sideAngle = Vector3.Angle(direction, _transform.right);

        RaycastHit hit;
        bool ray = Physics.Raycast(_transform.position, direction.normalized, out hit);

        bool viewFlag = ray && hit.collider.gameObject == _debugSphere && GetDistance() > 1;

        //Debug.Log(viewFlag);
        if (!viewFlag) return;

        // 正面.
        if (forwardAngle < 90 * 0.5f)
        {
            FoundFlag((int)viewDirection.FORWARD);
            _text.text = "正面";
        }
        // 後ろ.
        else if (forwardAngle > 135 && forwardAngle < 180)
        {
            FoundFlag((int)viewDirection.BACKWARD);
            _text.text = "後ろ";
        }
        // 右.
        else if (sideAngle < 90 * 0.5f)
        {
            FoundFlag((int)viewDirection.RIGHT);
            _text.text = "右";
        }
        // 左.
        else if (sideAngle > 135 && sideAngle < 180)
        {
            FoundFlag((int)viewDirection.LEFT);
            _text.text = "左";
        }
        else
        {
            FoundFlag((int)viewDirection.NONE);
            _text.text = "NONE";
        }



    }

    /// <summary>
    /// スティックがいる位置をtureで返す
    /// </summary>
    /// <param name="foundNum">スティックの位置を示す番号</param>
    private void FoundFlag(int foundNum)
    {
        for (int i = 0; i < (int)viewDirection.NONE; i++)
        {
            if (i == foundNum)
            {
                _viewDirection[i] = true;
            }
            else
            {
                _viewDirection[i] = false;
            }
        }
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

    // ダメージを与えられるかどうか.
    public bool GetIsCauseDamage() { return _isCauseDamage; }

    // 距離
    private float GetDistance()
    {
        _currentDistance = (_debugSphere.transform.position - _transform.position).magnitude;
        return _currentDistance;
    }

}