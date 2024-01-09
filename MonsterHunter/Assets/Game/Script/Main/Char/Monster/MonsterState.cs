/*モンスターステート*/

using UnityEngine;
using UnityEngine.UI;

public partial class MonsterState : MonoBehaviour
{
    public static readonly MonsterStateRoar _roar = new();// 咆哮.
    public static readonly MonsterStateIdle _idle = new();// アイドル.
    public static readonly MonsterStateRun _run = new();// 移動.
    public static readonly MonsterStateDown _down = new();// 死ぬ.

    public static readonly MonsterStateAt _at = new();// 攻撃(デバッグ用).
    public static readonly MonsterStateRotateAttack _rotate = new();// 回転攻撃.
    public static readonly MonsterStateBless _bless = new();// ブレス攻撃.
    public static readonly MonsterStateBite _bite = new();// 噛みつき攻撃.
    public static readonly MonsterStateRushForward _rush = new();// 突進攻撃.
    public static readonly MonsterStateWingBlowRight _wingBlowRight = new();// 右翼攻撃.
    public static readonly MonsterStateWingBlowLeft _wingBlowLeft = new();// 左翼攻撃.
    public static readonly MonsterStateTailAttack _tail = new();// 尻尾攻撃.
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
        // 計算情報の代入.
        SubstituteVariable();

        // 状態のフレームの時間を増やす.
        _stateFlame++;

        _currentState.OnFixedUpdate(this);

        // 乱数を常に与える.
        _randomNumber = Random.Range(1, 101);

        //Debug.Log(_currentState);

        
        _textHp.text = "MonsterHp:" + _HitPoint;

        // プレイヤーとモンスター同士の角度、距離によって処理を変更.
        PositionalRelationship();
        // アニメーション遷移.
        AnimTransition();

        // 最初だけ咆哮するようにする.
        if(_isRoar && _stateFlame >= 10)
        {
            RoarTransition();
            
        }

        //Debug.Log(_currentState);
        // 体力が0になった時の処理.
        if(_HitPoint <= 0)
        {
            ChangeStateDeath();
        }
        // 体力を0未満にしない.
        HitPointLowerLimit();

        if (_takeDamage)
        {
            GetOnDamager();
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
            //GetOnDamager();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    



}
