/*�ꎞ��~�������ɃQ�[���S�̂̎��Ԃ��X�g�b�v*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTimeStop : MonoBehaviour
{
    void Start()
    {
        
    }

    // �ꎞ��~������.
    public void StopTime()
    {
        Time.timeScale = 0.0f;
    }

    // �ꎞ��~������.
    public void StartTime()
    {
        Time.timeScale = 1.0f;
    }
    
}
