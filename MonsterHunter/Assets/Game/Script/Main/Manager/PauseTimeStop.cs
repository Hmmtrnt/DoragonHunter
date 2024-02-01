/*�ꎞ��~�������ɃQ�[���S�̂̓������X�g�b�v*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PauseTimeStop : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachinePOV _cinemachinePOV;
    // �J�����̉�]�ʂ̕ێ�.
    private float _originalHorizontalAxisMaxSpeed;
    private float _originalVerticalAxisMaxSpeed;

    void Start()
    {
        _cinemachineVirtualCamera = GameObject.Find("CameraBase").GetComponent<CinemachineVirtualCamera>();
        _cinemachinePOV = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        // �J�����̌��̉�]����l��ێ�.
        _originalHorizontalAxisMaxSpeed = _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed;
        _originalVerticalAxisMaxSpeed = _cinemachinePOV.m_VerticalAxis.m_MaxSpeed;
    }

    // �ꎞ��~������.
    public void StopTime()
    {
        Time.timeScale = 0.0f;

        // �J�����̉�]�𖳌��ɂ���.
        //_cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = 0;
        //_cinemachinePOV.m_VerticalAxis.m_MaxSpeed = 0;
    }

    // �ꎞ��~������.
    public void StartTime()
    {
        Time.timeScale = 1.0f;
        // �J�����̉�]��L����.
        //_cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = _originalHorizontalAxisMaxSpeed;
        //_cinemachinePOV.m_VerticalAxis.m_MaxSpeed = _originalVerticalAxisMaxSpeed;
    }
    
}
