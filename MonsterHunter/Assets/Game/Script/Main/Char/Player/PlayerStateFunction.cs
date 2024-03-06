/*�v���C���[�X�e�[�g�̊֐��܂Ƃ�*/

using System.Buffers;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;


public partial class PlayerState
{
    /// <summary>
    /// �v���C���[���̏�����.
    /// </summary>
    private void VariableInitialization()
    {
        _input = GameObject.FindWithTag("Manager").GetComponent<ControllerManager>();
        _mainSceneSelectUi = GameObject.Find("SelectItem").GetComponent<MainSceneMenuSelectUi>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _Monster = GameObject.FindWithTag("Monster");
        _MonsterState = GameObject.FindWithTag("Monster").GetComponent<MonsterState>();
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
        _attackCol = GameObject.Find("AttackCollider").GetComponent<AttackCol>();
        _weaponObject.SetActive(false);
        _weaponActive = false;
        _cureMedicineNum = 10;
    }

    /// <summary>
    /// ��ԑJ�ڎ��̏�����.
    /// </summary>
    private void StateTransitionInitialization()
    {
        _stateFlame = 0;
        _maintainTime = 100;
        _isPlayOneShot = false;
    }

    /// <summary>
    /// ��ԑJ�ڂ̃t���O�̑��.
    /// </summary>
    private void StateTransitionFlag()
    {
        /*�ȉ��A�[�����̏��*/
        // �ҋ@���.
        _stateTransitionFlag[(int)StateTransitionKinds.IDLE] = _leftStickHorizontal == 0 && _leftStickVertical == 0;
        // ������.
        _stateTransitionFlag[(int)StateTransitionKinds.AVOID] = (_leftStickHorizontal != 0 || _leftStickVertical != 0) &&
            _stamina >= _avoidStaminaCost && _input._AButtonDown;
        // ������.
        _stateTransitionFlag[(int)StateTransitionKinds.RUN] = (_leftStickHorizontal != 0 || _leftStickVertical != 0) && 
            !_input._RBButton;
        // �_�b�V�����.
        _stateTransitionFlag[(int)StateTransitionKinds.DASH] = (_leftStickHorizontal != 0 || _leftStickVertical != 0) &&
            _input._RBButton;
        // ��J�_�b�V�����.
        _stateTransitionFlag[(int)StateTransitionKinds.FATIGUEDASH] = _stamina <= _maxStamina / 5;
        // �񕜏��.
        _stateTransitionFlag[(int)StateTransitionKinds.RECOVERY] = _input._XButtonDown && !_unsheathedSword &&
            _currentHitPoint != _maxHitPoint && _cureMedicineNum > 0;

        /*�ȉ��A�������̏��*/
        // ����������.
        _stateTransitionFlag[(int)StateTransitionKinds.DRAWSWORDTRANSITION] = _input._YButtonDown;
        // �ҋ@���.
        _stateTransitionFlag[(int)StateTransitionKinds.DRAWIDLE] = _leftStickHorizontal == 0 && _leftStickVertical == 0;
        // ������.
        _stateTransitionFlag[(int)StateTransitionKinds.DRAWRUN] = _leftStickHorizontal != 0 || _leftStickVertical != 0;
        // �O������.
        _stateTransitionFlag[(int)StateTransitionKinds.DRAWAVOID] = (_leftStickHorizontal != 0 || _leftStickVertical != 0) &&
            _input._AButtonDown;
        // �E������.
        _stateTransitionFlag[(int)StateTransitionKinds.RIGHTAVOID] = _viewDirection[(int)viewDirection.RIGHT] &&
                GetDistance() > 0.5f &&
                _input._AButtonDown;
        // ��������.
        _stateTransitionFlag[(int)StateTransitionKinds.LEFTAVOID] = _viewDirection[(int)viewDirection.LEFT] &&
            GetDistance() > 0.5f &&
            _input._AButtonDown;
        // ��������.
        _stateTransitionFlag[(int)StateTransitionKinds.BACKAVOID] = _viewDirection[(int)viewDirection.BACKWARD] &&
            GetDistance() > 0.5f &&
            _input._AButtonDown;
        // �[��������.
        _stateTransitionFlag[(int)StateTransitionKinds.SHEATHINGSWORD] = _input._XButtonDown || _input._RBButtonDown;
        // ���ݍ��ݎa����.
        _stateTransitionFlag[(int)StateTransitionKinds.STEPPINGSLASH] = _input._YButtonDown;
        // �˂����.
        _stateTransitionFlag[(int)StateTransitionKinds.PIERCING] = (_input._BButtonDown || _input._YButtonDown) && 
            !_input._LBButton;
        // �؂�グ���.
        _stateTransitionFlag[(int)StateTransitionKinds.SLASHUP] = _input._BButtonDown || _input._YButtonDown;
        // �C�n�a��1���.
        _stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE1] = _input._RightTrigger >= 0.5;
        // �C�n�a��2���.
        _stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE2] = _input._RightTrigger >= 0.5;
        // �C�n�a��3���.
        _stateTransitionFlag[(int)StateTransitionKinds.SPIRITBLADE3] = _input._RightTrigger >= 0.5;
        // �C�n���]�a����.
        _stateTransitionFlag[(int)StateTransitionKinds.ROUNDSLASH] = _input._RightTrigger >= 0.5;
        // ��Z�̍\�����.
        _stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSTANCE] = _input._LBButton && _input._BButtonDown &&
            _applyRedRenkiGauge;
        // ��Z�̐������.
        _stateTransitionFlag[(int)StateTransitionKinds.GREATATTACKSUCCESS] = _counterSuccess;

        /*�[���A������ԗ����̋��ʂ̏��*/
        // �̗͂�0�����ɂȂ������.
        _stateTransitionFlag[(int)StateTransitionKinds.DEAD] = _currentHitPoint <= 0;
    }

    /// <summary>
    /// �ЂƂO�̏�Ԃ��擾���A���̏�ԂɑJ�ڂ��鎞�Ԃ�ݒ�.
    /// </summary>
    /// <param name="PrevState">�ЂƂO�̏��</param>
    /// <param name="NextStateTransitionTime">���̏�ԂɑJ�ڏo����悤�ɂȂ鎞��</param>
    private void SetNextStateTransitionTime(StateBase PrevState, int NextStateTransitionTime)
    {
        if(_currentState == PrevState)
        {
            _nextMotionFlame = NextStateTransitionTime;
        }
    }

    /// <summary>
    /// ���̏�ԂɑJ�ڂ��鎞�Ԃ����Z�b�g.
    /// </summary>
    private void ResetNextStateTransitionTime()
    {
        _nextMotionFlame = 0;
    }

    // �܂��g���\��Ȃ�.
    /// <summary>
    /// ��ԏI�����̏�����.
    /// </summary>
    private void StateTransitionEnd()
    {

    }

    /// <summary>
    /// ��ԑJ��.
    /// </summary>
    /// <param name="nextState">���ɑJ�ڂ�����</param>
    private void StateTransition(StateBase nextState)
    {
        // ��ԏI����.
        _currentState.OnExit(this, nextState);
        // �e��Ԃ̎��Ԃ����Z�b�g.
        ResetTime();
        // ���̏�Ԃ̌Ăяo��.
        nextState.OnEnter(this, _currentState);
        // ���ɑJ�ڂ����Ԃ̑��.
        _currentState = nextState;
    }

    /// <summary>
    /// �A�j���[�V�����J��.
    /// </summary>
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
        _animator.SetBool("DrawBAvoid", _drawnBackAvoidMotion);
        _animator.SetBool("SheathingSword", _drawnSheathingSword);
        _animator.SetBool("SteppingSlash", _drawnSteppingSlash);
        _animator.SetBool("Thrust", _drawnThrustSlash);

        _animator.SetBool("SlashUp", _drawnSlashUp);
        _animator.SetBool("SpiritBlade1", _drawnSpiritBlade1);
        _animator.SetBool("SpiritBlade2", _drawnSpiritBlade2);
        _animator.SetBool("SpiritBlade3", _drawnSpiritBlade3);
        _animator.SetBool("SpiritRoundSlash", _drawnSpiritRoundSlash);
        _animator.SetBool("GreatAttackStance", _greatAttackStanceMotion);
        _animator.SetBool("GreatAttackSuccess", _greatAttackSuccess);

        /*����*/
        // bool.
        _animator.SetBool("Damage", _damageMotion);
        _animator.SetBool("Down", _downMotion);
    }

    /// <summary>
    /// ���݂̏�Ԃ̃t���[�����Ǘ�.
    /// </summary>
    private void StateFlameManager()
    {
        if(!_currentHitStop)
        {
            _stateFlame++;
        }
    }

    /// <summary>
    /// �e��Ԃ̌o�ߎ��Ԃ��v��.
    /// </summary>
    private void StateTime()
    {
        if(!_currentHitStop)
        {
            _stateTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// �e��Ԃ̌o�ߎ��Ԃ����Z�b�g.
    /// </summary>
    private void ResetTime()
    {
        _stateTime = 0;
    }

    /// <summary>
    /// X���AY���̉�]���Œ�.
    /// </summary>
    private void FixedRotate()
    {
        _transform.Rotate(0, _transform.rotation.y, 0);
    }

    /// <summary>
    /// ���ɏ�����.
    /// </summary>
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
        _stickPosition.transform.position = new Vector3(transform.position.x + _moveVelocity.x * 5, transform.position.y, transform.position.z + _moveVelocity.z * 5);
        // ����̓����蔻��̕\����\��.
        _weaponObject.SetActive(_weaponActive);
        // �U���͂̑��.
        _attackDamage = _attackPower * _MonsterFleshy;
        // ���C�Q�[�W�ԓK�p�������ɍU���͂��㏸������.
        if(_applyRedRenkiGauge)
        {
            _attackDamage *= 1.12f;
        }

        // �X�^�~�i�̎����񕜂��s�����ǂ���.
        _autoRecaveryStaminaFlag = _currentState != _dash &&
            _currentState != _avoid &&
            _currentState != _fatigueDash &&
            _currentState != _avoidDrawnSword &&
            _currentState != _rightAvoid &&
            _currentState != _leftAvoid &&
            _currentState != _backAvoid;

        RenkiGaugeDraw();
    }

    

    /// <summary>
    /// ���C�Q�[�W�ԂɂȂ�ƌ��ʂ�K�p.
    /// </summary>
    private void ApplyRedRenkiGauge()
    {
        if (_currentRenkiGauge > 75)
        {
            _applyRedRenkiGauge = true;
        }
        else if(_currentRenkiGauge < 75)
        {
            _applyRedRenkiGauge = false;
        }
    }

    /// <summary>
    /// �v���C���[�̎���p.
    /// </summary>
    private void viewAngle()
    {
        Vector3 direction = _stickPosition.transform.position - _transform.position;
        // �n���^�[�ƃf�o�b�O�p�L���[�u�̃x�N�g���̂Ȃ��p.
        // �f�o�b�O�p�L���[�u�̐���.
        float forwardAngle = Vector3.Angle(direction, _transform.forward);
        // �I�u�W�F�N�g�̑���.
        float sideAngle = Vector3.Angle(direction, _transform.right);

        RaycastHit hit;
        bool ray = Physics.Raycast(_transform.position, direction.normalized, out hit);

        bool viewFlag = ray && hit.collider.gameObject == _stickPosition && GetDistance() > 1;

        // ����.
        if (forwardAngle < 45)
        {
            FoundFlag((int)viewDirection.FORWARD);
        }
        // ���.
        else if (forwardAngle > 135)
        {
            FoundFlag((int)viewDirection.BACKWARD);
        }
        // �E.
        else if (sideAngle < 45)
        {
            FoundFlag((int)viewDirection.RIGHT);
        }
        // ��.
        else if (sideAngle > 135)
        {
            FoundFlag((int)viewDirection.LEFT);
        }
        // �����A
        else if(forwardAngle == 0 && sideAngle == 0)
        {
            FoundFlag((int)viewDirection.NONE);
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

    private void ConsumeStamina()
    {
        _stamina -= _isDashStaminaCost;
    }

    /// <summary>
    /// �X�^�~�i�̎�����.
    /// </summary>
    private void AutoRecoveryStamina()
    {
        _stamina += _autoRecaveryStamina;
    }

    /// <summary>
    /// ������̈ړ�
    /// </summary>
    private void MoveAvoid()
    {
        // ����.
        //if (_avoidTime <= 15)
        //{
        //    _rigidbody.velocity *= _deceleration;
        //}

        //// ��C�Ɍ���.
        //if (_avoidTime >= 55)
        //{
        //    _rigidbody.velocity *= 0.8f;
        //}

        // ��x������ʂ��Ǝ��͒ʂ��Ȃ��悤�ɂ���.
        if (!_isProcess) return;

        // �������Ƃ��̗͂�������.
        _rigidbody.AddForce(_avoidVelocity, ForceMode.Impulse);

        _isProcess = false;

    }

    /// <summary>
    /// �B�C�Q�[�W���R����.
    /// </summary>
    private void RenkiNaturalConsume()
    {
        // ���C�Q�[�W�������˔j��ێ����ԈȊO�ɏ���.
        if (_currentRenkiGauge <= 0 || _maintainTimeRenkiGauge > 0) return;
        _currentRenkiGauge -= 0.03f;
    }

    /// <summary>
    /// ���C�Q�[�W�ێ����Ԃ̌o��.
    /// </summary>
    private void MaintainElapsedTimeRenkiGauge()
    {
        if(_maintainTimeRenkiGauge <= 0) return;
        _maintainTimeRenkiGauge--;
    }

    /// <summary>
    /// �B�C�Q�[�W�̑�������.
    /// </summary>
    public void RenkiGaugeFluctuation()
    {
        if (GetIsCauseDamage())
        {
            // �B�C�Q�[�W����.
            _currentRenkiGauge += _increaseAmountRenkiGauge;
            // ���΂炭���C�Q�[�W�����炳�Ȃ�.
            _maintainTimeRenkiGauge = _maintainTime;
        }
    }

    /// <summary>
    /// ���C�Q�[�W�Ԃ̕\��.
    /// </summary>
    private void RenkiGaugeDraw()
    {
        // �B�C�Q�[�W�Ԃ̃o�t��K�p�����ۂɕ\��.
        if(_applyRedRenkiGauge)
        {
            _currentRedRenkiGauge = 100;
        }
        else
        {
            _currentRedRenkiGauge = 0;
        }
    }

    /// <summary>
    /// �ϐ��̏��������˔j���Ȃ�.
    /// </summary>
    /// <param name="currentVariable">���݂̕ϐ�</param>
    /// <param name="maxVariable">�ϐ�����l</param>
    private void LimitStop(ref float currentVariable, float maxVariable)
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
    
    /// <summary>
    /// ���[�V�������̑O�i.
    /// </summary>
    /// <param name="speedPower">�O�i�����</param>
    private void ForwardStep(float speedPower)
    {
        _rigidbody.velocity = _transform.forward * speedPower;
    }

    /// <summary>
    /// ��ԑJ�ڂ��s��.
    /// </summary>
    /// <param name="transitionFlag">��ԑJ�ڂ��s�����߂̃t���O</param>
    /// <param name="nextState">�J�ڐ�̏��</param>
    private void TransitionState(bool transitionFlag, StateBase nextState)
    {
        if (transitionFlag)
        {
            StateTransition(nextState);
        }
    }


    /// <summary>
    /// �X�e�B�b�N��|���ƈړ���ԂɑJ��.
    /// </summary>
    private void TransitionMove()
    {
        if (_stateTransitionFlag[(int)StateTransitionKinds.RUN])
        {
            RunOrDash();
        }
    }

    /// <summary>
    /// ���邩�_�b�V�����邩�����߂�.
    /// </summary>
    private void RunOrDash()
    {
        // ���j���[�J���Ă���Ƃ��̓_�b�V�����Ȃ�.
        TransitionState(_input._RBButton && !_openMenu, _dash);
        TransitionState(!_input._RBButton, _running);
    }

    /// <summary>
    /// �_���[�W���󂯂����ɑ̗͂����炵��ԑJ��.
    /// </summary>
    private void OnDamage()
    {
        // �̗͂�0�̎��͒ʂ��Ȃ�.
        if (_currentHitPoint <= 0) return;

        _currentHitPoint = _currentHitPoint - _MonsterState.GetMonsterAttack();

        StateTransition(_damage);
    }

    /// <summary>
    /// �̗͂�0�ɂȂ������Ɏ��S��Ԃ�.
    /// </summary>
    private void OnDead()
    {
        StateTransition(_dead);
    }

    /// <summary>
    /// ���X�e�B�b�N�̓��͏��擾.
    /// </summary>
    private void GetStickInput()
    {
        // ���͏����.
        _leftStickHorizontal = _input._LeftStickHorizontal;
        _leftStickVertical = _input._LeftStickVertical;
    }

    /// <summary>
    /// �ړ����̉�]����.
    /// </summary>
    private void RotateDirection()
    {
        transform.forward = Vector3.Slerp(transform.forward, _moveVelocity, Time.deltaTime * _rotateSpeed);
    }

    /// <summary>
    /// SE��炷�Ƃ��̏���.
    /// </summary>
    /// <param name="flameNum1">�炷�t���[����</param>
    /// <param name="seName">SE�̎��</param>
    private void SEPlayTest(float flameNum1, int seName)
    {
        //if (_stateFlame == flameNum1)
        //{
        //    _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, seName);
        //}

        if ((_stateTime >= flameNum1 && _stateTime <= flameNum1 + 0.04f) && !_isPlayOneShot)
        {
            _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, seName);

            _isPlayOneShot = true;
        }
    }

    /// <summary>
    /// SE�Đ��������ǂ����̃t���O��false�Ƀ��Z�b�g.
    /// </summary>
    /// <param name="time">���Z�b�g����^�C�~���O</param>
    private void PlayOneShotReset(float time)
    {
        if ((_stateTime >= time && _stateTime <= time + 0.04f) && _isPlayOneShot)
        {
            _isPlayOneShot = false;
        }
    }

    /// <summary>
    /// SE��炷�Ƃ��̏���.
    /// </summary>
    /// <param name="flameNum1">�炷�t���[����</param>
    /// <param name="seName">SE�̎��</param>
    private void SEPlay(int flameNum1, int seName)
    {
        if(_stateFlame == flameNum1)
        {
            _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, seName);
        }
    }

    /// <summary>
    /// SE��炷�Ƃ��̏���.
    /// </summary>
    /// <param name="flameNum1">���ڂ̖炷�t���[����</param>
    /// <param name="flameNum2">���ڂ̖炷�t���[����</param>
    /// <param name="seName">SE�̎��</param>
    private void SEPlay(int flameNum1, int flameNum2, int seName)
    {
        if (_stateFlame == flameNum1 ||
            _stateFlame == flameNum2)
        {
            _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, seName);
        }
    }

    // �g��Ȃ��Ȃ�.
    /// <summary>
    /// SE��炷�Ƃ��̏���.
    /// </summary>
    /// <param name="flameNum1">���ڂ̖炷�t���[����</param>
    /// <param name="flameNum2">���ڂ̖炷�t���[����</param>
    /// <param name="flameNum3">�O��ڂ̖炷�t���[����</param>
    /// <param name="seName">SE�̎��</param>
    private void SEPlay(int flameNum1, int flameNum2, int flameNum3, int seName)
    {
        if (_stateFlame == flameNum1 ||
            _stateFlame == flameNum2 ||
            _stateFlame == flameNum3)
        {
            _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, seName);
        }
    }

    /// <summary>
    /// ���j���[���J���Ă��邩�̏�����.
    /// </summary>
    private void OpenMenu()
    {
        _openMenu = _mainSceneManager.GetOpenMenu();
    }

    /// <summary>
    /// �_�b�V�����Ă��邩�ǂ����̏��擾.
    /// </summary>
    /// <returns>�_�b�V�����Ă��邩�ǂ���</returns>
    public bool GetIsDashing() { return _isDashing; }

    /// <summary>
    /// ����t���[���̐����擾.
    /// </summary>
    /// <returns>������Ă���Ƃ��̃t���[����</returns>
    public int GetAvoidTime() { return _avoidTime; }

    /// <summary>
    /// ������Ă��邩�ǂ����̏��擾.
    /// </summary>
    /// <returns>��������Ă��邩�ǂ���</returns>
    public bool GetIsAvoiding() { return _isAvoiding; }

    /// <summary>
    /// �񕜂��Ă��鎞�Ԏ擾.
    /// </summary>
    /// <returns>�񕜂��Ă��鎞��</returns>
    public int GetRecoveryTime() { return _currentRecoveryTime; }

    /// <summary>
    /// �񕜂��Ă��邩�ǂ����̏��擾.
    /// </summary>
    /// <returns>�񕜂��Ă��邩�ǂ���</returns>
    public bool GetIsRecovery() { return _isRecovery; }

    /// <summary>
    /// �c��̗�.
    /// </summary>
    /// <returns>�̗�</returns>
    public float GetHitPoint() { return _currentHitPoint; }

    /// <summary>
    /// �̗͍ő�l.
    /// </summary>
    /// <returns>�̗͂̍ő�l</returns>
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

    /// <summary>
    /// �J�E���^�[��t���Ă��邩�ǂ����擾.
    /// </summary>
    /// <returns></returns>
    public bool GetCounterValid() {  return _counterValid; }

    /// <summary>
    /// �X�e�B�b�N�̌X���ɂ���ċ��������߂�. 
    /// </summary>
    /// <returns>�X�e�B�b�N�̌X���ƃv���C���[�̋���</returns>
    private float GetDistance()
    {
        _currentDistance = (_stickPosition.transform.position - _transform.position).magnitude;
        return _currentDistance;
    }

    // �񕜖�̐����擾.
    public int GetCureMedicineNum() { return _cureMedicineNum; }
}
