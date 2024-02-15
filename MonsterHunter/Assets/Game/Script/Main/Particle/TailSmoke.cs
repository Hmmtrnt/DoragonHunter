/*�����̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSmoke : MonoBehaviour
{
    // ��������
    private int _countLife;
    // �����X�^�[���.
    private Monster _monsterState;

    void Start()
    {
        _monsterState = GameObject.Find("Dragon").GetComponent<Monster>();

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
        if (_monsterState._currentRotateAttack)
        {
            transform.position = _monsterState._footSmokePosition[2].transform.position;
        }
    }
}
