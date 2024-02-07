/*メインシーンマネージャー*/

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    // モンスターの情報.
    private MonsterState _monsterState;
    // プレイヤーの情報.
    private PlayerState _playerState;

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
    // プレイ中UIの表示非表示.
    public bool _openGamePlayUi = true;

    // カメラの回転量の保持.
    private float _originalHorizontalAxisMaxSpeed;
    private float _originalVerticalAxisMaxSpeed;

    // ゲーム全体の時間を停止するまでの時間
    private int _pauseCount;

    void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        _mainSceneSelectUi = GameObject.Find("SelectItem").GetComponent<MainSceneMenuSelectUi>();
        _pauseTimeStop = GetComponent<PauseTimeStop>();
        _monsterState = GameObject.Find("Dragon").GetComponent<MonsterState>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _cinemachineVirtualCamera = GameObject.Find("CameraBase").GetComponent<CinemachineVirtualCamera>();
        _cinemachinePOV = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        // カメラの元の回転する値を保持.
        _originalHorizontalAxisMaxSpeed = _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed;
        _originalVerticalAxisMaxSpeed = _cinemachinePOV.m_VerticalAxis.m_MaxSpeed;

        _pauseCount = 15;
    }

    void Update()
    {
        // ゲームの流れを止めたり動かしたりする.
        if(_pauseStop)
        {
            _openMenu = false;
            _openPause = true;
            _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = 0;
            _cinemachinePOV.m_VerticalAxis.m_MaxSpeed = 0;

            if(_pauseCount == 0)
            {
                _pauseTimeStop.StopTime();
            }

            _pauseCount--;
        }
        else
        {
            _pauseTimeStop.StartTime();
            _openPause = false;
            _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = _originalHorizontalAxisMaxSpeed;
            _cinemachinePOV.m_VerticalAxis.m_MaxSpeed = _originalVerticalAxisMaxSpeed;
            _pauseCount = 15;
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

    private void FixedUpdate()
    {
        BGMChange();
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

    // モンスターを倒したときにBGMを変更する.
    private void BGMChange()
    {
        if(_monsterState.GetHitPoint() <= 0)
        {
            _bgmManager.BGMChange((int)BGMManager.BGM.VICTORY);
        }
        else if(_playerState.GetHitPoint() <= 0)
        {
            _bgmManager.BGMChange((int)BGMManager.BGM.FAILED);
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
    // プレイ中のUIを表示しているかどうか.
    public bool GetGamePlayUI() { return _openGamePlayUi; }

    // 現在一時停止中かどうか.
    public bool GetPauseStop() { return _pauseStop; }
}
