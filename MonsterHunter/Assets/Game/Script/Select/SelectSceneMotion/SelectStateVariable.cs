/*�I����ʂ̃v���C���[�ϐ�*/

using UnityEngine;

public partial class SelectPlayerState
{
    private static readonly SelectStateIdle _idle = new();      // �A�C�h��.
    private static readonly SelectStateRunning _running = new();// ����.
    private static readonly SelectStateDash _dash = new();      // �_�b�V��.

    private StateBase _currentState = _idle;// ���݂̏��.

    // ��tUI.
    private ReceptionFlag _receptionFlag;
    // �^�C�g����ʂ֖߂�UI.
    private TitleGuide _titleGuide;
    // �����N�\��UI.
    private RankTable _rankTable;

    // �R���g���[���[�̓��͏��.
    private ControllerManager _input;

    /*�A�j���[�V����*/
    private Animator _animator;

    // Setfloat
    private float _currentRunSpeed = 0;// ���݂̑��鑬�x.

    // Setbool
    private bool _idleMotion = false;// �A�C�h��.
    private bool _runMotion = false;// ����.
    private bool _dashMotion = false;// �_�b�V��.

    private Rigidbody _rigidbody;

    // transform���L���b�V��.
    private Transform _transform;
    // �J����.
    private Camera _camera;
    // �J�����̐���.
    private Vector3 _cameraForward;

    /*�R���g���[���[�ϐ�*/
    // ���X�e�B�b�N�̓��͏��.
    private float _leftStickHorizontal;
    private float _leftStickVertical;

    /*�ړ�*/

    // ���x.
    private Vector3 _moveVelocity = new(0.0f,0.0f,0.0f);
    // �ړ����̉�]���x.
    private float _rotateSpeed = 30.0f;
    // �ړ����x�{��.
    private float _moveVelocityMagnification = 10;
    // ����Ƃ��̈ړ��{��.
    private float _moveVelocityRunMagnification = 10;
    // �_�b�V�����̈ړ��{��.
    private float _moveVelocityDashMagnigication = 15;


    // �N�G�X�g�I����ʂ��J���Ă��邩�ǂ���.
    private bool _openMenu = false;
}
