/*�n���^�[�̍U���G�t�F�N�g*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHitEffect : MonoBehaviour
{
    // ���݂��Ă��鎞��.
    private int _currentTime = 0;

    private void FixedUpdate()
    {
        _currentTime++;
        // ������x���Ԃ����ƃG�t�F�N�g������.
        if(_currentTime >= 100)
        {
            Destroy(gameObject);
        }
    }
}
