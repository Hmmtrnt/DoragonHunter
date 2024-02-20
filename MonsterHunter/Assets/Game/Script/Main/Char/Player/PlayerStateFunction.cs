/*プレイヤーステートの関数まとめ*/

using UnityEngine;

public partial class Player
{
    /// <summary>
    /// プレイヤー情報の初期化.
    /// </summary>
    private void Initialization()
    {
        _input = GameObject.FindWithTag("Manager").GetComponent<ControllerManager>();
        _mainSceneSelectUi = GameObject.Find("SelectItem").GetComponent<MainSceneMenuSelectUi>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _Monster = GameObject.FindWithTag("Monster");
        _MonsterState = GameObject.FindWithTag("Monster").GetComponent<Monster>();
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
        _weaponObject.SetActive(false);
        _weaponActive = false;
        _cureMedicineNum = 10;
    }

    /// <summary>
    /// 状態遷移時の初期化.
    /// </summary>
    private void StateTransitionInitialization()
    {
        _stateFlame = 0;
        _maintainTime = 100;
    }

    // まだ使う予定なし.
    /// <summary>
    /// 状態終了時の初期化.
    /// </summary>
    private void StateTransitionEnd()
    {

    }

    /// <summary>
    /// 状態遷移.
    /// </summary>
    /// <param name="nextState">次に遷移する状態</param>
    private void StateTransition(StateBase nextState)
    {
        // 状態終了時.
        _currentState.OnExit(this, nextState);
        // 次の状態の呼び出し.
        nextState.OnEnter(this, _currentState);
        // 次に遷移する状態の代入.
        _currentState = nextState;
    }

    /// <summary>
    /// アニメーション遷移.
    /// </summary>
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
        _animator.SetBool("DrawBAvoid", _drawnBackAvoidMotion);
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

    /// <summary>
    /// 現在の状態のフレーム数管理.
    /// </summary>
    private void StateFlameManager()
    {
        if(!_currentHitStop)
        {
            _stateFlame++;
        }
    }

    /// <summary>
    /// 一定に情報を代入.
    /// </summary>
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
        //Debug.Log(_currentRenkiGauge);

        RenkiGaugeDraw();
    }

    // 現在使用していない.
    /// <summary>
    /// カメラの注視点の挙動.
    /// </summary>
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

    /// <summary>
    /// スティックがオブジェクトから見てどの向きに倒しているかを確認.
    /// </summary>
    private void StickDirection()
    {
        // 正面.
        if (_viewDirection[(int)viewDirection.FORWARD])
        {
            //_text.text = "正面";
            
        }
        // 背後.
        else if (_viewDirection[(int)viewDirection.BACKWARD])
        {
            //_text.text = "背後";
        }
        // 右.
        else if (_viewDirection[(int)viewDirection.RIGHT])
        {
            //_text.text = "右";
        }
        // 左.
        else if (_viewDirection[(int)viewDirection.LEFT])
        {
            //_text.text = "左";
        }
        else
        {
            //_text.text = "NONE";
        }
    }

    /// <summary>
    /// 練気ゲージ赤になると効果を適用.
    /// </summary>
    private void ApplyRedRenkiGauge()
    {
        if (_currentRenkiGauge > 75)
        {
            _applyRedRenkiGauge = true;
        }
        else if(_currentRenkiGauge < 75)
        {
            _applyRedRenkiGauge = false;
        }
    }

    /// <summary>
    /// プレイヤーの視野角.
    /// </summary>
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

        // 正面.
        if (forwardAngle < 45)
        {
            FoundFlag((int)viewDirection.FORWARD);
        }
        // 後ろ.
        else if (forwardAngle > 135)
        {
            FoundFlag((int)viewDirection.BACKWARD);
        }
        // 右.
        else if (sideAngle < 45)
        {
            FoundFlag((int)viewDirection.RIGHT);
        }
        // 左.
        else if (sideAngle > 135)
        {
            FoundFlag((int)viewDirection.LEFT);
        }
        // もし、
        else if(forwardAngle == 0 && sideAngle == 0)
        {
            FoundFlag((int)viewDirection.NONE);
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

    /// <summary>
    /// スタミナの自動回復.
    /// </summary>
    private void AutoRecoveryStamina()
    {
        _stamina += _autoRecaveryStamina;
    }

    /// <summary>
    /// 回避時の移動
    /// </summary>
    private void MoveAvoid()
    {
        // 減速.
        if (_avoidTime <= 15)
        {
            _rigidbody.velocity *= _deceleration;
        }

        // 一気に減速.
        if (_avoidTime >= 55)
        {
            _rigidbody.velocity *= 0.8f;
        }

        // 一度処理を通すと次は通さないようにする.
        if (!_isProcess) return;

        // 回避するときの力を加える.
        _rigidbody.AddForce(_avoidVelocity, ForceMode.Impulse);

        _isProcess = false;

    }

    /// <summary>
    /// 錬気ゲージ自然消費.
    /// </summary>
    private void RenkiNaturalConsume()
    {
        // 練気ゲージが下限突破や維持時間以外に消費.
        if (_currentRenkiGauge <= 0 || _maintainTimeRenkiGauge > 0) return;
        _currentRenkiGauge -= 0.03f;
    }

    /// <summary>
    /// 練気ゲージ維持時間の経過.
    /// </summary>
    private void MaintainElapsedTimeRenkiGauge()
    {
        if(_maintainTimeRenkiGauge <= 0) return;
        _maintainTimeRenkiGauge--;
    }

    // 使っていない.
    /// <summary>
    /// 練気ゲージ赤自然消費.
    /// </summary>
    private void RedRenkiNaturalConsume()
    {
        // 練気ゲージ赤が下限突破以外に消費.
        if(_currentRedRenkiGauge <= 0) return;
        _currentRedRenkiGauge -= 0.05f;
    }

    /// <summary>
    /// 錬気ゲージの増減処理.
    /// </summary>
    public void RenkiGaugeFluctuation()
    {
        if (GetIsCauseDamage())
        {
            // 錬気ゲージ増加.
            _currentRenkiGauge += _increaseAmountRenkiGauge;
            // しばらく練気ゲージを減らさない.
            _maintainTimeRenkiGauge = _maintainTime;
        }
    }

    /// <summary>
    /// 練気ゲージ赤の表示.
    /// </summary>
    private void RenkiGaugeDraw()
    {
        // 錬気ゲージ赤のバフを適用した際に表示.
        if(_applyRedRenkiGauge)
        {
            _currentRedRenkiGauge = 100;
        }
        else
        {
            _currentRedRenkiGauge = 0;
        }
    }

    /// <summary>
    /// 変数の上限下限を突破しない.
    /// </summary>
    /// <param name="currentVariable">現在の変数</param>
    /// <param name="maxVariable">変数上限値</param>
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
    
    /// <summary>
    /// モーション中の前進.
    /// </summary>
    /// <param name="speedPower">前進する力</param>
    private void ForwardStep(float speedPower)
    {
        _rigidbody.velocity = _transform.forward * speedPower;
    }

    /// <summary>
    /// ダメージを受けた時に体力を減らし状態遷移.
    /// </summary>
    private void OnDamage()
    {
        // 体力が0の時は通さない.
        if (_hitPoint <= 0) return;

        _hitPoint = _hitPoint - _MonsterState.GetMonsterAttack();

        StateTransition(_damage);
    }

    /// <summary>
    /// 体力が0になった時に死亡状態へ.
    /// </summary>
    private void OnDead()
    {
        StateTransition(_dead);
    }

    /// <summary>
    /// 左スティックの入力情報取得.
    /// </summary>
    private void GetStickInput()
    {
        // 入力情報代入.
        _leftStickHorizontal = _input._LeftStickHorizontal;
        _leftStickVertical = _input._LeftStickVertical;
    }

    /// <summary>
    /// 移動時の回転処理.
    /// </summary>
    private void RotateDirection()
    {
        transform.forward = Vector3.Slerp(transform.forward, _moveVelocity, Time.deltaTime * _rotateSpeed);
    }

    /// <summary>
    /// SEを鳴らすときの処理.
    /// </summary>
    /// <param name="flameNum1">鳴らすフレーム数</param>
    /// <param name="seName">SEの種類</param>
    private void SEPlay(int flameNum1, int seName)
    {
        if(_stateFlame == flameNum1)
        {
            _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, seName);
        }
    }

    /// <summary>
    /// SEを鳴らすときの処理.
    /// </summary>
    /// <param name="flameNum1">一回目の鳴らすフレーム数</param>
    /// <param name="flameNum2">二回目の鳴らすフレーム数</param>
    /// <param name="seName">SEの種類</param>
    private void SEPlay(int flameNum1, int flameNum2, int seName)
    {
        if (_stateFlame == flameNum1 ||
            _stateFlame == flameNum2)
        {
            _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, seName);
        }
    }

    /// <summary>
    /// SEを鳴らすときの処理.
    /// </summary>
    /// <param name="flameNum1">一回目の鳴らすフレーム数</param>
    /// <param name="flameNum2">二回目の鳴らすフレーム数</param>
    /// <param name="flameNum3">三回目の鳴らすフレーム数</param>
    /// <param name="seName">SEの種類</param>
    private void SEPlay(int flameNum1, int flameNum2, int flameNum3, int seName)
    {
        if (_stateFlame == flameNum1 ||
            _stateFlame == flameNum2 ||
            _stateFlame == flameNum3)
        {
            _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, seName);
        }
    }

    /// <summary>
    /// メニューを開いているかの情報を代入.
    /// </summary>
    private void OpenMenu()
    {
        _openMenu = _mainSceneManager.GetOpenMenu();
    }

    // 
    /// <summary>
    /// ダッシュしているかどうかの情報取得.
    /// </summary>
    /// <returns>ダッシュしているかどうか</returns>
    public bool GetIsDashing() { return _isDashing; }

    /// <summary>
    /// 回避フレームの数を取得.
    /// </summary>
    /// <returns>回避しているときのフレーム数</returns>
    public int GetAvoidTime() { return _avoidTime; }

    /// <summary>
    /// 回避しているかどうかの情報取得.
    /// </summary>
    /// <returns>回避をしているかどうか</returns>
    public bool GetIsAvoiding() { return _isAvoiding; }

    /// <summary>
    /// 回復している時間取得.
    /// </summary>
    /// <returns>回復している時間</returns>
    public int GetRecoveryTime() { return _currentRecoveryTime; }

    /// <summary>
    /// 回復しているかどうかの情報取得.
    /// </summary>
    /// <returns>回復しているかどうか</returns>
    public bool GetIsRecovery() { return _isRecovery; }

    /// <summary>
    /// 残り体力.
    /// </summary>
    /// <returns>体力</returns>
    public float GetHitPoint() { return _hitPoint; }

    /// <summary>
    /// 体力最大値.
    /// </summary>
    /// <returns>体力の最大値</returns>
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

    /// <summary>
    /// スティックの傾きによって距離を求める. 
    /// </summary>
    /// <returns>スティックの傾きとプレイヤーの距離</returns>
    private float GetDistance()
    {
        _currentDistance = (_debugSphere.transform.position - _transform.position).magnitude;
        return _currentDistance;
    }

    // 回復薬の数を取得.
    public int GetCureMedicineNum() { return _cureMedicineNum; }
}
