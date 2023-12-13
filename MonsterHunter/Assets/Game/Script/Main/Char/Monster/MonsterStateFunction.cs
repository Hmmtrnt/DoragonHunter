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
        _temp = _fireBallPosition.transform.position;

        _animator = GetComponent<Animator>();

        _text = GameObject.Find("DebugText").GetComponent<Text>();
        _textHp = GameObject.Find("MonsterHp").GetComponent<Text>();

        for (int i = 0; i < (int)viewDirection.NONE; i++)
        {
            _viewDirection[i] = false;
        }
        _idleMotion = true;
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
        _animator.SetBool("Bless", _blessMotion);
    }

    // プレイヤーとモンスター同士の角度、距離によって処理を変更
    private void PositionalRelationship()
    {
        // 正面
        if (_viewDirection[(int)viewDirection.FORWARD])
        {
            if (GetDistance() <= _shortDistance)
            {
                _text.text = "正面近距離";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "正面遠距離";
                _isNearDistance = false;
            }
        }
        // 背後
        else if (_viewDirection[(int)viewDirection.BACKWARD])
        {
            if (GetDistance() <= _shortDistance)
            {
                _text.text = "背後近距離";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "背後遠距離";
                _isNearDistance = false;
            }
        }
        // 右
        else if (_viewDirection[(int)viewDirection.RIGHT])
        {
            if (GetDistance() <= _shortDistance)
            {
                _text.text = "右近距離";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "右遠距離";
                _isNearDistance = false;
            }
        }
        // 左
        else if (_viewDirection[(int)viewDirection.LEFT])
        {
            if (GetDistance() <= _shortDistance)
            {
                _text.text = "左近距離";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "左遠距離";
                _isNearDistance = false;

            }
        }
        if (GetDistance() >= _longDistance)
        {
            _text.text = "NONE";
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
    /// 見つかっているかの値を返す
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

    public void DamageUI(Collider col)
    {
        var obj = Instantiate(_damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
    }
}
