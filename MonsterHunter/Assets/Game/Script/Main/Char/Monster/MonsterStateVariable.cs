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
    private PlayerState _state;

    // ファイアーボールのプレハブ
    private GameObject _fireBall;
    // ファイアーボールの生成位置
    private GameObject _fireBallPosition;
    private Vector3 _temp;

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

    // 以下デバッグ用

    // デバッグ用ステータス
    // 体力
    private float _debagHitPoint = 300;
    // 攻撃力
    private float _debagAttackPower = 1;

    // デバッグ用攻撃判定
    private GameObject _debugAttackCol;
    // デバッグ用攻撃判定を生成するかどうか
    private bool _indicateAttackCol = false;

    [Header("回転速度")]
    [SerializeField] private float _rotateSpeed = 0;

    private LineRenderer _line;

    // デバッグ用テキスト
    private Text _text;
    private Text _textHp;

}
