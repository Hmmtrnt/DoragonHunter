/*�I����ʂ̑I��UI*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneSelectUi : MonoBehaviour
{
    // �I�����ڐ�.
    public enum SelectItem
    {
        EASY,       // �C�[�W�[���[�h.
        HATD,       // �n�[�h���[�h.
        TUTORIAL,   // �`���[�g���A�����[�h.
        MAXITEMNUM  // ���ڂ̍ő吔.
    }

    // UI�̍��W.
    private RectTransform _rectTransform;
    // �p�b�h�̓��A���.
    private ControllerManager _controllerManager;
    // �I������UI�̊֐�.
    private Menu _menu;

    // ���ݑI�΂�Ă���I��ԍ�.
    private int _selectNum = (int)SelectItem.EASY;


    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
    }

    void Update()
    {
        _menu.SelectMove(_controllerManager._UpDownCrossKey, ref _selectNum);
        _menu.CrossKeyPushFlameCount(_controllerManager._UpDownCrossKey);
        _menu.CrossKeyNoPush(_controllerManager._UpDownCrossKey);
    }

    private void FixedUpdate()
    {
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXITEMNUM);
        SelectPosition();

    }

    // �I�����Ă��鍀�ڂ̍��W����.
    private void SelectPosition()
    {
        if(_selectNum == (int)SelectItem.EASY)
        {
            _rectTransform.anchoredPosition = new Vector3(-500, 250, 0);
        }
        else if(_selectNum == (int)SelectItem.HATD)
        {
            _rectTransform.anchoredPosition = new Vector3(-500, 0, 0);
        }
        else if(_selectNum == (int)SelectItem.TUTORIAL) 
        {
            _rectTransform.anchoredPosition = new Vector3(-500, -250, 0);
        }
    }

     //�I�����Ă��鍀�ڂ̔ԍ�.
    public int GetSelectNumber() { return _selectNum; }
}
