// モンスターのState変数

using UnityEngine;
using UnityEngine.UI;

public partial class MonsterState
{
    enum viewDirection
    {
        FORWARD,
        BACKWARD,
        RIGHT,
        LEFT,
        NONE
    }

    // 目標のプレイヤー
    private GameObject _hunter;
    private Transform _trasnform;
    private Rigidbody _rigidbody;
    // プレイヤーのステート情報
    private PlayerState _playerState;

    // ファイアーボールのプレハブ
    private GameObject _fireBall;
    // ファイアーボールの生成位置
    private GameObject _fireBallPosition;
    private Vector3 _temp;

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
    private bool _idleMotion = false;
    private bool _runMotion = false;
    private bool _deathMotion = false;
    private bool _rotateMotion = false;
    private bool _blessMotion = false;
    private bool _biteMotion = false;
    private bool _rushMotion = false;


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

    // 状態のフレーム数
    private int _stateFlame = 0;

    // 乱数.
    private int _randomNumber = 0;

    // デバッグ用テキスト
    private Text _text;
    private Text _textHp;

}
