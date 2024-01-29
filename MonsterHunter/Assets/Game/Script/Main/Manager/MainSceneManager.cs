/*���C���V�[���}�l�[�W���[*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // BGM�}�l�[�W���[.
    private BGMManager _bgmManager;
    // �|�[�Y���.
    private MainSceneSelectUi _mainSceneSelectUi;
    // �ꎞ��~.
    private PauseTimeStop _pauseTimeStop;
    // �ꎞ��~���Ă��邩�ǂ���.
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
        // �Q�[���̗�����~�߂��蓮�������肷��.
        if(_pauseStop)
        {
            _pauseTimeStop.StopTime();
        }
        else
        {
            _pauseTimeStop.StartTime();
        }

        // �ꎞ��~���������Ƃ��̏���.
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
