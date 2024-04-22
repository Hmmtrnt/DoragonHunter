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
    private HuntingSceneManager _huntingSceneManager;
    // SE.
    private SEManager _seManager;
    // �t�F�[�h.
    private Fade _fade;

    // ���ݑI�΂�Ă���I��ԍ�.
    public int _selectNum = (int)SelectItem.NO;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _sceneTransitionManager = GameObject.Find("GameManager").GetComponent<SceneTransitionManager>();
        _huntingSceneManager = GameObject.Find("GameManager").GetComponent<HuntingSceneManager>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
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
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
            _huntingSceneManager._openRetireConfirmation = false;
        }
        else if(_selectNum == (int)SelectItem.YES && _controllerManager._AButtonDown) 
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
            _fade._isFading = false;
            
        }
        if(_fade._fadeEnd && _selectNum == (int)SelectItem.YES)
        {
            _sceneTransitionManager.SelectScene();
        }
    }
}
