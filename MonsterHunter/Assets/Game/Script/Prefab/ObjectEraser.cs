/*�v���n�u�̎��R����*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEraser : MonoBehaviour
{
    // ���݂��Ă��鎞��.
    private int _currentTime = 0;

    private void Update()
    {
        _currentTime++;
        // ������x���Ԃ����ƃG�t�F�N�g������.
        if(_currentTime >= 100)
        {
            Destroy(gameObject);
        }
    }
}
