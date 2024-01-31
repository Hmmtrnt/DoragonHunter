/*���C���V�[���}�l�[�W���[*/

using Cinemachine;
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

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachinePOV _cinemachinePOV;

    // �ꎞ��~���Ă��邩�ǂ���.
    private bool _pauseStop = false;
    // ���j���[��ʂ��J���Ă��邩.
    public bool _openMenu = false;
    // �I�v�V������ʂ��J���Ă��邩.
    public bool _openOption = false;
    // �|�[�Y��ʂ��J���Ă��邩�ǂ���.
    public bool _openPause = false;
    // ���^�C�A�m�F��ʂ��J���Ă��邩.
    public bool _openRetireConfirmation = false;
    // �J�����̉�]�ʂ̕ێ�.
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
        // �J�����̌��̉�]����l��ێ�.
        _originalHorizontalAxisMaxSpeed = _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed;
        _originalVerticalAxisMaxSpeed = _cinemachinePOV.m_VerticalAxis.m_MaxSpeed;
    }

    void Update()
    {
        // �Q�[���̗�����~�߂��蓮�������肷��.
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

        // �ꎞ��~���������Ƃ��̏���.
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
                else if(_openRetireConfirmation)
                {
                    _openRetireConfirmation = false;
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
    // �|�[�Y��ʂ��J���Ă��邩�ǂ���.
    public bool GetOpenPause() { return _openPause; }

    // ���^�C�A�m�F��ʂ��J���Ă��邩�ǂ���.
    public bool GetOpenRetireConfirmation() { return _openRetireConfirmation; }
}
