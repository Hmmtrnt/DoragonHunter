// モンスターのState変数

using UnityEngine;

public partial class MonsterState
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

    // 次の状態に遷移するときの時間.
    public enum StateTransitionKinds
    {
        ROAR,
        IDLE,
        DOWN,
        FALTER,
        ROTATE,
        BLESS,
        BITE,
        RUSH,
        WINGBLOWRIGHT,
        WINGBLOWLEFT,
        TAIL,

        MAX
    }

    // モンスターから見てハンターの位置はどこか.
    enum viewDirection
    {
        FORWARD,
        BACKWARD,
        RIGHT,
        LEFT,
        NONE
    }

    // 足煙エフェクトが出る位置.
    enum footSmokePosition
    {
        WINGRIGHT,
        WINGLEFT,
        TAIL,
        NONE
    }

    // 足煙エフェクトの違い.
    enum footSmokeEffect
    {
        LEG,
        TAIL,
        WING,
        NONE,
    }

    [Header("次の状態に遷移するときの時間")]
    [SerializeField, EnumIndex(typeof(StateTransitionKinds))]
    private float[] _stateTransitionTime;

    // 狩猟中シーンの情報.
    private HuntingSceneManager _huntingSceneManager;

    // 目標のプレイヤー.
    private GameObject _hunter;
    private Transform _transform;
    // プレイヤーのステート情報.
    private PlayerState _playerState;

    // SEを鳴らす.
    private SEManager _seManager;

    // ファイアーボールのプレハブ.
    private GameObject _fireBall;
    // ファイアーボールの生成位置.
    private GameObject _fireBallPosition;
    // 噛みつき判定.
    [Header("噛みつき判定")]
    public GameObject _biteCollisiton;
    // 突進判定.
    [Header("突進攻撃判定")]
    public GameObject _rushCollisiton;
    // 翼攻撃判定.
    [Header("翼攻撃判定")]
    public GameObject _wingRightCollisiton;
    public GameObject _wingLeftCollisiton;

    // 尾判定.
    [Header("尾攻撃判定")]
    public GameObject[] _tailCollisiton;

    // 回転攻撃判定.
    [Header("回転攻撃判定")]
    public GameObject _rotateCollisiton;

    // 足煙エフェクトの生成箇所.
    [Header("足煙エフェクトの生成箇所")]
    public GameObject[] _footSmokePosition;

    // 足煙エフェクトプレハブ.
    private GameObject[] _footSmokePrehub = new GameObject[3];


    // 子オブジェクトの当たり判定.
    private MeshCollider[] _colliderChildren;

    // ハンターがモンスターのどの向きにいるかを取得
    private bool[] _viewDirection = new bool[5];

    // 現在のプレイヤーとモンスターの距離
    private float _currentDistance = 0;
    // 近距離
    private float _shortDistance = 20;
    // 遠距離
    private float _longDistance = 50;

    // 遠、近距離にいるかどうかの真偽.
    // true  : 近
    // false : 遠
    private bool _isNearDistance = false;

    /*アニメーション*/
    private Animator _animator;
    // bool
    private bool _roarMotion = false;// 咆哮.
    private bool _idleMotion = false;// 待機.
    private bool _weakenMotion = false;// 弱った時の待機.
    private bool _falterMotion = false;// 怯む.
    private bool _deathMotion = false;// 死.
    private bool _rotateMotion = false;// 回転攻撃.
    private bool _blessMotion = false;// ブレス攻撃.
    private bool _biteMotion = false;// 噛みつき攻撃.
    private bool _rushMotion = false;// 突進攻撃.
    private bool _tailMotion = false;// 尻尾攻撃.
    private bool _wingLeftMotion = false;// 左翼攻撃.
    private bool _wingRightMotion = false;// 右翼攻撃.


    // 他のスクリプトに現在の状態情報を渡すための変数.
    public bool _currentRotateAttack = false;
    public bool _currentWingAttackLeft = false;
    public bool _currentWingAttackRight = false;

    // 咆哮するかどうか.
    private bool _isRoar = false;

    // 怯み値.
    private float _falterValue = 0;
    // 怯み始める最大値.
    private float _falterMaxValue = 1000;

    // ステータス.
    // 体力.
    private float _HitPoint = 0;
    // 最大体力数.
    private float _MaxHitPoint = 0;
    // 弱るタイミングの体力の割合.
    private float _weakenTimingHitPoint = 0;
    // モンスターが弱っているかを取得.
    private bool _weakenState = false;

    // 攻撃力
    private float _AttackPower = 10;

    [Header("回転速度")]
    [SerializeField] private float _rotateSpeed = 0;

    // 移動速度.
    private Vector3 _moveVelocity = new(0.0f, 0.0f, 0.0f);

    // 前後の移動速度の調整.
    private float _forwardSpeed = 0;
    // 左右の移動速度の調整.
    private float _sideSpeed = 0;

    // 状態のフレーム数
    private int _stateFlame = 0;
    // 状態の時間
    private float _stateTime = 0;

    // 状態管理のための乱数.
    private int _randomNumber = 0;

    // ダメージを受けるかどうか.
    private bool _takeDamage = false;

    // SEを同時に慣らさないようにする為の変数.
    private bool _isPlayOneShot = false;

    // チュートリアル状態かどうか.
    private bool _tutorialState = false;

    // 行動を起こすかどうか.
    [Header("行動を起こすかどうか")]
    [SerializeField] private bool _isAction = false;
}
