/*�����̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingSmoke : MonoBehaviour
{
    // ��������
    private int _countLife;
    // �����X�^�[���.
    private MonsterState _monsterState;

    void Start()
    {
        _monsterState = GameObject.Find("Dragon").GetComponent<MonsterState>();

    }

    private void FixedUpdate()
    {
        _countLife++;

        // �p�[�e�B�N������������.
        if (_countLife == 50)
        {
            Destroy(gameObject);
        }

        ParticlePositionDesignation();
    }

    // �p�[�e�B�N���̍��W�w��.
    private void ParticlePositionDesignation()
    {
        // ���ꂼ��̏�Ԃ̏��ɂ���ăp�[�e�B�N�������f���̃��b�V���ɌŒ�.
        if (_monsterState._currentWingAttackLeft)
        {
            transform.position = _monsterState._footSmokePosition[1].transform.position;
        }
        if (_monsterState._currentWingAttackRight)
        {
            transform.position = _monsterState._footSmokePosition[0].transform.position;
        }
    }
}
