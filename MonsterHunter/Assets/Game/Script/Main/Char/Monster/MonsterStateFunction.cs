/*モンスターの全体の関数*/

using UnityEngine;
using UnityEngine.UI;

public partial class MonsterState
{
    // ステートの変更.
    private void ChangeState(StateBase nextState)
    {
        _currentState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentState);
        _currentState = nextState;
    }

    // 初期化.
    private void Initialization()
    {
        _hunter = GameObject.Find("Hunter");
        _trasnform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _playerState = _hunter.GetComponent<PlayerState>();

        _fireBall = (GameObject)Resources.Load("FireBall");
        _fireBallPosition = GameObject.Find("BlessPosition");
        //_footSmokePrehub = (GameObject)Resources.Load("MonsterLegSmoke");
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
        //_isRoar = false;


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

        // 体力の決定.
        if(_HitPointMany)
        {
            _HitPoint = 10000;
        }
        else
        {
            _HitPoint = 3000;
        }
    }

    // 計算した情報の代入.
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

        // デバッグ用
        //Debug.Log(_moveVelocity);

    }

    // 状態遷移時の初期化.
    private void StateTransitionInitialization()
    {
        _stateFlame = 0;
    }

    // アニメーション遷移.
    private void AnimTransition()
    {
        // アニメーターがアタッチされていなければ引っかかる
        Debug.Assert(_currentState != null);

        // bool
        _animator.SetBool("Roar", _roarMotion);
        _animator.SetBool("Idle", _idleMotion);
        _animator.SetBool("Death", _deathMotion);
        _animator.SetBool("Bless", _blessMotion);
        _animator.SetBool("Bite", _biteMotion);
        _animator.SetBool("Tail", _tailMotion);
        _animator.SetBool("WingLeft", _wingLeftMotion);
        _animator.SetBool("WingRight", _wingRightMotion);
        _animator.SetBool("ForwardRush", _rushMotion);
        _animator.SetBool("Rota", _rotateMotion);
    }

    // 咆哮モーションに遷移.
    private void RoarTransition()
    {
        ChangeState(_roar);
    }

    // プレイヤーとモンスター同士の角度、距離によって処理を変更.
    private void PositionalRelationship()
    {
        // 正面
        if (_viewDirection[(int)viewDirection.FORWARD])
        {
            if (GetDistance() <= _shortDistance)
            {
                //_text.text = "正面近距離";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                //_text.text = "正面遠距離";
                _isNearDistance = false;
            }
        }
        // 背後
        else if (_viewDirection[(int)viewDirection.BACKWARD])
        {
            if (GetDistance() <= _shortDistance)
            {
                //_text.text = "背後近距離";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                //_text.text = "背後遠距離";
                _isNearDistance = false;
            }
        }
        // 右
        else if (_viewDirection[(int)viewDirection.RIGHT])
        {
            if (GetDistance() <= _shortDistance)
            {
                //_text.text = "右近距離";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                //_text.text = "右遠距離";
                _isNearDistance = false;
            }
        }
        // 左
        else if (_viewDirection[(int)viewDirection.LEFT])
        {
            if (GetDistance() <= _shortDistance)
            {
                //_text.text = "左近距離";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                //_text.text = "左遠距離";
                _isNearDistance = false;

            }
        }
        if (GetDistance() >= _longDistance)
        {
            //_text.text = "NONE";
        }


    }

    // プレイヤーが今モンスターから見てどこにいるのかを取得する
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

    // 体力が0になると強制的にダウンする.
    private void ChangeStateDeath()
    {
        _currentState = _down;
        ChangeState(_down);
    }

    // 体力を0未満にしない.
    private void HitPointLowerLimit()
    {
        if(_HitPoint <= 0)
        {
            _HitPoint = 0;
        }
    }

    // プレイヤーの方を向く.
    private void TurnTowards(int turnFlame)
    {
        // ターゲットの方向ベクトル.
        Vector3 _direction = new Vector3(_hunter.transform.position.x - transform.position.x,
            0.0f, _hunter.transform.position.z - transform.position.z);
        // 方向ベクトルからクォータニオン取得
        Quaternion _rotation = Quaternion.LookRotation(_direction, Vector3.up);

        // デバッグ用ブレス
        // プレイヤーのほうを向いて回転
        if (_stateFlame <= turnFlame)
        {
            _trasnform.rotation = Quaternion.Slerp(_trasnform.rotation, _rotation, Time.deltaTime * _rotateSpeed);
        }
    }

    // 足煙エフェクトの生成.
    private void FootSmokeSpawn(int footSmokeKinds, int footSmokePosition)
    {
        Instantiate(_footSmokePrehub[footSmokeKinds],
                _footSmokePosition[footSmokePosition].transform.position,
                Quaternion.identity);
    }

    private float GetDistance()
    {
        _currentDistance = (_hunter.transform.position - _trasnform.position).magnitude;

        return _currentDistance;
    }

    public float GetMonsterAttack()
    {
        return _AttackPower;
    }

    // ダメージをくらう.
    private float GetOnDamager()
    {
        _HitPoint = _HitPoint - _playerState.GetHunterAttack();
        return _HitPoint;
    }

    public void SetHitPoint(float hitPoint)
    {
        _HitPoint = hitPoint;
    }

    public float GetHitPoint()
    {
        return _HitPoint;
    }
}
