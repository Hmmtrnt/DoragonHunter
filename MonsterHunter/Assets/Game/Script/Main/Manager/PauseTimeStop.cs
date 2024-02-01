/*一時停止した時にゲーム全体の動きをストップ*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PauseTimeStop : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachinePOV _cinemachinePOV;
    // カメラの回転量の保持.
    private float _originalHorizontalAxisMaxSpeed;
    private float _originalVerticalAxisMaxSpeed;

    void Start()
    {
        _cinemachineVirtualCamera = GameObject.Find("CameraBase").GetComponent<CinemachineVirtualCamera>();
        _cinemachinePOV = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        // カメラの元の回転する値を保持.
        _originalHorizontalAxisMaxSpeed = _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed;
        _originalVerticalAxisMaxSpeed = _cinemachinePOV.m_VerticalAxis.m_MaxSpeed;
    }

    // 一時停止させる.
    public void StopTime()
    {
        Time.timeScale = 0.0f;

        // カメラの回転を無効にする.
        //_cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = 0;
        //_cinemachinePOV.m_VerticalAxis.m_MaxSpeed = 0;
    }

    // 一時停止を解除.
    public void StartTime()
    {
        Time.timeScale = 1.0f;
        // カメラの回転を有効化.
        //_cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = _originalHorizontalAxisMaxSpeed;
        //_cinemachinePOV.m_VerticalAxis.m_MaxSpeed = _originalVerticalAxisMaxSpeed;
    }
    
}
