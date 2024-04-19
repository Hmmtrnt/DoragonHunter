/*モンスターの全体の関数*/

using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.GridLayoutGroup;

public partial class MonsterState
{
    // 以下の4つの関数の処理順変更不可.
    /// <summary>
    /// 初期化.
    /// </summary>
    private void Init()
    {
        Initialization();
        _currentState.OnEnter(this, null);
    }

    /// <summary>
    /// 更新処理.
    /// </summary>
    private void UpdateProcess()
    {
        _currentState.OnUpdate(this);
        _currentState.OnChangeState(this);
        ViewAngle();
        WeakenState();
        // 状態の経過時間を増やす.
        StateTime();
    }

    /// <summary>
    /// 固定更新処理.
    /// </summary>
    private void FixedUpdateProcess()
    {
        // 計算情報の代入.
        SubstituteVariable();

        // 状態のフレームの時間を増やす.
        _stateFlame++;

        _currentState.OnFixedUpdate(this);

        // 乱数を常に与える.
        _randomNumber = Random.Range(1, 101);

        // プレイヤーとモンスター同士の角度、距離によって処理を変更.
        PositionalRelationship();
        // アニメーション遷移.
        AnimTransition();

        // 最初だけ咆哮するようにする.
        if (_isRoar && _stateFlame >= 10)
        {
            RoarTransition();
        }

        // 怯み値がたまった時と生きているときに処理.
        if (_falterValue >= _falterMaxValue && _HitPoint > 0)
        {
            ChangeFlater();
        }

        // 体力が0になった時の処理.
        if (_HitPoint <= 0)
        {
            if(!_tutorialState)
            {
                ChangeStateDeath();
            }
        }
        // 体力を0未満にしない.
        HitPointLowerLimit();

        // ダメージを受ける.
        if (_takeDamage && !_tutorialState)
        {
            GetOnDamager();
            GetOnFalter();
            _takeDamage = false;
        }

        //Debug.Log(_HitPoint);
    }

    /// <summary>
    /// 貫通した瞬間.
    /// </summary>
    /// <param name="other">対象の当たり判定</param>
    private void TriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HunterAtCol" && _playerState.GetIsCauseDamage())
        {
            _playerState.SetIsCauseDamage(false);
            _takeDamage = true;
        }
    }

    /// <summary>
    /// ステートの変更.
    /// </summary>
    /// <param name="nextState">次の状態</param>
    private void ChangeState(StateBase nextState)
    {
        _currentState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentState);
        _currentState = nextState;
    }

    /// <summary>
    /// 初期化.
    /// </summary>
    private void Initialization()
    {
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
        _hunter = GameObject.Find("Hunter");
        _trasnform = transform;
        _playerState = _hunter.GetComponent<PlayerState>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fireBall = (GameObject)Resources.Load("FireBall2");
        _fireBallPosition = GameObject.Find("BlessPosition");
        _footSmokePrehub[0] = (GameObject)Resources.Load("MonsterLegSmoke");
        _footSmokePrehub[1] = (GameObject)Resources.Load("MonsterTailSmoke");
        _footSmokePrehub[2] = (GameObject)Resources.Load("MonsterWingSmoke");

        _colliderChildren = GetComponentsInChildren<MeshCollider>();

        _animator = GetComponent<Animator>();


        for (int i = 0; i < (int)viewDirection.NONE; i++)
        {
            _viewDirection[i] = false;
        }
        _idleMotion = true;
        // 初手吠える.
        _isRoar = true;

        // 攻撃当たり判定を無効.
        _biteCollisiton.SetActive(false);
        _rushCollisiton.SetActive(false);
        _wingLeftCollisiton.SetActive(false);
        _wingRightCollisiton.SetActive(false);
        for(int tailColNum = 0; tailColNum < _tailCollisiton.Length; tailColNum++) 
        {
            _tailCollisiton[tailColNum].SetActive(false);
        }
        _rotateCollisiton.SetActive(false);

#if false
        // 体力の決定.
        //if(_mainSceneManager._hitPointMany)
        //{
        //    _MaxHitPoint = 10000;
        //}
        //else
        //{
        //    _MaxHitPoint = 5000;
        //}
#else
        _MaxHitPoint = 10;
#endif


        _weakenTimingHitPoint = _MaxHitPoint / 4;
        _HitPoint = _MaxHitPoint;

        _randomNumber = 0;

        // チュートリアル状態かを見る
        _tutorialState = SceneManager.GetActiveScene().name == "TutorialScene";
    }

    /// <summary>
    /// モンスターの状態を弱らせる.
    /// </summary>
    private void WeakenState()
    {
        if(_HitPoint <= _weakenTimingHitPoint)
        {
            _weakenState = true;
        }
    }

    /// <summary>
    /// 計算した情報の代入.
    /// </summary>
    private void SubstituteVariable()
    {
        // モンスターの正面
        Vector3 monsterForward = Vector3.Scale(transform.forward, new Vector3(1.0f,0.0f,1.0f)).normalized;

        // 前後.
        Vector3 moveForward = monsterForward * _forwardSpeed;
        // 左右.
        Vector3 moveSide = transform.right * _sideSpeed;
        // 速度の代入
        _moveVelocity = moveForward + moveSide;
    }

    /// <summary>
    /// 状態遷移時の初期化.
    /// </summary>
    private void StateTransitionInitialization()
    {
        _stateFlame = 0;
        _stateTime = 0;
        _isPlayOneShot = false;
    }

    /// <summary>
    /// アニメーション遷移.
    /// </summary>
    private void AnimTransition()
    {
        // アニメーターがアタッチされていなければ引っかかる
        Debug.Assert(_currentState != null);

        // bool
        _animator.SetBool("Roar", _roarMotion);
        _animator.SetBool("Idle", _idleMotion);
        _animator.SetBool("WeakenIdle", _weakenMotion);
        _animator.SetBool("Falter", _falterMotion);
        _animator.SetBool("Death", _deathMotion);
        _animator.SetBool("Bless", _blessMotion);
        _animator.SetBool("Bite", _biteMotion);
        _animator.SetBool("Tail", _tailMotion);
        _animator.SetBool("WingLeft", _wingLeftMotion);
        _animator.SetBool("WingRight", _wingRightMotion);
        _animator.SetBool("ForwardRush", _rushMotion);
        _animator.SetBool("Rota", _rotateMotion);
    }

    /// <summary>
    /// 状態の経過時間取得.
    /// </summary>
    private void StateTime()
    {
        _stateTime += Time.deltaTime;
    }

    /// <summary>
    /// 咆哮モーションに遷移.
    /// </summary>
    private void RoarTransition()
    {
        ChangeState(_roar);
    }

    /// <summary>
    /// プレイヤーとモンスター同士の角度、距離によって処理を変更.
    /// </summary>
    private void PositionalRelationship()
    {
        // 正面
        if (_viewDirection[(int)viewDirection.FORWARD])
        {
            if (GetDistance() <= _shortDistance)
            {
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _isNearDistance = false;
            }
        }
        // 背後
        else if (_viewDirection[(int)viewDirection.BACKWARD])
        {
            if (GetDistance() <= _shortDistance)
            {
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _isNearDistance = false;
            }
        }
        // 右
        else if (_viewDirection[(int)viewDirection.RIGHT])
        {
            if (GetDistance() <= _shortDistance)
            {
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _isNearDistance = false;
            }
        }
        // 左
        else if (_viewDirection[(int)viewDirection.LEFT])
        {
            if (GetDistance() <= _shortDistance)
            {
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _isNearDistance = false;

            }
        }
    }

    /// <summary>
    /// プレイヤーが今モンスターから見てどこにいるのかを取得する.
    /// </summary>
    private void ViewAngle()
    {
        Vector3 direction = _hunter.transform.position - _trasnform.position;
        // オブジェクトとプレイヤーのベクトルのなす角
        // オブジェクトの正面.
        float forwardAngle = Vector3.Angle(direction, _trasnform.forward);
        // オブジェクトの側面.
        float sideAngle = Vector3.Angle(direction, _trasnform.right);

        RaycastHit hit;
        bool ray = Physics.Raycast(_trasnform.position, direction.normalized, out hit);

        bool viewFlag = ray && hit.collider.gameObject == _hunter;

        if (!viewFlag) return;

        // 正面.
        if (forwardAngle < 90 * 0.5f)
        {
            FoundFlag((int)viewDirection.FORWARD);
        }
        // 後ろ.
        else if (forwardAngle > 135 && forwardAngle < 180)
        {
            FoundFlag((int)viewDirection.BACKWARD);
        }
        // 右.
        else if (sideAngle < 90 * 0.5f)
        {
            FoundFlag((int)viewDirection.RIGHT);
        }
        // 左.
        else if (sideAngle > 135 && sideAngle < 180)
        {
            FoundFlag((int)viewDirection.LEFT);
        }
        else
        {
            FoundFlag((int)viewDirection.NONE);
        }
    }

    /// <summary>
    /// 見つかっているかの値を返す.
    /// </summary>
    /// <param name="foundNum">プレイヤーの位置を示す番号</param>
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
    /// 怯み値がたまった時の処理.
    /// </summary>
    private void ChangeFlater()
    {
        // 怯み値をリセット.
        _falterValue = 0;
        //_falterMaxValue = 0;
        ChangeState(_falter);
    }

    /// <summary>
    /// 体力が0になると強制的にダウンする.
    /// </summary>
    private void ChangeStateDeath()
    {
        _currentState = _down;
        ChangeState(_down);
    }

    /// <summary>
    /// 体力を0未満にしない.
    /// </summary>
    private void HitPointLowerLimit()
    {
        if(_HitPoint <= 0)
        {
            _HitPoint = 0;
        }
    }

    /// <summary>
    /// プレイヤーの方を向く.
    /// </summary>
    /// <param name="turnFlame">向くスピード</param>
    private void TurnTowards(int turnFlame)
    {
        // ターゲットの方向ベクトル.
        Vector3 _direction = new Vector3(_hunter.transform.position.x - transform.position.x,
            0.0f, _hunter.transform.position.z - transform.position.z);
        // 方向ベクトルからクォータニオン取得
        Quaternion _rotation = Quaternion.LookRotation(_direction, Vector3.up);

        // プレイヤーのほうを向いて回転
        if (_stateFlame <= turnFlame)
        {
            _trasnform.rotation = Quaternion.Slerp(_trasnform.rotation, _rotation, Time.deltaTime * _rotateSpeed);
        }
    }

    /// <summary>
    /// 足煙エフェクトの生成.
    /// </summary>
    /// <param name="footSmokeKinds">煙の種類</param>
    /// <param name="footSmokePosition">発生タイミング</param>
    private void FootSmokeSpawn(int footSmokeKinds, int footSmokePosition)
    {
        Instantiate(_footSmokePrehub[footSmokeKinds],
                _footSmokePosition[footSmokePosition].transform.position,
                Quaternion.identity);
    }

    /// <summary>
    /// SEを鳴らす処理.
    /// </summary>
    /// <param name="flameNum1">鳴らすタイミングの秒数</param>
    /// <param name="seName">鳴らす音楽の種類</param>
    private void SEPlay(float flameNum1, int seName)
    {
        if ((_stateTime >= flameNum1 && _stateTime <= flameNum1 + 0.04f) && !_isPlayOneShot)
        {
            _seManager.MonsterPlaySE((int)SEManager.AudioNumber.AUDIO3D, seName);

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
    /// 攻撃パターンAIの処理.
    /// </summary>
    private void AttackStateAi()
    {
        // 近距離.
        if (_isNearDistance)
        {
            // 正面(主にかみつき).
            if (_viewDirection[(int)viewDirection.FORWARD])
            {
                if (_randomNumber <= 30)
                {
                    ChangeState(_bite);
                }
                else if (_randomNumber <= 70)
                {
                    ChangeState(_rush);
                }
                else
                {
                    ChangeState(_rotate);
                }
            }
            // 後ろ.
            else if (_viewDirection[(int)viewDirection.BACKWARD])
            {
                if (_randomNumber <= 30)
                {
                    ChangeState(_rotate);
                }
                else if (_randomNumber <= 60)
                {
                    ChangeState(_tail);
                }
                else
                {
                    ChangeState(_bite);
                }
            }
            // 左.
            else if (   _viewDirection[(int)viewDirection.LEFT])
            {
                if (_randomNumber <= 20)
                {
                    ChangeState(_rotate);
                }
                else if (_randomNumber <= 60)
                {
                    ChangeState(_wingBlowLeft);
                }
                else
                {
                    ChangeState(_bite);
                }
            }
            // 右.
            else if (_viewDirection[(int)viewDirection.RIGHT])
            {
                if (_randomNumber <= 20)
                {
                    ChangeState(_rotate);
                }
                else if (_randomNumber <= 60)
                {
                    ChangeState(_wingBlowRight);
                }
                else
                {
                    ChangeState(_bite);
                }
            }
        }
        // 遠距離.
        else
        {
            // 正面.
            if (_viewDirection[(int)viewDirection.FORWARD])
            {
                if (_randomNumber <= 60)
                {
                    ChangeState(_rush);
                }
                else
                {
                    ChangeState(_bless);
                }
            }
            // 背後.
            else if (_viewDirection[(int)viewDirection.BACKWARD])
            {
                if (_randomNumber <= 40)
                {
                    ChangeState(_rush);
                }
                else
                {
                    ChangeState(_bless);
                }
            }
            // 左.
            else if (_viewDirection[(int)viewDirection.LEFT])
            {
                ChangeState(_bless);
            }
            // 右,
            else if (_viewDirection[(int)viewDirection.RIGHT])
            {
                ChangeState(_bless);
            }
        }
    }

    /// <summary>
    /// チュートリアルシーンのAI.
    /// </summary>
    private void TutorialAI()
    {
        // 攻撃可能にチェックを入れたら攻撃を行う.
        //if ()
        //{

        //}
    }


    /// <summary>
    /// プレイヤーとモンスター同士の距離取得.
    /// </summary>
    /// <returns></returns>
    private float GetDistance()
    {
        _currentDistance = (_hunter.transform.position - _trasnform.position).magnitude;

        return _currentDistance;
    }

    /// <summary>
    /// モンスターの攻撃力.
    /// </summary>
    /// <returns></returns>
    public float GetMonsterAttack()
    {
        return _AttackPower;
    }

    /// <summary>
    /// ダメージを受けた後の体力.
    /// </summary>
    /// <returns></returns>
    private float GetOnDamager()
    {
        _HitPoint = _HitPoint - _playerState.GetHunterAttack();
        return _HitPoint;
    }

    /// <summary>
    /// 怯み値を蓄積.
    /// </summary>
    /// <returns></returns>
    private float GetOnFalter()
    {
        _falterValue = _falterValue + _playerState.GetHunterAttack();
        return _falterValue;
    }
    /// <summary>
    /// 体力を取得
    /// </summary>
    /// <returns></returns>
    public float GetHitPoint()
    {
        return _HitPoint;
    }
}
