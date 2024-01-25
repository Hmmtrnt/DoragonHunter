/*���C���V�[���̑I��UI�̏���*/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneSelectUi : MonoBehaviour
{
    private enum SelectItem
    {
        OPTION,// �ݒ�.
        PAUSE,// �ꎞ��~.
        RETIREMENT,// ���^�C�A.
        MAXNUM// ���ڐ�.
    }

    // UI�̍��W.
    private RectTransform _rectTransform;
    // �I������UI�̊֐�.
    private Menu _menu;
    // �v���C���[���.
    private PlayerState _playerState;
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;

    // ���ݑI�΂�Ă���I��ԍ�.
    private int _selectNum = (int)SelectItem.OPTION;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
    }

    void Update()
    {
        // ���j���[��ʂ��J�������ɏ���.
        if (!_playerState.GetOpenMenu()) return;
        _menu.SelectMove(ref _selectNum);
        _menu.CrossKeyPushFlameCount();
        _menu.CrossKeyNoPush();
        CloseInit();
    }

    private void FixedUpdate()
    {
        // ���j���[��ʂ��J�������ɏ���.
        if(!_playerState.GetOpenMenu()) return;
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXNUM);
        SelectPosition();
    }

    // �I������Ă��鍀�ڂ̍��W����.
    private void SelectPosition()
    {
        if(_selectNum == (int)SelectItem.OPTION)
        {
            _rectTransform.anchoredPosition = new Vector3(100.0f, -20.0f, 0.0f);
        }
        else if(_selectNum == (int)SelectItem.PAUSE)
        {
            _rectTransform.anchoredPosition = new Vector3(100.0f, -60.0f, 0.0f);
        }
        else if(_selectNum == (int)SelectItem.RETIREMENT)
        {
            _rectTransform.anchoredPosition = new Vector3(100.0f, -100.0f, 0.0f);
        }
    }

    // ����Ƃ��ɏ�����.
    private void CloseInit()
    {
        if(_controllerManager._BButtonDown)
        {
            _selectNum = (int)SelectItem.OPTION;
        }
    }


    // �I�����Ă��鍀�ڂ̔ԍ�.
    public int GetSelectNumber() { return _selectNum; }
}
