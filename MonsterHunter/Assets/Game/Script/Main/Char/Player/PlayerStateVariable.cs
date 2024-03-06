﻿// プレイヤーの変数.

using UnityEngine;

public partial class PlayerState
{
    //--納刀状態--//
    private static readonly StateIdle _idle = new();                // アイドル.
    private static readonly StateAvoid _avoid = new();              // 回避.
    private static readonly StateRunning _running = new();          // 走る.
    private static readonly StateDash _dash = new();                // ダッシュ.
    private static readonly StateFatigueDash _fatigueDash = new();  // 疲労時のダッシュ.
    private static readonly StateRecovery _recovery = new();        // 回復.

    //--抜刀状態--//
    private static readonly StateDrawnSwordTransition _drawSwordTransition = new(); // 抜刀する.
    private static readonly StateIdleDrawnSword _idleDrawnSword = new();            // アイドル.
    private static readonly StateRunDrawnSword _runDrawnSword = new();              // 走る.
    private static readonly StateAvoidDrawSword _avoidDrawnSword = new();           // 抜刀回避.
    private static readonly StateRightAvoidDrawSword _rightAvoid = new();           // 攻撃後の右回避.
    private static readonly StateLeftAvoidDrawSword _leftAvoid = new();             // 攻撃後の左回避.
    private static readonly StateBackAvoidDrawSword _backAvoid = new();             // 攻撃後の後ろ回避.
    private static readonly StateSheathingSword _sheathingSword = new();            // 納刀する.
    private static readonly StateSteppingSlash _steppingSlash = new();              // 踏み込み斬り.
    private static readonly StatePrick _prick = new();                              // 突き.
    private static readonly StateSlashUp _slashUp = new();                          // 斬り上げ.
    private static readonly StateSpiritBlade1 _spiritBlade1 = new();                // 気刃斬り1.
    private static readonly StateSpiritBlade2 _spiritBlade2 = new();                // 気刃斬り2.
    private static readonly StateSpiritBlade3 _spiritBlade3 = new();                // 気刃斬り3.
    private static readonly StateRoundSlash _roundSlash = new();                    // 気刃大回転斬り.
    private static readonly StateGreatAttackStance _stance = new();                 // 必殺技の構え.
    private static readonly StateGreatAttackSuccess _stanceSuccess = new();         // 必殺技成功.

    //--共通状態--//
    private static readonly StateDamage _damage = new();    // ダメージを受けた.
    private static readonly StateDead _dead = new();        // やられた.

    // 現在のState.
    private StateBase _currentState = _idle;

    enum viewDirection
    {
        FORWARD = 0,
        BACKWARD,
        RIGHT,
        LEFT,
        NONE
    }

    // 次の状態に遷移するときの時間.
    public enum StateTransitionKinds
    {
        /*納刀時*/
        IDLE = 0,
        AVOID,
        RUN,
        DASH,
        FATIGUEDASH,
        RECOVERY,
        /*抜刀時*/
        DRAWSWORDTRANSITION,
        DRAWIDLE,
        DRAWRUN,
        DRAWAVOID,
        RIGHTAVOID,
        LEFTAVOID,
        BACKAVOID,
        SHEATHINGSWORD,
        STEPPINGSLASH,
        PRICK,
        SLASHUP,
        SPIRITBLADE1,
        SPIRITBLADE2,
        SPIRITBLADE3,
        ROUNDSLASH,
        GREATATTACKSTANCE,
        GREATATTACKSUCCESS,
        /*納刀抜刀共通処理*/
        DAMAGE,
        DEAD,

        MAX
    }

    [Header("次の状態に遷移するときの時間")]
    [SerializeField, EnumIndex(typeof(StateTransitionKinds))]
    private float[] _stateTransitionTime;
    private bool[] _stateTransitionFlag = new bool[(int)StateTransitionKinds.MAX];

    // デバッグ用のテキスト
    //public Text _text;

    // スティックがハンターのどの向きにいるかを取得
    private bool[] _viewDirection = new bool[(int)viewDirection.NONE];

    // スティックの傾きを表す球とハンターの距離
    private float _currentDistance;

    // コントローラーの入力情報.
    private ControllerManager _input;

    // SEマネージャー.
    private SEManager _seManager;

    // メニュー画面の選択している情報.
    private MainSceneMenuSelectUi _mainSceneSelectUi;

    // カメラの注視点.
    [SerializeField] private GameObject _cameraFollow;

    // メインシーンの情報.
    private MainSceneManager _mainSceneManager;

    // スティックの傾き具合に合わせる.
    public GameObject _stickPosition;

    // 攻撃判定の情報.
    private AttackCol _attackCol;

    // SEを同時に慣らさないようにする為の変数.
    private bool _isPlayOneShot = false;

    /*アニメーション*/
    private Animator _animator;
    //--納刀状態--//
    // Setfloat
    private float _currentRunSpeed = 0;// 現在の走る速度.

    // Setbool
    private bool _idleMotion    = false;// アイドル.
    private bool _runMotion     = false;// 走る.
    private bool _dashMotion    = false;// ダッシュ.
    private bool _fatigueMotion = false;// 疲労ダッシュ.
    private bool _avoidMotion   = false;// 回避.
    private bool _healMotion    = false;// 回復.

    //--抜刀状態--//
    // Setbool
    private bool _drawnSwordMotion          = false;// 抜刀.
    private bool _drawnIdleMotion           = false;// アイドル.
    private bool _drawnRunMotion            = false;// 走る.
    private bool _drawnAvoidMotion          = false;// 回避.
    private bool _drawnRightAvoidMotion     = false;// 右回避.
    private bool _drawnLeftAvoidMotion      = false;// 左回避.
    private bool _drawnBackAvoidMotion      = false;// 後ろ回避.
    private bool _drawnSheathingSword       = false;// 納刀.
    private bool _drawnSteppingSlash        = false;// 踏み込み斬り.
    private bool _drawnThrustSlash          = false;// 突き.
    private bool _drawnSlashUp              = false;// 斬り上げ.
    private bool _drawnSpiritBlade1         = false;// 気刃斬り1.
    private bool _drawnSpiritBlade2         = false;// 気刃斬り2.
    private bool _drawnSpiritBlade3         = false;// 気刃斬り3.
    private bool _drawnSpiritRoundSlash     = false;// 気刃大回転斬り.
    private bool _greatAttackStanceMotion   = false;// 必殺技の構え.
    private bool _greatAttackSuccess        = false;// 必殺技成功.

    // 次のモーションに遷移するフレーム.
    private float _nextMotionFlame = 0;
    // 次のモーションに遷移する時間.
    private float _nextMotionTime = 0;

    //--共通モーション--//
    // Setbool
    private bool _damageMotion  = false;// ダメージ.
    private bool _downMotion    = false;// ダウン.

    // プレイヤーのステータス.
    // 現在の体力.
    [Header("現在の体力")]
    [SerializeField] private float _currentHitPoint = 200;
    // 体力最大値.
    private float _maxHitPoint = 200;
    // スタミナ.
    private float _stamina = 200;
    // スタミナ最大値.
    private float _maxStamina = 200;
    // スタミナの自動回復量
    private float _autoRecaveryStamina = 0.5f;
    // 自動回復を行わない時のフラグ.
    private bool _autoRecaveryStaminaFlag = false;

    // モーションフレーム.
    private int _motionFrame = 0;
    // 現在の状態のフレーム数.
    public int _stateFlame = 0;
    // 現在の状態の時間.
    public float _stateTime = 0;
    // ヒットストップ中かどうか.
    public bool _currentHitStop = false;
    // ヒットストップ時間.
    public float _hitStopTime = 0;

    // カウンター有効かどうか.
    private bool _counterValid = false;
    // カウンター成功したかどうか.
    private bool _counterSuccess = false;

    // モンスターに与えるダメージ.
    private float _attackDamage = 0.0f;
    // 元の攻撃力.
    private float _attackPower = 0.0f;
    // 攻撃したモンスターの肉質.
    [Header("攻撃したモンスターの肉質")]
    public float _MonsterFleshy = 0;

    // モーション値.
    //private float _MotionValue = 0;

    // Rigidbody.
    private Rigidbody _rigidbody;

    // 納刀抜刀を確認するデバッグ用オブジェクト.
    private GameObject _debagObject;
    // 武器オブジェクトの当たり判定.
    [Header("武器オブジェクトの当たり判定")]
    public GameObject _weaponObject;
    // 武器オブジェクトの表示非表示
    [Header("武器オブジェクトの表示非表示")]
    public bool _weaponActive;

    

    // transformをキャッシュ.
    private Transform _transform;
    // カメラ.
    private Camera _camera;
    // カメラの正面.
    private Vector3 _cameraForward;

    /*コントローラー変数*/
    // 左スティックの入力情報.
    private float _leftStickHorizontal;
    private float _leftStickVertical;

    // 抜刀状態
    // true :抜刀.
    // false:納刀.
    private bool _unsheathedSword = false;

    // 一度通ったら二度は通らない
    private bool _isProcess = false;
    // 攻撃判定を呼び出すとき一度通ったら二度は通らない.
    private bool _isAttackProcess = false;

    // ダメージを与えられるかどうか.
    public bool _isCauseDamage = true;

    /*錬気ゲージ*/
    // 最大練気ゲージ.
    private float _maxRenkiGauge = 100;
    // 現在の練気ゲージ.
    public float _currentRenkiGauge = 0;
    // 練気ゲージの増加量
    public float _increaseAmountRenkiGauge = 0;
    // 練気ゲージを維持する時間.
    public int _maintainTimeRenkiGauge = 0;
    // 練気ゲージを維持する時間を代入する変数.
    public int _maintainTime = 0;
    // 最大練気ゲージ赤.
    private float _maxRedRenkiGauge = 100;
    // 現在の練気ゲージ赤.
    public float _currentRedRenkiGauge = 0;
    // 練気ゲージ赤適用中.
    private bool _applyRedRenkiGauge = false;

    /*移動時の変数*/
    // 走る時の移動倍率.
    [Header("走る時の移動倍率")]
    [SerializeField] private float _moveVelocityRunMagnification = 11;
    // ダッシュ時の移動倍率.
    [Header("ダッシュ時の移動倍率")]
    [SerializeField] private float _moveVelocityDashMagnigication = 20;
    // 疲労ダッシュ時の移動倍率.
    [Header("疲労ダッシュ時の移動倍率")]
    [SerializeField] private float _moveVelocityFatigueDashMagnigication = 5;

    // 移動速度倍率.
    private float _moveVelocityMagnification = 0;
    // 回復しながらの移動倍率.
    //private float _moveVelocityRecoveryMagnification = 10;

    // 移動速度.
    private Vector3 _moveVelocity = new(0.0f, 0.0f, 0.0f);
    // 移動時の回転速度
    private float _rotateSpeed = 20.0f;

    // ダッシュしているかどうか.
    private bool _isDashing = false;
    // ダッシュしているときのスタミナ消費量.
    [Header("ダッシュしているときのスタミナ消費量")]
    [SerializeField] private float _isDashStaminaCost = 0.2f;

    // 重力.
    private float _gravity = 0.0f;

    /*回避時の変数*/
    // 回避速度倍率.
    [Header("回避速度倍率")]
    [SerializeField]private float _avoidVelocityMagnification = 25;
    // 回避速度.
    private Vector3 _avoidVelocity = Vector3.zero;
    // 現在の回避フレーム.
    private int _avoidTime = 0;
    // 最大回避フレーム.
    private int _avoidMaxTime = 80;
    // 回避時のスタミナ消費量.
    [Header("回避時のスタミナ消費量")]
    [SerializeField] private float _avoidStaminaCost = 25;

    // 回避しているかどうか.
    private bool _isAvoiding = false;

    // スティックの傾きとプレイヤー間の方向ベクトル.
    private Vector3 _stickDirection = Vector3.zero;

    /*回復*/
    // 回復薬の数.
    private int _cureMedicineNum = 0;
    // 回復しているかどうか.
    private bool _isRecovery = false;
    // 現在の回復時間.
    private int _currentRecoveryTime = 0;
    // 最大回復時間.
    private int _maxRecoveryTime = 300;
    // 回復量.
    [Header("回復量")]
    [SerializeField] private float _recoveryAmount = 0.0f;

    /*共通*/
    // 減速.
    private float _deceleration = 0;

    // メニューを開いているか取得.
    private bool _openMenu = false;

    // 以下デバッグ用変数
    // モンスターオブジェクト.
    private GameObject _Monster;
    // モンスターのState.
    private MonsterState _MonsterState;
}