/*�I����ʂ̃v���C���[�ϐ�*/

using UnityEngine;

public partial class SelectPlayerState
{
    // ��tUI.
    private ReceptionFlag _receptionFlag;
    // �^�C�g����ʂ֖߂�UI.
    private TitleGuide _titleGuide;

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

    // ���̃��[�V�����ɑJ�ڂ���t���[��.
    private float _nextMotionFlame = 0;

    // ���݂̏�Ԃ̃t���[����.
    public int _stateFlame = 0;

    // �����G���W��.
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
    private float _moveVelocityMagnification = 12;
    // ����Ƃ��̈ړ��{��.
    private float _moveVelocityRunMagnification = 12;
    // �_�b�V�����̈ړ��{��.
    private float _moveVelocityDashMagnigication = 20;


    // �N�G�X�g�I����ʂ��J���Ă��邩�ǂ���.
    private bool _openMenu = false;
}
