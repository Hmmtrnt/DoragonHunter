/*メインシーンマネージャー*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // BGMマネージャー.
    private BGMManager _bgmManager;
    // ポーズ画面.
    private MainSceneSelectUi _mainSceneSelectUi;
    // 一時停止.
    private PauseTimeStop _pauseTimeStop;
    // 一時停止しているかどうか.
    private bool _pauseStop = false;

    void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        _mainSceneSelectUi = GameObject.Find("SelectItem").GetComponent<MainSceneSelectUi>();
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
        if(_mainSceneSelectUi._selectNum == (int)MainSceneSelectUi.SelectItem.PAUSE && 
            _controllerManager._AButtonDown &&
            !_pauseStop)
        {
            _pauseStop = true;
        }
        else if(_pauseStop && _controllerManager._PressAnyButton)
        {
            _pauseStop = false;
        }
        
    }
}
