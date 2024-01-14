/*�v���C���[�X�e�[�g�̊֐��܂Ƃ�*/

using UnityEngine;

public partial class PlayerState
{
    // �v���C���[���̏�����.
    private void Initialization()
    {
        _input = GameObject.FindWithTag("Manager").GetComponent<ControllerManager>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _Monster = GameObject.FindWithTag("Monster");
        _MonsterState = GameObject.FindWithTag("Monster").GetComponent<MonsterState>();
        _weaponObject.SetActive(false);
        _weaponActive = false;
    }

    // ��ԑJ�ڎ��̏�����.
    private void StateTransitionInitialization()
    {
        _stateFlame = 0;
        _maintainTime = 100;
    }

    /// <summary>
    /// �X�e�[�g�ύX.
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
        // �A�j���[�^�[���A�^�b�`����Ă��Ȃ���΃X�L�b�v.
        if (_animator == null) return;

        /*�[��*/
        // float.
        _animator.SetFloat("Speed", _currentRunSpeed);

        // bool.
        _animator.SetBool("Idle", _idleMotion);
        _animator.SetBool("Run", _runMotion);
        _animator.SetBool("Dash", _dashMotion);
        _animator.SetBool("Fatigue", _fatigueMotion);
        _animator.SetBool("Avoid", _avoidMotion);
        _animator.SetBool("Heal", _healMotion);

        /*����*/
        // bool.
        _animator.SetBool("DrawnSword", _drawnSwordMotion);
        _animator.SetBool("DrawnIdle", _drawnIdleMotion);
        _animator.SetBool("DrawnRun", _drawnRunMotion);
        _animator.SetBool("DrawAvoid", _drawnAvoidMotion);
        _animator.SetBool("DrawRAvoid", _drawnRightAvoidMotion);

        _animator.SetBool("DrawLAvoid", _drawnLeftAvoidMotion);
        _animator.SetBool("SheathingSword", _drawnSheathingSword);
        _animator.SetBool("SteppingSlash", _drawnSteppingSlash);
        _animator.SetBool("Thrust", _drawnThrustSlash);
        _animator.SetBool("SlashUp", _drawnSlashUp);

        _animator.SetBool("SpiritBlade1", _drawnSpiritBlade1);
        _animator.SetBool("SpiritBlade2", _drawnSpiritBlade2);
        _animator.SetBool("SpiritBlade3", _drawnSpiritBlade3);
        _animator.SetBool("SpiritRoundSlash", _drawnSpiritRoundSlash);

        /*����*/
        // bool.
        _animator.SetBool("Damage", _damageMotion);
        _animator.SetBool("Down", _downMotion);
    }

    // ���ɏ�����.
    private void SubstituteVariableFixedUpdate()
    {

        // �J�����̐���.
        _cameraForward = Vector3.Scale(_camera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;

        /*�J�����̌�������ړ������擾*/
        // ����.
        Vector3 moveForward = _cameraForward * _leftStickVertical;
        // ��.
        Vector3 moveSide = _camera.transform.right * _leftStickHorizontal;
        // ���x�̑��.
        _moveVelocity = moveForward + moveSide;
        //�X�e�B�b�N���ǂ̕����ɌX���Ă��邩���擾.
        _debugSphere.transform.position = new Vector3(transform.position.x + _moveVelocity.x * 5, transform.position.y, transform.position.z + _moveVelocity.z * 5);
        // ����̓����蔻��̕\����\��.
        _weaponObject.SetActive(_weaponActive);
        // �U���͂̑��.
        _attackDamage = _attackPower * _MonsterFleshy;
        // ���C�Q�[�W�ԓK�p�������ɍU���͂��㏸������.
        if(_applyRedRenkiGauge)
        {
            _attackDamage *= 1.12f;
        }

    }

    // �J�����̒����_�̋���.
    private void CameraFollowUpdate()
    {
        //if(_unsheathedSword)
        //{
        //    if(_cameraFollow.transform.position.z <= _transform.position.z + 0.3f )
        //    {
        //        _cameraFollow.transform.position += new Vector3(0.0f, 0.0f, 0.01f);
        //    }
        //}
        //else
        //{
        //    if (_cameraFollow.transform.position.z >= _transform.position.z)
        //    {
        //        _cameraFollow.transform.position -= new Vector3(0.0f, 0.0f, 0.01f);
        //    }
        //}
    }

    // �X�e�B�b�N���n���^�[���猩�Ăǂ̌����������Ă��邩.
    private void StickDirection()
    {
        // ����.
        if (_viewDirection[(int)viewDirection.FORWARD])
        {
            _text.text = "����";
            
        }
        // �w��.
        else if (_viewDirection[(int)viewDirection.BACKWARD])
        {
            _text.text = "�w��";
        }
        // �E.
        else if (_viewDirection[(int)viewDirection.RIGHT])
        {
            _text.text = "�E";
        }
        // ��.
        else if (_viewDirection[(int)viewDirection.LEFT])
        {
            _text.text = "��";
        }
        else
        {
            _text.text = "NONE";
        }
    }

    // ���C�Q�[�W�ԂɂȂ�ƌ��ʂ�K�p.
    private void ApplyRedRenkiGauge()
    {
        if (_currentRenkiGauge > 75)
        {
            //_currentRedRenkiGauge = 100;
            _applyRedRenkiGauge = true;
        }
        else if(_currentRenkiGauge < 75)
        {
            _applyRedRenkiGauge = false;
        }

        //Debug.Log(_applyRedRenkiGauge);
    }

    // �v���C���[�̎���p.
    private void viewAngle()
    {
        Vector3 direction = _debugSphere.transform.position - _transform.position;
        // �n���^�[�ƃf�o�b�O�p�L���[�u�̃x�N�g���̂Ȃ��p.
        // �f�o�b�O�p�L���[�u�̐���.
        float forwardAngle = Vector3.Angle(direction, _transform.forward);
        // �I�u�W�F�N�g�̑���.
        float sideAngle = Vector3.Angle(direction, _transform.right);

        RaycastHit hit;
        bool ray = Physics.Raycast(_transform.position, direction.normalized, out hit);

        bool viewFlag = ray && hit.collider.gameObject == _debugSphere && GetDistance() > 1;


        //Debug.Log(viewFlag);
        //if (!viewFlag) return;

        // ����.
        if (forwardAngle < 90 * 0.5f)
        {
            FoundFlag((int)viewDirection.FORWARD);
            //_text.text = "����";
        }
        // ���.
        else if (forwardAngle > 135 && forwardAngle < 180)
        {
            FoundFlag((int)viewDirection.BACKWARD);
            //_text.text = "���";
        }
        // �E.
        else if (sideAngle < 90 * 0.5f)
        {
            FoundFlag((int)viewDirection.RIGHT);
            //_text.text = "�E";
        }
        // ��.
        else if (sideAngle > 135 && sideAngle < 180)
        {
            FoundFlag((int)viewDirection.LEFT);
            //_text.text = "��";
        }
        else
        {
            FoundFlag((int)viewDirection.NONE);
            //_text.text = "NONE";
        }
    }

    /// <summary>
    /// �X�e�B�b�N������ʒu��ture�ŕԂ�
    /// </summary>
    /// <param name="foundNum">�X�e�B�b�N�̈ʒu�������ԍ�</param>
    private void FoundFlag(int foundNum)
    {
        for (int i = 0; i < (int)viewDirection.NONE; i++)
        {
            if (i == foundNum)
            {
                _viewDirection[i] = true;
            }
            else
            {
                _viewDirection[i] = false;
            }
        }
    }

    // �X�^�~�i�̎�����.
    private void AutoRecoveryStamina()
    {
        _stamina += _autoRecaveryStamina;
    }

    // �B�C�Q�[�W���R����.
    private void RenkiNaturalConsume()
    {
        // ���C�Q�[�W�������˔j��ێ����ԈȊO�ɏ���.
        if (_currentRenkiGauge <= 0 || _maintainTimeRenkiGauge > 0) return;
        _currentRenkiGauge -= 0.03f;
    }

    // ���C�Q�[�W�ێ����Ԃ̌o��.
    private void MaintainElapsedTimeRenkiGauge()
    {
        if(_maintainTimeRenkiGauge <= 0) return;
        _maintainTimeRenkiGauge--;
    }

    // ���C�Q�[�W�Ԏ��R����.
    private void RedRenkiNaturalConsume()
    {
        // ���C�Q�[�W�Ԃ������˔j�ȊO�ɏ���.
        if(_currentRedRenkiGauge <= 0) return;
        _currentRedRenkiGauge -= 0.05f;
    }

    // �ϐ��̏��������˔j���Ȃ�.
    private void LimitStop(ref float currentVariable, ref float maxVariable)
    {
        // ����˔j�h�~.
        if (currentVariable >= maxVariable)
        {
            currentVariable = maxVariable;
        }
        // �����˔j�h�~.
        if (currentVariable < 0)
        {
            currentVariable = 0;
        }
    }
    

    // �O�i����.
    private void ForwardStep(float speedPower)
    {
        _rigidbody.velocity = _transform.forward * speedPower;
    }

    // �_���[�W���󂯂����ɑJ��.
    private void OnDamage()
    {
        if (_hitPoint <= 0) return;

        _hitPoint = _hitPoint - _MonsterState.GetMonsterAttack();

        ChangeState(_damage);
    }

    // �̗͂�0�ɂȂ������ɌĂяo��.
    private void OnDead()
    {
        ChangeState(_dead);
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
        transform.forward = Vector3.Slerp(transform.forward, _moveVelocity, Time.deltaTime * _rotateSpeed);
    }

    // �n�ʊђʖ���
    private void GroundPenetrationDisable()
    {
        if(_transform.position.y < 0)
        {
            _transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }
    }

    

    // �_�b�V�����Ă��邩�ǂ����̏��擾.
    public bool GetIsDashing() { return _isDashing; }

    // ����t���[���̐����擾.
    public int GetAvoidTime() { return _avoidTime; }

    // ������Ă��邩�ǂ����̏��擾.
    public bool GetIsAvoiding() { return _isAvoiding; }

    // �񕜂��Ă��鎞�Ԏ擾.
    public int GetRecoveryTime() { return _currentRecoveryTime; }

    // �񕜂��Ă��邩�ǂ����̏��擾.
    public bool GetIsRecovery() { return _isRecovery; }

    // �c��̗�.
    public float GetHitPoint() { return _hitPoint; }
    // �̗͍ő�l.
    public float GetMaxHitPoint() { return _maxHitPoint; }
    // �c��X�^�~�i.
    public float GetStamina() { return _stamina; }
    // �X�^�~�i�ő�l.
    public float GetMaxStamina() { return _maxStamina; }

    // �_���[�W��^�������̒l.
    public float GetHunterAttack() { return _attackDamage; }

    // �_���[�W��^�����邩�ǂ���.
    public bool GetIsCauseDamage() { return _isCauseDamage; }
    // �_���[�W��^�����邩�̑��.
    public void SetIsCauseDamage(bool causeDamage) { _isCauseDamage = causeDamage; }

    // �ő�B�C�Q�[�W.
    public float GetMaxRenkiGauge() { return _maxRenkiGauge; }
    // ���݂̘B�C�Q�[�W.
    public float GetCurrentRenkiGauge() { return _currentRenkiGauge; }
    // �ő���C�Q�[�W��.
    public float GetMaxRedRenkiGauge() { return _maxRedRenkiGauge; }
    // ���݂̗��C�Q�[�W��.
    public float GetCurrentRedRenkiGauge() { return _currentRedRenkiGauge; }
    // �C�n���]�a����s���Ă���r��.
    public bool GetRoundSlash() { return _drawnSpiritRoundSlash; }

    // �X�e�B�b�N�̌X���ɂ���ċ��������߂�.
    private float GetDistance()
    {
        _currentDistance = (_debugSphere.transform.position - _transform.position).magnitude;
        return _currentDistance;
    }
}
