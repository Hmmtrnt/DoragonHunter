/*���C���V�[���}�l�[�W���[*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MainSceneManager : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // BGM�}�l�[�W���[.
    private BGMManager _bgmManager;
    // �|�[�Y���.
    private MainSceneMenuSelectUi _mainSceneSelectUi;
    // �ꎞ��~.
    private PauseTimeStop _pauseTimeStop;
    // �ꎞ��~���Ă��邩�ǂ���.
    private bool _pauseStop = false;

    // ���j���[��ʂ��J���Ă��邩.
    public bool _openMenu = false;
    // �I�v�V������ʂ��J���Ă��邩.
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

    // ���j���[��ʂ̊J����.
    private void MenuOpneAndClose()
    {
        // �J���Ƃ�
        if (!_openMenu)
        {
            if (_controllerManager._MenuButtonDown)
            {
                _openMenu = true;
            }
        }
        // ����Ƃ�
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

    // ���j���[��ʂ��J���Ă��邩�ǂ���.
    public bool GetOpenMenu() { return _openMenu; }
    // �I�v�V������ʂ��J���Ă��邩�ǂ���.
    public bool GetOpenOption() { return _openOption; }
}
