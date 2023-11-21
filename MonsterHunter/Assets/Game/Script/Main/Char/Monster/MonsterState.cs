/*モンスターステート*/

using UnityEngine;
using UnityEngine.UI;

public partial class MonsterState : MonoBehaviour
{
    public static readonly MonsterStateIdle _idle = new();// アイドル.
    public static readonly MonsterStateRun _run = new();// 移動.
    public static readonly MonsterStateAt _at = new();// 攻撃(デバッグ用).
    public static readonly MonsterStateRotateAttack _rotate = new();// 回転攻撃.
    public static readonly MonsterStateBless _bless = new();// ブレス攻撃.

    // Stateの初期化.
    private StateBase _currentState = _idle;
    // デバッグ用のStateの初期化
    //private StateBase _currentState = _bless;

    private void Start()
    {
        Initialization();
        _currentState.OnEnter(this, null);
    }

    private void Update()
    {
        _currentState.OnUpdate(this);
        _currentState.OnChangeState(this);
        ViewAngle();
    }

    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate(this);

        // 攻撃判定の生成
        _debugAttackCol.SetActive(_indicateAttackCol);

        if(_debagHitPoint <= 0)
        {
            gameObject.SetActive(false);
        }
        _textHp.text = "MonsterHp:" + _debagHitPoint;
        PositionalRelationship();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _collisionTag = collision.transform.tag;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _collisionTag = null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HunterAtCol" && _playerState.GetIsCauseDamage())
        {
            GetOnDamager();
            //other.transform.root.GetComponent<teakedamage>
        }
    }

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

        _line = GetComponent<LineRenderer>();
        _text = GameObject.Find("DebugText").GetComponent<Text>();
        _textHp = GameObject.Find("MonsterHp").GetComponent<Text>();

        _debugAttackCol = GameObject.FindWithTag("MonsterAtCol");
        
        for(int i = 0; i < (int)viewDirection.NONE; i++)
        {
            _viewDirection[i] = false;
        }
    }

    // プレイヤーとモンスター同士の角度、距離によって処理を変更
    private void PositionalRelationship()
    {
        // 正面
        if (_viewDirection[(int)viewDirection.FORWARD])
        {
            if(GetDistance() <= _shortDistance)
            {
                _text.text = "正面近距離";
                _isNearDistance = true;
            }
            else if(GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "正面遠距離";
                _isNearDistance = false;
            }
        }
        // 背後
        else if(_viewDirection[(int)viewDirection.BACKWARD])
        {
            if(GetDistance() <= _shortDistance)
            {
                _text.text = "背後近距離";
                _isNearDistance = true;
            }
            else if(GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "背後遠距離";
                _isNearDistance = false;
            }
        }
        // 右
        else if(_viewDirection[(int)viewDirection.RIGHT])
        {
            if(GetDistance() <= _shortDistance)
            {
                _text.text = "右近距離";
                _isNearDistance = true;
            }
            else if(GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "右遠距離";
                _isNearDistance = false;
            }
        }
        // 左
        else if(_viewDirection[(int)viewDirection.LEFT])
        {
            if(GetDistance() <= _shortDistance)
            {
                _text.text = "左近距離";
                _isNearDistance = true;
            }
            else if(GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "左遠距離";
                _isNearDistance = false;

            }
        }
        else if(_viewDirection[(int)viewDirection.NONE] && GetDistance() >= _longDistance)
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
        if (forwardAngle < 90 * 0.5f )
        {
            FoundFlag((int)viewDirection.FORWARD);
        }
        // 後ろ.
        else if(forwardAngle > 135 && forwardAngle < 180)
        {
            FoundFlag((int)viewDirection.BACKWARD);
        }
        // 右.
        else if(sideAngle < 90 * 0.5f)
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

        //Debug.Log(forwardAngle);
        //Debug.Log(sideAngle);
        

        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, _hunter.transform.position);
    }

    /// <summary>
    /// 見つかっているかの値を返す
    /// </summary>
    /// <param name="foundNum">プレイヤーの位置を示す番号</param>
    private void FoundFlag(int foundNum)
    {
        for(int i = 0; i < (int)viewDirection.NONE; i++)
        {
            if(i == foundNum)
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


    private float GetDistance()
    {
        _currentDistance = (_hunter.transform.position - _trasnform.position).magnitude;

        return _currentDistance;
    }

    public float GetMonsterAttack()
    {
        return _debagAttackPower;
    }

    private float GetOnDamager()
    {
        _debagHitPoint = _debagHitPoint - _playerState.GetHunterAttack();
        return _debagHitPoint;
    }

    public void SetHitPoint(float hitPoint)
    {
        _debagHitPoint = hitPoint;
    }

    public float GetHitPoint()
    {
        return _debagHitPoint;
    }
}
