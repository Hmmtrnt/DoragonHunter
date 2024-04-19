/*�����X�^�[�̑S�̂̊֐�*/

using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.GridLayoutGroup;

public partial class MonsterState
{
    // �ȉ���4�̊֐��̏������ύX�s��.
    /// <summary>
    /// ������.
    /// </summary>
    private void Init()
    {
        Initialization();
        _currentState.OnEnter(this, null);
    }

    /// <summary>
    /// �X�V����.
    /// </summary>
    private void UpdateProcess()
    {
        _currentState.OnUpdate(this);
        _currentState.OnChangeState(this);
        ViewAngle();
        WeakenState();
        // ��Ԃ̌o�ߎ��Ԃ𑝂₷.
        StateTime();
    }

    /// <summary>
    /// �Œ�X�V����.
    /// </summary>
    private void FixedUpdateProcess()
    {
        // �v�Z���̑��.
        SubstituteVariable();

        // ��Ԃ̃t���[���̎��Ԃ𑝂₷.
        _stateFlame++;

        _currentState.OnFixedUpdate(this);

        // ��������ɗ^����.
        _randomNumber = Random.Range(1, 101);

        // �v���C���[�ƃ����X�^�[���m�̊p�x�A�����ɂ���ď�����ύX.
        PositionalRelationship();
        // �A�j���[�V�����J��.
        AnimTransition();

        // �ŏ��������K����悤�ɂ���.
        if (_isRoar && _stateFlame >= 10)
        {
            RoarTransition();
        }

        // ���ݒl�����܂������Ɛ����Ă���Ƃ��ɏ���.
        if (_falterValue >= _falterMaxValue && _HitPoint > 0)
        {
            ChangeFlater();
        }

        // �̗͂�0�ɂȂ������̏���.
        if (_HitPoint <= 0)
        {
            if(!_tutorialState)
            {
                ChangeStateDeath();
            }
        }
        // �̗͂�0�����ɂ��Ȃ�.
        HitPointLowerLimit();

        // �_���[�W���󂯂�.
        if (_takeDamage && !_tutorialState)
        {
            GetOnDamager();
            GetOnFalter();
            _takeDamage = false;
        }

        //Debug.Log(_HitPoint);
    }

    /// <summary>
    /// �ђʂ����u��.
    /// </summary>
    /// <param name="other">�Ώۂ̓����蔻��</param>
    private void TriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HunterAtCol" && _playerState.GetIsCauseDamage())
        {
            _playerState.SetIsCauseDamage(false);
            _takeDamage = true;
        }
    }

    /// <summary>
    /// �X�e�[�g�̕ύX.
    /// </summary>
    /// <param name="nextState">���̏��</param>
    private void ChangeState(StateBase nextState)
    {
        _currentState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentState);
        _currentState = nextState;
    }

    /// <summary>
    /// ������.
    /// </summary>
    private void Initialization()
    {
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
        _hunter = GameObject.Find("Hunter");
        _trasnform = transform;
        _playerState = _hunter.GetComponent<PlayerState>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fireBall = (GameObject)Resources.Load("FireBall2");
        _fireBallPosition = GameObject.Find("BlessPosition");
        _footSmokePrehub[0] = (GameObject)Resources.Load("MonsterLegSmoke");
        _footSmokePrehub[1] = (GameObject)Resources.Load("MonsterTailSmoke");
        _footSmokePrehub[2] = (GameObject)Resources.Load("MonsterWingSmoke");

        _colliderChildren = GetComponentsInChildren<MeshCollider>();

        _animator = GetComponent<Animator>();


        for (int i = 0; i < (int)viewDirection.NONE; i++)
        {
            _viewDirection[i] = false;
        }
        _idleMotion = true;
        // ����i����.
        _isRoar = true;

        // �U�������蔻��𖳌�.
        _biteCollisiton.SetActive(false);
        _rushCollisiton.SetActive(false);
        _wingLeftCollisiton.SetActive(false);
        _wingRightCollisiton.SetActive(false);
        for(int tailColNum = 0; tailColNum < _tailCollisiton.Length; tailColNum++) 
        {
            _tailCollisiton[tailColNum].SetActive(false);
        }
        _rotateCollisiton.SetActive(false);

#if false
        // �̗͂̌���.
        //if(_mainSceneManager._hitPointMany)
        //{
        //    _MaxHitPoint = 10000;
        //}
        //else
        //{
        //    _MaxHitPoint = 5000;
        //}
#else
        _MaxHitPoint = 10;
#endif


        _weakenTimingHitPoint = _MaxHitPoint / 4;
        _HitPoint = _MaxHitPoint;

        _randomNumber = 0;

        // �`���[�g���A����Ԃ�������
        _tutorialState = SceneManager.GetActiveScene().name == "TutorialScene";
    }

    /// <summary>
    /// �����X�^�[�̏�Ԃ���点��.
    /// </summary>
    private void WeakenState()
    {
        if(_HitPoint <= _weakenTimingHitPoint)
        {
            _weakenState = true;
        }
    }

    /// <summary>
    /// �v�Z�������̑��.
    /// </summary>
    private void SubstituteVariable()
    {
        // �����X�^�[�̐���
        Vector3 monsterForward = Vector3.Scale(transform.forward, new Vector3(1.0f,0.0f,1.0f)).normalized;

        // �O��.
        Vector3 moveForward = monsterForward * _forwardSpeed;
        // ���E.
        Vector3 moveSide = transform.right * _sideSpeed;
        // ���x�̑��
        _moveVelocity = moveForward + moveSide;
    }

    /// <summary>
    /// ��ԑJ�ڎ��̏�����.
    /// </summary>
    private void StateTransitionInitialization()
    {
        _stateFlame = 0;
        _stateTime = 0;
        _isPlayOneShot = false;
    }

    /// <summary>
    /// �A�j���[�V�����J��.
    /// </summary>
    private void AnimTransition()
    {
        // �A�j���[�^�[���A�^�b�`����Ă��Ȃ���Έ���������
        Debug.Assert(_currentState != null);

        // bool
        _animator.SetBool("Roar", _roarMotion);
        _animator.SetBool("Idle", _idleMotion);
        _animator.SetBool("WeakenIdle", _weakenMotion);
        _animator.SetBool("Falter", _falterMotion);
        _animator.SetBool("Death", _deathMotion);
        _animator.SetBool("Bless", _blessMotion);
        _animator.SetBool("Bite", _biteMotion);
        _animator.SetBool("Tail", _tailMotion);
        _animator.SetBool("WingLeft", _wingLeftMotion);
        _animator.SetBool("WingRight", _wingRightMotion);
        _animator.SetBool("ForwardRush", _rushMotion);
        _animator.SetBool("Rota", _rotateMotion);
    }

    /// <summary>
    /// ��Ԃ̌o�ߎ��Ԏ擾.
    /// </summary>
    private void StateTime()
    {
        _stateTime += Time.deltaTime;
    }

    /// <summary>
    /// ���K���[�V�����ɑJ��.
    /// </summary>
    private void RoarTransition()
    {
        ChangeState(_roar);
    }

    /// <summary>
    /// �v���C���[�ƃ����X�^�[���m�̊p�x�A�����ɂ���ď�����ύX.
    /// </summary>
    private void PositionalRelationship()
    {
        // ����
        if (_viewDirection[(int)viewDirection.FORWARD])
        {
            if (GetDistance() <= _shortDistance)
            {
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _isNearDistance = false;
            }
        }
        // �w��
        else if (_viewDirection[(int)viewDirection.BACKWARD])
        {
            if (GetDistance() <= _shortDistance)
            {
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _isNearDistance = false;
            }
        }
        // �E
        else if (_viewDirection[(int)viewDirection.RIGHT])
        {
            if (GetDistance() <= _shortDistance)
            {
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _isNearDistance = false;
            }
        }
        // ��
        else if (_viewDirection[(int)viewDirection.LEFT])
        {
            if (GetDistance() <= _shortDistance)
            {
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _isNearDistance = false;

            }
        }
    }

    /// <summary>
    /// �v���C���[���������X�^�[���猩�Ăǂ��ɂ���̂����擾����.
    /// </summary>
    private void ViewAngle()
    {
        Vector3 direction = _hunter.transform.position - _trasnform.position;
        // �I�u�W�F�N�g�ƃv���C���[�̃x�N�g���̂Ȃ��p
        // �I�u�W�F�N�g�̐���.
        float forwardAngle = Vector3.Angle(direction, _trasnform.forward);
        // �I�u�W�F�N�g�̑���.
        float sideAngle = Vector3.Angle(direction, _trasnform.right);

        RaycastHit hit;
        bool ray = Physics.Raycast(_trasnform.position, direction.normalized, out hit);

        bool viewFlag = ray && hit.collider.gameObject == _hunter;

        if (!viewFlag) return;

        // ����.
        if (forwardAngle < 90 * 0.5f)
        {
            FoundFlag((int)viewDirection.FORWARD);
        }
        // ���.
        else if (forwardAngle > 135 && forwardAngle < 180)
        {
            FoundFlag((int)viewDirection.BACKWARD);
        }
        // �E.
        else if (sideAngle < 90 * 0.5f)
        {
            FoundFlag((int)viewDirection.RIGHT);
        }
        // ��.
        else if (sideAngle > 135 && sideAngle < 180)
        {
            FoundFlag((int)viewDirection.LEFT);
        }
        else
        {
            FoundFlag((int)viewDirection.NONE);
        }
    }

    /// <summary>
    /// �������Ă��邩�̒l��Ԃ�.
    /// </summary>
    /// <param name="foundNum">�v���C���[�̈ʒu�������ԍ�</param>
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

    /// <summary>
    /// ���ݒl�����܂������̏���.
    /// </summary>
    private void ChangeFlater()
    {
        // ���ݒl�����Z�b�g.
        _falterValue = 0;
        //_falterMaxValue = 0;
        ChangeState(_falter);
    }

    /// <summary>
    /// �̗͂�0�ɂȂ�Ƌ����I�Ƀ_�E������.
    /// </summary>
    private void ChangeStateDeath()
    {
        _currentState = _down;
        ChangeState(_down);
    }

    /// <summary>
    /// �̗͂�0�����ɂ��Ȃ�.
    /// </summary>
    private void HitPointLowerLimit()
    {
        if(_HitPoint <= 0)
        {
            _HitPoint = 0;
        }
    }

    /// <summary>
    /// �v���C���[�̕�������.
    /// </summary>
    /// <param name="turnFlame">�����X�s�[�h</param>
    private void TurnTowards(int turnFlame)
    {
        // �^�[�Q�b�g�̕����x�N�g��.
        Vector3 _direction = new Vector3(_hunter.transform.position.x - transform.position.x,
            0.0f, _hunter.transform.position.z - transform.position.z);
        // �����x�N�g������N�H�[�^�j�I���擾
        Quaternion _rotation = Quaternion.LookRotation(_direction, Vector3.up);

        // �v���C���[�̂ق��������ĉ�]
        if (_stateFlame <= turnFlame)
        {
            _trasnform.rotation = Quaternion.Slerp(_trasnform.rotation, _rotation, Time.deltaTime * _rotateSpeed);
        }
    }

    /// <summary>
    /// �����G�t�F�N�g�̐���.
    /// </summary>
    /// <param name="footSmokeKinds">���̎��</param>
    /// <param name="footSmokePosition">�����^�C�~���O</param>
    private void FootSmokeSpawn(int footSmokeKinds, int footSmokePosition)
    {
        Instantiate(_footSmokePrehub[footSmokeKinds],
                _footSmokePosition[footSmokePosition].transform.position,
                Quaternion.identity);
    }

    /// <summary>
    /// SE��炷����.
    /// </summary>
    /// <param name="flameNum1">�炷�^�C�~���O�̕b��</param>
    /// <param name="seName">�炷���y�̎��</param>
    private void SEPlay(float flameNum1, int seName)
    {
        if ((_stateTime >= flameNum1 && _stateTime <= flameNum1 + 0.04f) && !_isPlayOneShot)
        {
            _seManager.MonsterPlaySE((int)SEManager.AudioNumber.AUDIO3D, seName);

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
    /// �U���p�^�[��AI�̏���.
    /// </summary>
    private void AttackStateAi()
    {
        // �ߋ���.
        if (_isNearDistance)
        {
            // ����(��ɂ��݂�).
            if (_viewDirection[(int)viewDirection.FORWARD])
            {
                if (_randomNumber <= 30)
                {
                    ChangeState(_bite);
                }
                else if (_randomNumber <= 70)
                {
                    ChangeState(_rush);
                }
                else
                {
                    ChangeState(_rotate);
                }
            }
            // ���.
            else if (_viewDirection[(int)viewDirection.BACKWARD])
            {
                if (_randomNumber <= 30)
                {
                    ChangeState(_rotate);
                }
                else if (_randomNumber <= 60)
                {
                    ChangeState(_tail);
                }
                else
                {
                    ChangeState(_bite);
                }
            }
            // ��.
            else if (   _viewDirection[(int)viewDirection.LEFT])
            {
                if (_randomNumber <= 20)
                {
                    ChangeState(_rotate);
                }
                else if (_randomNumber <= 60)
                {
                    ChangeState(_wingBlowLeft);
                }
                else
                {
                    ChangeState(_bite);
                }
            }
            // �E.
            else if (_viewDirection[(int)viewDirection.RIGHT])
            {
                if (_randomNumber <= 20)
                {
                    ChangeState(_rotate);
                }
                else if (_randomNumber <= 60)
                {
                    ChangeState(_wingBlowRight);
                }
                else
                {
                    ChangeState(_bite);
                }
            }
        }
        // ������.
        else
        {
            // ����.
            if (_viewDirection[(int)viewDirection.FORWARD])
            {
                if (_randomNumber <= 60)
                {
                    ChangeState(_rush);
                }
                else
                {
                    ChangeState(_bless);
                }
            }
            // �w��.
            else if (_viewDirection[(int)viewDirection.BACKWARD])
            {
                if (_randomNumber <= 40)
                {
                    ChangeState(_rush);
                }
                else
                {
                    ChangeState(_bless);
                }
            }
            // ��.
            else if (_viewDirection[(int)viewDirection.LEFT])
            {
                ChangeState(_bless);
            }
            // �E,
            else if (_viewDirection[(int)viewDirection.RIGHT])
            {
                ChangeState(_bless);
            }
        }
    }

    /// <summary>
    /// �`���[�g���A���V�[����AI.
    /// </summary>
    private void TutorialAI()
    {
        // �U���\�Ƀ`�F�b�N����ꂽ��U�����s��.
        //if ()
        //{

        //}
    }


    /// <summary>
    /// �v���C���[�ƃ����X�^�[���m�̋����擾.
    /// </summary>
    /// <returns></returns>
    private float GetDistance()
    {
        _currentDistance = (_hunter.transform.position - _trasnform.position).magnitude;

        return _currentDistance;
    }

    /// <summary>
    /// �����X�^�[�̍U����.
    /// </summary>
    /// <returns></returns>
    public float GetMonsterAttack()
    {
        return _AttackPower;
    }

    /// <summary>
    /// �_���[�W���󂯂���̗̑�.
    /// </summary>
    /// <returns></returns>
    private float GetOnDamager()
    {
        _HitPoint = _HitPoint - _playerState.GetHunterAttack();
        return _HitPoint;
    }

    /// <summary>
    /// ���ݒl��~��.
    /// </summary>
    /// <returns></returns>
    private float GetOnFalter()
    {
        _falterValue = _falterValue + _playerState.GetHunterAttack();
        return _falterValue;
    }
    /// <summary>
    /// �̗͂��擾
    /// </summary>
    /// <returns></returns>
    public float GetHitPoint()
    {
        return _HitPoint;
    }
}
