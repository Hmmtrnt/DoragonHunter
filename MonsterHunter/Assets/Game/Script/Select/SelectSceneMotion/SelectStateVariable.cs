/*選択画面のプレイヤー変数*/

using UnityEngine;

public partial class SelectPlayerState
{
    private static readonly SelectStateIdle _idle = new();      // アイドル.
    private static readonly SelectStateRunning _running = new();// 走る.
    private static readonly SelectStateDash _dash = new();      // ダッシュ.

    private StateBase _currentState = _idle;// 現在の状態.

    // 受付UI.
    private ReceptionFlag _receptionFlag;
    // タイトル画面へ戻るUI.
    private TitleGuide _titleGuide;
    // ランク表のUI.
    private RankTable _rankTable;

    // コントローラーの入力情報.
    private ControllerManager _input;

    /*アニメーション*/
    private Animator _animator;

    // Setfloat
    private float _currentRunSpeed = 0;// 現在の走る速度.

    // Setbool
    private bool _idleMotion = false;// アイドル.
    private bool _runMotion = false;// 走る.
    private bool _dashMotion = false;// ダッシュ.

    private Rigidbody _rigidbody;

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

    /*移動*/

    // 速度.
    private Vector3 _moveVelocity = new(0.0f,0.0f,0.0f);
    // 移動時の回転速度.
    private float _rotateSpeed = 30.0f;
    // 移動速度倍率.
    private float _moveVelocityMagnification = 10;
    // 走るときの移動倍率.
    private float _moveVelocityRunMagnification = 10;
    // ダッシュ時の移動倍率.
    private float _moveVelocityDashMagnigication = 15;


    // クエスト選択画面を開いているかどうか.
    private bool _openMenu = false;
}
