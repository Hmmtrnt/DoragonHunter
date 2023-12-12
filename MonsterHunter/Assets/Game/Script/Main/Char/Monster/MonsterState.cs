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
    public static readonly MonsterStateBite _bite = new();// 噛みつき攻撃.
    public static readonly MonsterStateRushForward _rush = new();// 突進.
    public static readonly MonsterStateWingBlow _wingBlow = new();// 翼で攻撃.
    public static readonly MonsterStatePowerFireBall _powerFireBall = new();// 大技火球.


    // Stateの初期化.
    private StateBase _currentState = _idle;
    // デバッグ用のStateの初期化
    //private StateBase _currentState = _bless;

    private void Start()
    {
        Initialization();
        _currentState.OnEnter(this, null);
        _randomNumber = 0;
    }

    private void Update()
    {
        _currentState.OnUpdate(this);
        _currentState.OnChangeState(this);
        ViewAngle();

        
    }

    private void FixedUpdate()
    {
        // 状態のフレームの時間を増やす.
        _stateFlame++;

        _currentState.OnFixedUpdate(this);

        // 乱数を常に与える.
        _randomNumber = Random.Range(1, 101);

        Debug.Log(_randomNumber);

        // 攻撃判定の生成.

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
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
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
