// プレイヤーの変数.

using UnityEngine;
using UnityEngine.UI;

public partial class PlayerState
{
    enum viewDirection
    {
        FORWARD,
        BACKWARD,
        RIGHT,
        LEFT,
        NONE
    }

    // デバッグ用のテキスト
    public Text _text;

    // スティックがハンターのどの向きにいるかを取得
    private bool[] _viewDirection = new bool[5];

    // スティックの傾きを表す球とハンターの距離
    private float _currentDistance;

    // コントローラーの入力情報.
    private ControllerManager _input;

    /*アニメーション*/
    private Animator _animator;
    //--納刀状態--//
    // Setfloat
    private float _currentRunSpeed = 0;// 現在の走る速度.

    // Setbool
    private bool _idleMotion  = false;// アイドル.
    private bool _runMotion   = false;// 走る.
    private bool _dashMotion  = false;// ダッシュ.
    private bool _avoidMotion = false;// 回避.
    private bool _healMotion  = false;// 回復.

    //--抜刀状態--//
    // Setbool
    private bool _drawnSwordMotion      = false;// 抜刀.
    private bool _drawnIdleMotion       = false;// アイドル.
    private bool _drawnAvoidMotion      = false;// 回避.
    private bool _drawnRightAvoidMotion = false;// 右回避.
    private bool _drawnLeftAvoidMotion  = false;// 左回避.
    private bool _drawnSteppingSlash    = false;// 踏み込み斬り.
    private bool _drawnThrustSlash      = false;// 突き.
    private bool _drawnSlashUp          = false;// 斬り上げ.
    private bool _drawnSpiritBlade1     = false;// 気刃斬り1.
    private bool _drawnSpiritBlade2     = false;// 気刃斬り2.
    private bool _drawnSpiritBlade3     = false;// 気刃斬り3.
    private bool _drawnSpiritRoundSlash = false;// 気刃大回転斬り.

    // 次のモーションに遷移するフレーム.
    private float _nextMotionFlame = 0;

    //--共通モーション--//
    // Setbool
    private bool _damageMotion = false;// ダメージ.

    // プレイヤーのステータス.
    // 体力.
    private float _hitPoint = 200;
    // 体力最大値.
    private float _maxHitPoint = 200;
    // スタミナ.
    private float _stamina = 200;
    // スタミナ最大値.
    private float _maxStamina = 200;
    // スタミナの自動回復量
    private float _autoRecaveryStamina = 0.5f;

    // 攻撃力.
    private float _AttackPower = 100;
    // 攻撃フレーム数
    private int _attackFrame = 0;

    //private bool _isReceiveDamage = false;

    // モーション値.
    //private float _MotionValue = 0;

    // Rigidbody.
    private Rigidbody _rigidbody;

    // 納刀抜刀を確認するデバッグ用オブジェクト.
    private GameObject _debagObject;

    

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

    // 現在の攻撃のモーション値.
    //private int _currentAttackMotionValue = 0;
    // 最大攻撃のモーション値.
    //private int _maxAttackMotionValue = 0;

    // 攻撃判定.

    // ダメージを与えられるかどうか.
    private bool _isCauseDamage = false;

    /*アイドル状態時の変数*/


    /*移動時の変数*/
    // 移動方向.
    private Vector3 _moveDirection;
    // 走る時の移動倍率.
    private float _moveVelocityRunMagnification = 12;
    // ダッシュ時の移動倍率.
    private float _moveVelocityDashMagnigication = 20;
    // 疲労ダッシュ時の移動倍率.
    private float _moveVelocityFatigueDashMagnigication = 10;

    // 移動速度倍率.
    private float _moveVelocityMagnification = 12;
    // 回復しながらの移動倍率.
    //private float _moveVelocityRecoveryMagnification = 10;

    // 移動速度.
    private Vector3 _moveVelocity = new(0.0f, 0.0f, 0.0f);
    // 移動時の回転速度
    private float _rotateSpeed = 30.0f;

    // ダッシュしているかどうか.
    private bool _isDashing = false;
    // ダッシュしているときのスタミナ消費量
    private float _isDashStaminaCost = 0.7f;

    // 重力.
    private float _gravity = 0.0f;

    /*回避時の変数*/
    // 回避速度倍率.
    private float _avoidVelocityMagnification = 50;
    // 回避速度.
    private Vector3 _avoidVelocity = Vector3.zero;
    // 現在の回避フレーム.
    private int _avoidTime = 0;
    // 最大回避フレーム.
    private int _avoidMaxTime = 45;
    // 回避時のスタミナ消費量.
    private float _avoidStaminaCost = 25;

    // 回避しているかどうか.
    private bool _isAvoiding = false;

    // スティックの傾きとプレイヤー間の方向ベクトル.
    private Vector3 _stickDirection = Vector3.zero;

    /*回復*/
    // 回復しているかどうか.
    private bool _isRecovery = false;
    // 現在の回復時間.
    private int _currentRecoveryTime = 0;
    // 最大回復時間.
    private int _maxRecoveryTime = 200;
    // 回復量.
    private float _recoveryAmount = 0.8f;


    // 以下デバッグ用変数
    // モンスターオブジェクト.
    private GameObject _Monster;
    // モンスターのState.
    private MonsterState _MonsterState;
}