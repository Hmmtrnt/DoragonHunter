/*メインシーンマネージャー*/

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MainSceneManager : MonoBehaviour
{
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // BGMマネージャー.
    private BGMManager _bgmManager;
    // ポーズ画面.
    private MainSceneMenuSelectUi _mainSceneSelectUi;
    // 一時停止.
    private PauseTimeStop _pauseTimeStop;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachinePOV _cinemachinePOV;

    // 一時停止しているかどうか.
    private bool _pauseStop = false;
    // メニュー画面を開いているか.
    public bool _openMenu = false;
    // オプション画面を開いているか.
    public bool _openOption = false;
    // ポーズ画面を開いているかどうか.
    public bool _openPause = false;
    // リタイア確認画面を開いているか.
    public bool _openRetireConfirmation = false;
    // カメラの回転量の保持.
    private float _originalHorizontalAxisMaxSpeed;
    private float _originalVerticalAxisMaxSpeed;

    void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        _mainSceneSelectUi = GameObject.Find("SelectItem").GetComponent<MainSceneMenuSelectUi>();
        _pauseTimeStop = GetComponent<PauseTimeStop>();
        _cinemachineVirtualCamera = GameObject.Find("CameraBase").GetComponent<CinemachineVirtualCamera>();
        _cinemachinePOV = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        // カメラの元の回転する値を保持.
        _originalHorizontalAxisMaxSpeed = _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed;
        _originalVerticalAxisMaxSpeed = _cinemachinePOV.m_VerticalAxis.m_MaxSpeed;
    }

    void Update()
    {
        // ゲームの流れを止めたり動かしたりする.
        if(_pauseStop)
        {
            _openMenu = false;
            _openPause = true;
            //_cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = 0;
            //_cinemachinePOV.m_VerticalAxis.m_MaxSpeed = 0;
            //_pauseTimeStop.StopTime();
        }
        else
        {
            _openPause = false;
            //_cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = _originalHorizontalAxisMaxSpeed;
            //_cinemachinePOV.m_VerticalAxis.m_MaxSpeed = _originalVerticalAxisMaxSpeed;
            //_pauseTimeStop.StartTime();
        }

        //Debug.Log(_cinemachinePOV.m_HorizontalAxis.m_MaxSpeed);

        // 一時停止を押したときの処理.
        if(_mainSceneSelectUi._selectNum == (int)MainSceneMenuSelectUi.SelectItem.PAUSE && 
            _controllerManager._AButtonDown && _openMenu &&
            !_pauseStop)
        {
            _pauseStop = true;
        }
        else if(_pauseStop && _controllerManager._PressAnyButton)
        {
            _pauseStop = false;
        }
        MenuOpneAndClose();
    }

    // メニュー画面の開閉制御.
    private void MenuOpneAndClose()
    {
        // 開くとき
        if (!_openMenu)
        {
            if (_controllerManager._MenuButtonDown)
            {
                _openMenu = true;
            }
        }
        // 閉じるとき
        else
        {
            if (_controllerManager._BButtonDown)
            {
                if (_openOption)
                {
                    _openOption = false;
                    return;
                }
                else if(_openRetireConfirmation)
                {
                    _openRetireConfirmation = false;
                    return;
                }

                _openMenu = false;
            }
        }
    }

    // メニュー画面を開いているかどうか.
    public bool GetOpenMenu() { return _openMenu; }
    // オプション画面を開いているかどうか.
    public bool GetOpenOption() { return _openOption; }
    // ポーズ画面を開いているかどうか.
    public bool GetOpenPause() { return _openPause; }

    // リタイア確認画面を開いているかどうか.
    public bool GetOpenRetireConfirmation() { return _openRetireConfirmation; }
}
