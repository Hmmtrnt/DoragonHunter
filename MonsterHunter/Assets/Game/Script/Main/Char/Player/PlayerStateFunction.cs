/*プレイヤーステートの関数まとめ*/

using UnityEngine;

public partial class PlayerState
{
    // 以下の5個の関数の処理順を変更してはいけない.

    /// <summary>
    /// 初期化時の処理.
    /// </summary>
    private void Init()
    {
        VariableInitialization();
        _currentState.OnEnter(this, null);
    }

    /// <summary>
    /// 更新処理.
    /// </summary>
    private void UpdateProcess()
    {
        StateTransitionFlag();

        GetStickInput();
        AnimTransition();

        _currentState.OnUpdate(this);
        if (!_huntingSceneManager.GetPauseStop())
        {
            _currentState.OnChangeState(this);
        }
        viewAngle();
        StateFlameManager();
        StateTime();
    }

    /// <summary>
    /// 50fps固定更新処理.
    /// </summary>
    private void FixedUpdateProcess()
    {
        SubstituteVariableFixedUpdate();

        // スタミナ.
        LimitStop(ref _stamina, _maxStamina);
        // 練気ゲージ.
        LimitStop(ref _currentRenkiGauge, _maxRenkiGauge);
        // 練気ゲージ赤.
        LimitStop(ref _currentRedRenkiGauge, _maxRedRenkiGauge);

        // 体力が0以下の時.
        if (_currentHitPoint <= 0)
        {
            OnDead();
        }

        // スタミナを回復させるタイミング指定.
        if (_autoRecaveryStaminaFlag)
        {
            AutoRecoveryStamina();
        }

        RenkiNaturalConsume();
        MaintainElapsedTimeRenkiGauge();
        ApplyRedRenkiGauge();
        OpenMenu();
    }

    /// <summary>
    /// 当たり判定に当たった瞬間の処理.
    /// </summary>
    /// <param name="collision">当たり判定の対象</param>
    private void ColEnter(Collision collision)
    {
        // モンスターに当たっても浮かないようにする.
        if (collision.transform.tag == "Monster")
        {
            _transform.position = new Vector3(_transform.position.x, 0.1f, _transform.position.z);
        }
    }

    /// <summary>
    /// 当たり判定が貫通した瞬間の処理.
    /// </summary>
    /// <param name="other">当たり判定の対象</param>
    private void TriggerEnter(Collider other)
    {
        // ダメージを受けつける.
        if (other.gameObject.tag == "MonsterAtCol" && _currentState != _damage)
        {
            // カウンターの受付をしていない時はダメージを受ける.
            if (!_counterValid)
            {
                OnDamage();
            }
            else if (_counterValid)
            {
                _counterSuccess = true;
            }

        }
    }


    /// <summary>
    /// プレイヤー情報の初期化.
    /// </summary>
    private void VariableInitialization()
    {
        _input = GameObject.FindWithTag("Manager").GetComponent<ControllerManager>();
        _mainSceneSelectUi = GameObject.Find("SelectItem").GetComponent<MainSceneMenuSelectUi>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _Monster = GameObject.FindWithTag("Monster");
        _MonsterState = GameObject.FindWithTag("Monster").GetComponent<MonsterState>();
        _huntingSceneManager = GameObject.Find("GameManager").GetComponent<HuntingSceneManager>();
        _attackCol = GameObject.Find("AttackCollider").GetComponent<AttackCol>();
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

    /// <summary>
    /// 状態遷移のフラグの代入.
    /// </summary>
    private void StateTransitionFlag()
    {
        /*以下、納刀時の状態*/
        // 待機状態.
        _stateTransitionFlag[(int)StateTransitionKinds.IDLE] = _leftStickHorizontal == 0 && _leftStickVertical == 0;
        // 回避状態.
        _stateTransitionFlag[(int)StateTransitionKinds.AVOID] = (_leftStickHorizontal != 0 || _leftStickVertical != 0) &&
            _stamina >= _avoidStaminaCost && _input._AButtonDown;
        // 走る状態.
        _stateTransitionFlag[(int)StateTransitionKinds.RUN] = (_leftStickHorizontal != 0 || _leftStickVertical != 0) && 
            !_input._RBButton;
        // ダッシュ状態.
        _stateTransitionFlag[(int)StateTransitionKinds.DASH] = (_leftStickHorizontal != 0 || _leftStickVertical != 0) &&
            _input._RBButton;
        // 疲労ダッシュ状態.
        _stateTransitionFlag[(int)StateTransitionKinds.FATIGUEDASH] = _stamina <= _maxStamina / 5;
        // 回復状態.
        _stateTransitionFlag[(int)StateTransitionKinds.RECOVERY] = _input._XButtonDown && !_unsheathedSword &&
            _currentHitPoint != _maxHitPoint && _cureMedicineNum > 0;

        /*以下、抜刀時の状態*/
        // 抜刀する状態.
        _stateTransitionFlag[(int)StateTransitionKinds.DRAWSWORDTRANSITION] = _input._YButtonDown;
        // 待機状態.
        _stateTransitionFlag[(int)StateTransitionKinds.DRAWIDLE] = _leftStickHorizontal == 0 && _leftStickVertical == 0;
        // 走る状態.
        _stateTransitionFlag[(int)StateTransitionKinds.DRAWRUN] = _leftStickHorizontal != 0 || _leftStickVertical != 0;
        // 前回避状態.
        _stateTransitionFlag[(int)StateTransitionKinds.DRAWAVOID] = (_leftStickHorizontal != 0 || _leftStickVertical != 0) &&
            _input._AButtonDown;
        // 右回避状態.
        _stateTransitionFlag[(int)StateTransitionKinds.RIGHTAVOID] = _viewDirection[(int)viewDirection.RIGHT] &&
                GetDistance() > 0.5f && _stamina >= _avoidStaminaCost &&
                _input._AButtonDown;
        // 左回避状態.
        _stateTransitionFlag[(int)StateTransitionKinds.LEFTAVOID] = _viewDirection[(int)viewDirection.LEFT] &&
            GetDistance() > 0.5f && _stamina >= _avoidStaminaCost &&
            _input._AButtonDown;
        // 後ろ回避状態.
        _stateTransitionFlag[(int)StateTransitionKinds.BACKAVOID] = _viewDirection[(int)viewDirection.BACKWARD] &&
            GetDistance() > 0.5f && _stamina >= _avoidStaminaCost &&
            _input._AButtonDown;
        // 納刀する状態.
        _stateTransitionFlag[(int)StateTransitionKinds.SHEATHINGSWORD] = _input._XButtonDown || _input._RBButtonDown;
        // 踏み込み斬り状態.
        _stateTransitionFlag[(int)StateTransitionKinds.STEPPINGSLASH] = _input._YButtonDown;
        // 突き状態.
        _stateTransitionFlag[(int)StateTransitionKinds.PRICK] = _input._BButtonDown && !_input._LBButton;
        // 切り上げ状態.
        _stateTransitionFlag[(int)StateTransitionKinds.SLASHUP] = _input._BButtonDown || _input._YButtonDown;
        // 気刃斬り1状態.
        _stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1] = _input._RightTrigger >= 0.5;
        // 気刃斬り2状態.
        _stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE2] = _input._RightTrigger >= 0.5;
        // 気刃斬り3状態.
        _stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE3] = _input._RightTrigger >= 0.5;
        // 気刃大回転斬り状態.
        _stateTransitionFlag[(int)StateTransitionKinds.ROUNDSLASH] = _input._RightTrigger >= 0.5;
        // 大技の構え状態.
        _stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSTANCE] = _input._LBButton && _input._BButtonDown &&
            _applyRedRenkiGauge;
        // 大技の成功状態.
        _stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSUCCESS] = _counterSuccess;

        /*納刀、抜刀状態両方の共通の状態*/
        // 体力が0いかになった状態.
        _stateTransitionFlag[(int)StateTransitionKinds.DEAD] = _currentHitPoint <= 0;
    }

    /// <summary>
    /// 状態遷移.
    /// </summary>
    /// <param name="nextState">次に遷移する状態</param>
    private void StateTransition(StateBase nextState)
    {
        // 状態終了時.
        _currentState.OnExit(this, nextState);
        // 各状態の時間をリセット.
        VariableReset();
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
        _animator.SetBool("GreatAttackStance", _greatAttackStanceMotion);
        _animator.SetBool("GreatAttackSuccess", _greatAttackSuccess);

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
    /// 各状態の経過時間を計測.
    /// </summary>
    private void StateTime()
    {
        if(!_currentHitStop)
        {
            _stateTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// 変数の値をリセット.
    /// </summary>
    private void VariableReset()
    {
        _stateTime = 0;
        _isPlayOneShot = false;
    }

    /// <summary>
    /// X軸、Y軸の回転を固定.
    /// </summary>
    private void FixedRotate()
    {
        _transform.Rotate(0, _transform.rotation.y, 0);
    }

    /// <summary>
    /// 一定に情報を代入.
    /// </summary>
    private void SubstituteVariableFixedUpdate()
    {
        //_stickPosition.FixPosition(_stickPositionObject.transform.position, _transform.position);


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
        _stickPositionObject.transform.position = new Vector3(transform.position.x + _moveVelocity.x * 5, transform.position.y, transform.position.z + _moveVelocity.z * 5);
        // 武器の当たり判定の表示非表示.
        _weaponObject.SetActive(_weaponActive);
        // 攻撃力の代入.
        _attackDamage = _attackPower * _MonsterFleshy;
        // 練気ゲージ赤適用した時に攻撃力を上昇させる.
        if(_applyRedRenkiGauge)
        {
            _attackDamage *= 1.12f;
        }

        // スタミナの自動回復を行うかどうか.
        _autoRecaveryStaminaFlag = _currentState != _dash &&
            _currentState != _avoid &&
            _currentState != _fatigueDash &&
            _currentState != _avoidDrawnSword &&
            _currentState != _rightAvoid &&
            _currentState != _leftAvoid &&
            _currentState != _backAvoid;

        RenkiGaugeDraw();
    }

    

    /// <summary>
    /// 練気ゲージ赤になると効果を適用.
    /// </summary>
    private void ApplyRedRenkiGauge()
    {
        if (_currentRenkiGauge > _renkiGaugeRedChangeTiming)
        {
            _applyRedRenkiGauge = true;
        }
        else if(_currentRenkiGauge < _renkiGaugeRedChangeTiming)
        {
            _applyRedRenkiGauge = false;
        }
    }

    /// <summary>
    /// プレイヤーから見てスティックがどこに傾いているかを見るの視野角.
    /// </summary>
    private void viewAngle()
    {
        Vector3 direction = _stickPositionObject.transform.position - _transform.position;
        // ハンターとデバッグ用キューブのベクトルのなす角.
        // デバッグ用キューブの正面.
        float forwardAngle = Vector3.Angle(direction, _transform.forward);
        // オブジェクトの側面.
        float sideAngle = Vector3.Angle(direction, _transform.right);

        RaycastHit hit;
        bool ray = Physics.Raycast(_transform.position, direction.normalized, out hit);

        bool viewFlag = ray && hit.collider.gameObject == _stickPositionObject && GetDistance() > 1;

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
        // スティックを傾けていない場合.
        else if(forwardAngle == 0 && sideAngle == 0)
        {
            FoundFlag((int)viewDirection.NONE);
        }
    }

    /// <summary>
    /// スティックがいる位置をtureで返す.
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
    /// ダッシュするときにスタミナを消費させる.
    /// </summary>
    private void ConsumeStamina()
    {
        _stamina -= _isDashStaminaCost;
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
    private void LimitStop(ref float currentVariable, float maxVariable)
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
    /// 状態遷移を行う.
    /// </summary>
    /// <param name="transitionFlag">状態遷移を行うためのフラグ</param>
    /// <param name="nextState">遷移先の状態</param>
    private void TransitionState(bool transitionFlag, StateBase nextState)
    {
        if (transitionFlag)
        {
            StateTransition(nextState);
        }
    }

    /// <summary>
    /// 走るかダッシュするかを決める.
    /// </summary>
    private void RunOrDash()
    {
        // メニュー開いているときはダッシュしない.
        TransitionState(_input._RBButton && !_openMenu, _dash);
        TransitionState(!_input._RBButton, _running);
    }

    /// <summary>
    /// ダメージを受けた時に体力を減らし状態遷移.
    /// </summary>
    private void OnDamage()
    {
        // 体力が0の時は通さない.
        if (_currentHitPoint <= 0) return;

        _currentHitPoint = _currentHitPoint - _MonsterState.GetMonsterAttack();

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
    /// <param name="flameNum">鳴らすフレーム数</param>
    /// <param name="seName">SEの種類</param>
    private void SEPlay(float flameNum, int seName)
    {
        if ((_stateTime >= flameNum && _stateTime <= flameNum + 0.04f) && !_isPlayOneShot)
        {
            _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, seName);

            _isPlayOneShot = true;
        }
    }

    /// <summary>
    /// SE再生したかどうかのフラグをfalseにリセット.
    /// </summary>
    /// <param name="time">リセットするタイミング</param>
    private void PlayOneShotReset(float time)
    {
        if ((_stateTime >= time && _stateTime <= time + 0.04f) && _isPlayOneShot)
        {
            _isPlayOneShot = false;
        }
    }

    /// <summary>
    /// メニューを開いているかの情報を代入.
    /// </summary>
    private void OpenMenu()
    {
        _openMenu = _huntingSceneManager.GetOpenMenu();
    }

    /// <summary>
    /// 残り体力.
    /// </summary>
    /// <returns></returns>
    public float GetHitPoint() { return _currentHitPoint; }
    /// <summary>
    /// 体力最大値.
    /// </summary>
    /// <returns></returns>
    public float GetMaxHitPoint() { return _maxHitPoint; }
    /// <summary>
    /// 残りスタミナ.
    /// </summary>
    /// <returns></returns>
    public float GetStamina() { return _stamina; }
    /// <summary>
    /// スタミナ最大値.
    /// </summary>
    /// <returns></returns>
    public float GetMaxStamina() { return _maxStamina; }

    /// <summary>
    /// 抜刀、納刀状態取得.
    /// </summary>
    /// <returns></returns>
    public bool GetIsUnsheathedSword() { return _unsheathedSword; }

    /// <summary>
    /// ダメージを与えた時の値.
    /// </summary>
    /// <returns></returns>
    public float GetHunterAttack() { return _attackDamage; }
    /// <summary>
    /// ダメージを与えられるかどうか.
    /// </summary>
    /// <returns></returns>
    public bool GetIsCauseDamage() { return _isCauseDamage; }
    /// <summary>
    /// ダメージを与えられるかの代入.
    /// </summary>
    /// <param name="causeDamage">攻撃を与えられるか</param>
    public void SetIsCauseDamage(bool causeDamage) { _isCauseDamage = causeDamage; }
    /// <summary>
    /// 最大錬気ゲージ.
    /// </summary>
    /// <returns></returns>
    public float GetMaxRenkiGauge() { return _maxRenkiGauge; }
    /// <summary>
    /// 現在の錬気ゲージ.
    /// </summary>
    /// <returns></returns>
    public float GetCurrentRenkiGauge() { return _currentRenkiGauge; }
    /// <summary>
    /// 最大練気ゲージ赤.
    /// </summary>
    /// <returns></returns>
    public float GetMaxRedRenkiGauge() { return _maxRedRenkiGauge; }
    /// <summary>
    /// 現在の練気ゲージ赤.
    /// </summary>
    /// <returns></returns>
    public float GetCurrentRedRenkiGauge() { return _currentRedRenkiGauge; }
    /// <summary>
    /// 練気ゲージ赤が適用されているかどうか取得.
    /// </summary>
    /// <returns></returns>
    public bool GetApplyRedRenkiGauge() { return _applyRedRenkiGauge; }
    /// <summary>
    /// 気刃大回転斬りを行っている途中.
    /// </summary>
    /// <returns></returns>
    public bool GetRoundSlash() { return _drawnSpiritRoundSlash; }
    /// <summary>
    /// 必殺技を成功させたかどうか.
    /// </summary>
    /// <returns></returns>
    public bool GetGreatAttackSuccess() {  return _greatAttackSuccess; }
    /// <summary>
    /// スティックの傾きによって距離を求める. 
    /// </summary>
    /// <returns>スティックの傾きとプレイヤーの距離</returns>
    private float GetDistance()
    {
        _currentDistance = (_stickPositionObject.transform.position - _transform.position).magnitude;
        return _currentDistance;
    }
    /// <summary>
    /// 回復薬の数を取得.
    /// </summary>
    /// <returns></returns>
    public int GetCureMedicineNum() { return _cureMedicineNum; }
}
