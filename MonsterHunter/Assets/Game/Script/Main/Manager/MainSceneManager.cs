/*メインシーンマネージャー*/

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
    // 一時停止しているかどうか.
    private bool _pauseStop = false;

    // メニュー画面を開いているか.
    public bool _openMenu = false;
    // オプション画面を開いているか.
    public bool _openOption = false;

    void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        _mainSceneSelectUi = GameObject.Find("SelectItem").GetComponent<MainSceneMenuSelectUi>();
        _pauseTimeStop = GetComponent<PauseTimeStop>();
    }

    void Update()
    {
        // ゲームの流れを止めたり動かしたりする.
        if(_pauseStop)
        {
            _pauseTimeStop.StopTime();
        }
        else
        {
            _pauseTimeStop.StartTime();
        }

        // 一時停止を押したときの処理.
        if(_mainSceneSelectUi._selectNum == (int)MainSceneMenuSelectUi.SelectItem.PAUSE && 
            _controllerManager._AButtonDown &&
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

                _openMenu = false;
            }
        }
    }

    // メニュー画面を開いているかどうか.
    public bool GetOpenMenu() { return _openMenu; }
    // オプション画面を開いているかどうか.
    public bool GetOpenOption() { return _openOption; }
}
