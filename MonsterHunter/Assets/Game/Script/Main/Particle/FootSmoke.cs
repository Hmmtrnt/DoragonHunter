/*�����̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSmoke : MonoBehaviour
{
    // ��������
    private int _countLife;

    private void FixedUpdate()
    {
        _countLife++;

        // �p�[�e�B�N������������.
        if (_countLife == 50)
        {
            Destroy(gameObject);
        }
    }
}
