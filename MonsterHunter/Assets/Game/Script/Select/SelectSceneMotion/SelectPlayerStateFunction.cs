/*�I����ʂ̃v���C���[�̊֐��܂Ƃ�*/

using UnityEngine;

public partial class SelectPlayerState
{
    // ������.
    private void Initialization()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _receptionFlag = GameObject.Find("ReceptioFlag").GetComponent<ReceptionFlag>();
        _titleGuide = GameObject.Find("TitleGuide").GetComponent<TitleGuide>();
        _rankTable = GameObject.Find("RankTableGuide").GetComponent<RankTable>();
        _input = GameObject.FindWithTag("Manager").GetComponent<ControllerManager>();
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _transform = transform;
    }

    /// <summary>
    /// ��ԑJ��
    /// </summary>
    /// <param name="nextState">���̕ύX������</param>
    private void ChangeState(StateBase nextState)
    {
        _currentState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentState);
        _currentState = nextState;
    }

    // �A�j���[�V�����J��.
    private void AnimTransition()
    {
        if(_animator == null) { return; }

        _animator.SetFloat("Speed", _currentRunSpeed);

        _animator.SetBool("Idle", _idleMotion);
        _animator.SetBool("Run", _runMotion);
        _animator.SetBool("Dash", _dashMotion);
    }

    // ��ɏ�����.
    private void SubstituteVariableFixedUpdate()
    {
        // �J�����̐���.
        _cameraForward = Vector3.Scale(_camera.transform.forward, new Vector3(1.0f,0.0f, 1.0f)).normalized;

        /*�J�����̌�������ړ������擾*/
        // ����.
        Vector3 moveForward = _cameraForward * _leftStickVertical;
        // ��.
        Vector3 moveSide = _camera.transform.right * _leftStickHorizontal;
        // ���x�̑��,
        _moveVelocity = moveForward + moveSide;

        // ���X�g���J���Ă��邩�ǂ������.
        _openMenu = _receptionFlag.GetOpenQuestList() || 
            _titleGuide.GetSceneTransitionUIOpen() || 
            _rankTable.GetRankTableUI();
    }

    // ���X�e�B�b�N�̓��͏��擾.
    private void GetStickInput()
    {
        // ���͏����.
        _leftStickHorizontal = _input._LeftStickHorizontal;
        _leftStickVertical = _input._LeftStickVertical;
    }

    // �ړ����̉�]����.
    private void RotateDirection()
    {
        _transform.forward = Vector3.Slerp(_transform.forward, _moveVelocity, Time.deltaTime * _rotateSpeed);
    }
}