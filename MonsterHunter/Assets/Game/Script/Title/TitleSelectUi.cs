/*�I��UI�̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSelectUi : MonoBehaviour
{
    // �I�����ڐ�.
    private enum SelectItem
    {
        GAMESTART,  // �Q�[���X�^�[�g.
        OPTION,     // �ݒ�.
        MAXITEMNUM  // ���ڂ̍ő吔.
    }

    // UI�̍��W.
    private RectTransform _rectTransform;
    // �^�C�g����ʂ̏���.
    private TitleUpdate _titleUpdate;
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // �I������UI�̊֐�.
    private Menu _menu;

    // ���ݑI�΂�Ă���I��ԍ�.
    // 1.�Q�[���X�^�[�g.
    // 2.�I�v�V����.
    private int _selectNum = (int)SelectItem.GAMESTART;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _titleUpdate = GameObject.Find("GameManager").GetComponent<TitleUpdate>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
    }

    void Update()
    {
        // PressAnyButton��������Ă��Ȃ��Ƃ��̓X�L�b�v.
        if (!_titleUpdate._pressAnyPush) return;

        _menu.SelectMove(ref _selectNum);
        _menu.CrossKeyPushFlameCount();
        _menu.CrossKeyNoPush();
    }

    private void FixedUpdate()
    {
        // PressAnyButton��������Ă��Ȃ��Ƃ��̓X�L�b�v.
        if (!_titleUpdate._pressAnyPush) return;
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXITEMNUM);
        SelectPosition();
    }

    // �I�����Ă��鍀�ڂ̍��W����.
    private void SelectPosition()
    {
        if (_selectNum == (int)SelectItem.GAMESTART)
        {
            _rectTransform.anchoredPosition = new Vector3(500.0f, -200.0f, 0.0f);
        }
        else if (_selectNum == (int)SelectItem.OPTION)
        {
            _rectTransform.anchoredPosition = new Vector3(500.0f, -300.0f, 0.0f);
        }
    }

    //�I�����Ă��鍀�ڂ̔ԍ�.
    public int GetSelectNumber() { return _selectNum; }
}