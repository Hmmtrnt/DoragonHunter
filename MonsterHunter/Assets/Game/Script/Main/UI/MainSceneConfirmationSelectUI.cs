/*�m�F��ʂ̑I��UI�̐���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneConfirmationSelectUI : MonoBehaviour
{
    public enum SelectItem
    {
        NO,     // ������.
        YES,    // �͂�.
        MAXNUM  // ���ڐ�.
    }

    // UI�̍��W.
    private RectTransform _rectTransform;
    // �I������UI�̊֐�.
    private Menu _menu;
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // �V�[���J�ڂ̏��.
    private SceneTransitionManager _sceneTransitionManager;
    // ���C���V�[���̏��.
    private MainSceneManager _mainSceneManager;

    // ���ݑI�΂�Ă���I��ԍ�.
    public int _selectNum = (int)SelectItem.NO;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _sceneTransitionManager = GameObject.Find("GameManager").GetComponent<SceneTransitionManager>();
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
        _selectNum = (int)SelectItem.NO;
    }

    private void OnEnable()
    {
        _selectNum = (int)SelectItem.NO;
    }

    void Update()
    {
        _menu.SelectMove(_controllerManager._RightLeftCrossKey , ref _selectNum);
        _menu.CrossKeyPushFlameCount(_controllerManager._RightLeftCrossKey);
        _menu.CrossKeyNoPush(_controllerManager._RightLeftCrossKey);
        SelectDecision();
    }

    private void FixedUpdate()
    {
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXNUM);
        SelectPosition();
    }

    // �I������Ă��鍀�ڂ̍��W�����.
    private void SelectPosition()
    {
        if (_selectNum == (int)SelectItem.NO)
        {
            _rectTransform.anchoredPosition = new Vector3(-75.0f, -20.0f, 0.0f);
        }
        else if (_selectNum == (int)SelectItem.YES)
        {
            _rectTransform.anchoredPosition = new Vector3(75.0f, -20.0f, 0.0f);
        }
    }

    // �I������Ă��鍀�ڂ̌���.
    private void SelectDecision()
    {
        if(_selectNum == (int)SelectItem.NO && _controllerManager._AButtonDown)
        {
            _mainSceneManager._openRetireConfirmation = false;
        }
        else if(_selectNum == (int)SelectItem.YES && _controllerManager._AButtonDown) 
        {
            _sceneTransitionManager.SelectScene();
        }
    }
}