/*プレイヤーステートの関数まとめ*/

using UnityEngine;

public partial class PlayerState
{
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
        _weaponObject.SetActive(false);
        _weaponActive = false;
    }

    // 状態遷移時の初期化.
    private void StateTransitionInitialization()
    {
        _stateFlame = 0;
        _maintainTime = 100;
    }

    /// <summary>
    /// ステート変更.
    /// </summary>
    /// <param name="nextState">次の変更する状態</param>
    private void ChangeState(StateBase nextState)
    {
        _currentState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentState);
        _currentState = nextState;
    }

    // アニメーション遷移.
    private void AnimTransition()
    {
        // アニメーターがアタッチされていなければスキップ.
        if (_animator == null) return;

        /*納刀*/
        // float.
        _animator.SetFloat("Speed", _currentRunSpeed);

        // bool.
        _animator.SetBool("Idle", _idleMotion);
        _animator.SetBool("Run", _runMotion);
        _animator.SetBool("Dash", _dashMotion);
        _animator.SetBool("Fatigue", _fatigueMotion);
        _animator.SetBool("Avoid", _avoidMotion);
        _animator.SetBool("Heal", _healMotion);

        /*抜刀*/
        // bool.
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
        // bool.
        _animator.SetBool("Damage", _damageMotion);
        _animator.SetBool("Down", _downMotion);
    }

    // 一定に情報を代入.
    private void SubstituteVariableFixedUpdate()
    {

        // カメラの正面.
        _cameraForward = Vector3.Scale(_camera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;

        /*カメラの向きから移動方向取得*/
        // 正面.
        Vector3 moveForward = _cameraForward * _leftStickVertical;
        // 横.
        Vector3 moveSide = _camera.transform.right * _leftStickHorizontal;
        // 速度の代入.
        _moveVelocity = moveForward + moveSide;
        //スティックがどの方向に傾いているかを取得.
        _debugSphere.transform.position = new Vector3(transform.position.x + _moveVelocity.x * 5, transform.position.y, transform.position.z + _moveVelocity.z * 5);
        // 武器の当たり判定の表示非表示.
        _weaponObject.SetActive(_weaponActive);
        // 攻撃力の代入.
        _attackDamage = _attackPower * _MonsterFleshy;
        // 練気ゲージ赤適用した時に攻撃力を上昇させる.
        if(_applyRedRenkiGauge)
        {
            _attackDamage *= 1.12f;
        }

    }

    // カメラの注視点の挙動.
    private void CameraFollowUpdate()
    {
        //if(_unsheathedSword)
        //{
        //    if(_cameraFollow.transform.position.z <= _transform.position.z + 0.3f )
        //    {
        //        _cameraFollow.transform.position += new Vector3(0.0f, 0.0f, 0.01f);
        //    }
        //}
        //else
        //{
        //    if (_cameraFollow.transform.position.z >= _transform.position.z)
        //    {
        //        _cameraFollow.transform.position -= new Vector3(0.0f, 0.0f, 0.01f);
        //    }
        //}
    }

    // スティックがハンターから見てどの向きを向いているか.
    private void StickDirection()
    {
        // 正面.
        if (_viewDirection[(int)viewDirection.FORWARD])
        {
            _text.text = "正面";
            
        }
        // 背後.
        else if (_viewDirection[(int)viewDirection.BACKWARD])
        {
            _text.text = "背後";
        }
        // 右.
        else if (_viewDirection[(int)viewDirection.RIGHT])
        {
            _text.text = "右";
        }
        // 左.
        else if (_viewDirection[(int)viewDirection.LEFT])
        {
            _text.text = "左";
        }
        else
        {
            _text.text = "NONE";
        }
    }

    // 練気ゲージ赤になると効果を適用.
    private void ApplyRedRenkiGauge()
    {
        if (_currentRenkiGauge > 75)
        {
            //_currentRedRenkiGauge = 100;
            _applyRedRenkiGauge = true;
        }
        else if(_currentRenkiGauge < 75)
        {
            _applyRedRenkiGauge = false;
        }

        //Debug.Log(_applyRedRenkiGauge);
    }

    // プレイヤーの視野角.
    private void viewAngle()
    {
        Vector3 direction = _debugSphere.transform.position - _transform.position;
        // ハンターとデバッグ用キューブのベクトルのなす角.
        // デバッグ用キューブの正面.
        float forwardAngle = Vector3.Angle(direction, _transform.forward);
        // オブジェクトの側面.
        float sideAngle = Vector3.Angle(direction, _transform.right);

        RaycastHit hit;
        bool ray = Physics.Raycast(_transform.position, direction.normalized, out hit);

        bool viewFlag = ray && hit.collider.gameObject == _debugSphere && GetDistance() > 1;


        //Debug.Log(viewFlag);
        //if (!viewFlag) return;

        // 正面.
        if (forwardAngle < 90 * 0.5f)
        {
            FoundFlag((int)viewDirection.FORWARD);
            //_text.text = "正面";
        }
        // 後ろ.
        else if (forwardAngle > 135 && forwardAngle < 180)
        {
            FoundFlag((int)viewDirection.BACKWARD);
            //_text.text = "後ろ";
        }
        // 右.
        else if (sideAngle < 90 * 0.5f)
        {
            FoundFlag((int)viewDirection.RIGHT);
            //_text.text = "右";
        }
        // 左.
        else if (sideAngle > 135 && sideAngle < 180)
        {
            FoundFlag((int)viewDirection.LEFT);
            //_text.text = "左";
        }
        else
        {
            FoundFlag((int)viewDirection.NONE);
            //_text.text = "NONE";
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

    // 錬気ゲージ自然消費.
    private void RenkiNaturalConsume()
    {
        // 練気ゲージが下限突破や維持時間以外に消費.
        if (_currentRenkiGauge <= 0 || _maintainTimeRenkiGauge > 0) return;
        _currentRenkiGauge -= 0.03f;
    }

    // 練気ゲージ維持時間の経過.
    private void MaintainElapsedTimeRenkiGauge()
    {
        if(_maintainTimeRenkiGauge <= 0) return;
        _maintainTimeRenkiGauge--;
    }

    // 練気ゲージ赤自然消費.
    private void RedRenkiNaturalConsume()
    {
        // 練気ゲージ赤が下限突破以外に消費.
        if(_currentRedRenkiGauge <= 0) return;
        _currentRedRenkiGauge -= 0.05f;
    }

    // 変数の上限下限を突破しない.
    private void LimitStop(ref float currentVariable, ref float maxVariable)
    {
        // 上限突破防止.
        if (currentVariable >= maxVariable)
        {
            currentVariable = maxVariable;
        }
        // 下限突破防止.
        if (currentVariable < 0)
        {
            currentVariable = 0;
        }
    }
    

    // 前進する.
    private void ForwardStep(float speedPower)
    {
        _rigidbody.velocity = _transform.forward * speedPower;
    }

    // ダメージを受けた時に遷移.
    private void OnDamage()
    {
        if (_hitPoint <= 0) return;

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

    // 地面貫通無効
    private void GroundPenetrationDisable()
    {
        if(_transform.position.y < 0)
        {
            _transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }
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
    public float GetHunterAttack() { return _attackDamage; }

    // ダメージを与えられるかどうか.
    public bool GetIsCauseDamage() { return _isCauseDamage; }
    // ダメージを与えられるかの代入.
    public void SetIsCauseDamage(bool causeDamage) { _isCauseDamage = causeDamage; }

    // 最大錬気ゲージ.
    public float GetMaxRenkiGauge() { return _maxRenkiGauge; }
    // 現在の錬気ゲージ.
    public float GetCurrentRenkiGauge() { return _currentRenkiGauge; }
    // 最大練気ゲージ赤.
    public float GetMaxRedRenkiGauge() { return _maxRedRenkiGauge; }
    // 現在の練気ゲージ赤.
    public float GetCurrentRedRenkiGauge() { return _currentRedRenkiGauge; }
    // 気刃大回転斬りを行っている途中.
    public bool GetRoundSlash() { return _drawnSpiritRoundSlash; }

    // スティックの傾きによって距離を求める.
    private float GetDistance()
    {
        _currentDistance = (_debugSphere.transform.position - _transform.position).magnitude;
        return _currentDistance;
    }
}
