/*�����X�^�[�̑S�̂̊֐�*/

using UnityEngine;
using UnityEngine.UI;

public partial class MonsterState
{
    // �X�e�[�g�̕ύX.
    private void ChangeState(StateBase nextState)
    {
        _currentState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentState);
        _currentState = nextState;
    }

    // ������.
    private void Initialization()
    {
        _hunter = GameObject.Find("Hunter");
        _trasnform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _playerState = _hunter.GetComponent<PlayerState>();

        _fireBall = (GameObject)Resources.Load("FireBall");
        _fireBallPosition = GameObject.Find("BlessPosition");


        _colliderChildren = GetComponentsInChildren<MeshCollider>();

        _animator = GetComponent<Animator>();

        _text = GameObject.Find("DebugText").GetComponent<Text>();
        _textHp = GameObject.Find("MonsterHp").GetComponent<Text>();

        for (int i = 0; i < (int)viewDirection.NONE; i++)
        {
            _viewDirection[i] = false;
        }
        _idleMotion = true;
        //_isRoar = true;
        _isRoar = false;

        _biteCollisiton.SetActive(false);
    }

    // ��ԑJ�ڎ��̏�����.
    private void StateTransitionInitialization()
    {
        _stateFlame = 0;
    }

    // �A�j���[�V�����J��.
    private void AnimTransition()
    {
        // �A�j���[�^�[���A�^�b�`����Ă��Ȃ���Έ���������
        Debug.Assert(_currentState != null);

        // bool
        _animator.SetBool("Roar", _roarMotion);
        _animator.SetBool("Idle", _idleMotion);
        _animator.SetBool("Death", _deathMotion);
        _animator.SetBool("Bless", _blessMotion);
        _animator.SetBool("Bite", _biteMotion);
        _animator.SetBool("Tail", _tailMotion);
        _animator.SetBool("WingLeft", _wingLeftMotion);
        _animator.SetBool("WingRight", _wingRightMotion);
        _animator.SetBool("ForwardRush", _rushMotion);
        _animator.SetBool("Rota", _rotateMotion);
    }

    // ���K���[�V�����ɑJ��.
    private void RoarTransition()
    {
        ChangeState(_roar);
    }

    // �v���C���[�ƃ����X�^�[���m�̊p�x�A�����ɂ���ď�����ύX.
    private void PositionalRelationship()
    {
        // ����
        if (_viewDirection[(int)viewDirection.FORWARD])
        {
            if (GetDistance() <= _shortDistance)
            {
                _text.text = "���ʋߋ���";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "���ʉ�����";
                _isNearDistance = false;
            }
        }
        // �w��
        else if (_viewDirection[(int)viewDirection.BACKWARD])
        {
            if (GetDistance() <= _shortDistance)
            {
                _text.text = "�w��ߋ���";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "�w�㉓����";
                _isNearDistance = false;
            }
        }
        // �E
        else if (_viewDirection[(int)viewDirection.RIGHT])
        {
            if (GetDistance() <= _shortDistance)
            {
                _text.text = "�E�ߋ���";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "�E������";
                _isNearDistance = false;
            }
        }
        // ��
        else if (_viewDirection[(int)viewDirection.LEFT])
        {
            if (GetDistance() <= _shortDistance)
            {
                _text.text = "���ߋ���";
                _isNearDistance = true;
            }
            else if (GetDistance() >= _shortDistance && GetDistance() <= _longDistance)
            {
                _text.text = "��������";
                _isNearDistance = false;

            }
        }
        if (GetDistance() >= _longDistance)
        {
            _text.text = "NONE";
        }


    }

    // �v���C���[���������X�^�[���猩�Ăǂ��ɂ���̂����擾����
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

    // �̗͂�0�ɂȂ�Ƌ����I�Ƀ_�E������.
    private void ChangeStateDeath()
    {
        _currentState = _down;
        // ���ʂƓ����蔻����ђʂ�����.
        foreach(MeshCollider collider in _colliderChildren)
        {
            collider.isTrigger = true;
        }
    }

    public void DamageUI(Collider col)
    {
        var obj = Instantiate(_damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
    }

    private float GetDistance()
    {
        _currentDistance = (_hunter.transform.position - _trasnform.position).magnitude;

        return _currentDistance;
    }

    public float GetMonsterAttack()
    {
        return _AttackPower;
    }

    private float GetOnDamager()
    {
        _HitPoint = _HitPoint - _playerState.GetHunterAttack();
        return _HitPoint;
    }

    public void SetHitPoint(float hitPoint)
    {
        _HitPoint = hitPoint;
    }

    public float GetHitPoint()
    {
        return _HitPoint;
    }
}
