/*モンスターの行動全体の管理*/

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public partial class MonsterState : MonoBehaviour
{
    /*攻撃以外のモーション*/
    public static readonly MonsterStateRoar             _roar = new();          // 咆哮.
    public static readonly MonsterStateIdle             _idle = new();          // 待機.
    public static readonly MonsterStateDown             _down = new();          // やられる.
    public static readonly MonsterStateFalter           _falter = new();        // 怯み.

    /*攻撃モーション*/
    public static readonly MonsterStateRotateAttack     _rotate = new();        // 回転攻撃.
    public static readonly MonsterStateBless            _bless = new();         // ブレス攻撃.
    public static readonly MonsterStateBite             _bite = new();          // 噛みつき攻撃.
    public static readonly MonsterStateRushForward      _rush = new();          // 突進攻撃.
    public static readonly MonsterStateWingBlowRight    _wingBlowRight = new(); // 右翼攻撃.
    public static readonly MonsterStateWingBlowLeft     _wingBlowLeft = new();  // 左翼攻撃.
    public static readonly MonsterStateTailAttack       _tail = new();          // 尻尾攻撃.

    // Stateの初期化.
    public StateBase _currentState = _idle;

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
        WeakenState();
        // 状態の経過時間を増やす.
        StateTime();
    }

    private void FixedUpdate()
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
        if(_isRoar && _stateFlame >= 10)
        {
            RoarTransition();
        }

        //Debug.Log(_HitPoint);

        // 怯み値がたまった時と生きているときに処理.
        if(_falterValue >= _falterMaxValue && _HitPoint > 0)
        {
            Debug.Log("to");
            ChangeFlater();
        }

        //Debug.Log(_currentState);
        //Debug.Log(_falterValue);

        // 体力が0になった時の処理.
        if (_HitPoint <= 0)
        {
            ChangeStateDeath();
        }
        // 体力を0未満にしない.
        HitPointLowerLimit();

        if (_takeDamage)
        {
            GetOnDamager();
            GetOnFalter();
            _takeDamage = false;
        }
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
            _playerState.SetIsCauseDamage(false);
            _takeDamage = true;
        }
    }
}
