/*MainScene��Option��ʂ̑I��UI�̐���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneOptionSelectUi : MonoBehaviour
{

    public enum SelectItem
    {
        MASTER, // �}�X�^�[����.
        BGM,    // BGM
        SE,     // SE
        MAXNUM
    }

    // UI�̍��W.
    private RectTransform _rectTransform;
    // �I������UI�̊֐�.
    private Menu _menu;
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;

    // ���ݑI�΂�Ă���I��ԍ�.
    public int _selectNum = (int)SelectItem.MASTER;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _selectNum = (int)SelectItem.MASTER;
    }

    void Update()
    {
        _menu.SelectMove(ref _selectNum);
        _menu.CrossKeyPushFlameCount();
        _menu.CrossKeyNoPush();
        CloseInit();
    }

    private void FixedUpdate()
    {
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXNUM);
        SelectPosition();
    }

    // �I������Ă��鍀�ڂ̍��W����.
    private void SelectPosition()
    {
        if(_selectNum == (int)SelectItem.MASTER)
        {
            _rectTransform.anchoredPosition = new Vector3(0.0f,40.0f,0.0f);
        }
        else if(_selectNum == (int)SelectItem.BGM)
        {
            _rectTransform.anchoredPosition = new Vector3(0.0f,0.0f,0.0f);
        }
        else if(_selectNum== (int)SelectItem.SE)
        {
            _rectTransform.anchoredPosition = new Vector3(0.0f, -40.0f, 0.0f);
        }
    }

    // ����Ƃ��ɏ�����.
    private void CloseInit()
    {
        if (_controllerManager._BButtonDown)
        {
            _selectNum = (int)SelectItem.MASTER;
        }
    }
}
