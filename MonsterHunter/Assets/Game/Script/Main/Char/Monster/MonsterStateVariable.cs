// モンスターのState変数

using UnityEngine;
using UnityEngine.UI;

public partial class MonsterState
{
    // モンスターから見てハンターの位置はどこか.
    enum viewDirection
    {
        FORWARD,
        BACKWARD,
        RIGHT,
        LEFT,
        NONE
    }

    // 目標のプレイヤー.
    private GameObject _hunter;
    private Transform _trasnform;
    private Rigidbody _rigidbody;
    // プレイヤーのステート情報.
    private PlayerState _playerState;

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


    // 子オブジェクトの当たり判定.
    private MeshCollider[] _colliderChildren;

    // 追従スピード
    private float _followingSpeed = 1;

    // 当たったオブジェクトのタグ取得
    private string _collisionTag = null;
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

    [Header("ダメージUI")]
    [SerializeField] private GameObject _damageUI;

    /*アニメーション*/
    private Animator _animator;
    // bool
    private bool _roarMotion = false;// 咆哮.
    private bool _idleMotion = false;// 待機.
    private bool _runMotion = false;// 走る.
    private bool _deathMotion = false;// 死.
    private bool _rotateMotion = false;// 回転攻撃.
    private bool _blessMotion = false;// ブレス攻撃.
    private bool _biteMotion = false;// 噛みつき攻撃.
    private bool _rushMotion = false;// 突進攻撃.
    private bool _tailMotion = false;// 尻尾攻撃.
    private bool _wingLeftMotion = false;// 左翼攻撃.
    private bool _wingRightMotion = false;// 右翼攻撃.

    // 咆哮するかどうか.
    private bool _isRoar;

    // 以下デバッグ用

    // デバッグ用ステータス
    // 体力
    private float _HitPoint = 3000;
    // 攻撃力
    private float _AttackPower = 5;

    // デバッグ用攻撃判定を生成するかどうか
    private bool _indicateAttackCol = false;

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

    // 乱数.
    private int _randomNumber = 0;

    // デバッグ用テキスト
    private Text _text;
    private Text _textHp;

}
