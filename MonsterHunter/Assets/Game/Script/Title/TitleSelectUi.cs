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
    // UI�S�̂̐���.
    private TitleMenuManager _menuManager;
    // SE�}�l�[�W���[.
    private SEManager _seManager;

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
        _menuManager = GameObject.Find("TitleUI").GetComponent<TitleMenuManager>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
    }

    void Update()
    {
        OptionOpenORClose();
        // PressAnyButton��������Ă��Ȃ��Ƃ��A�ݒ��ʂ��J���Ă��鎞�̓X�L�b�v.
        if (!_titleUpdate._pressAnyPush || _menuManager._openOption) return;

        _menu.SelectMove(_controllerManager._UpDownCrossKey, ref _selectNum);
        _menu.CrossKeyPushFlameCount(_controllerManager._UpDownCrossKey);
        _menu.CrossKeyNoPush(_controllerManager._UpDownCrossKey);
    }

    private void FixedUpdate()
    {
        // PressAnyButton��������Ă��Ȃ��Ƃ��A�ݒ��ʂ��J���Ă��鎞�̓X�L�b�v.
        if (!_titleUpdate._pressAnyPush || _menuManager._openOption) return;
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

    // �ݒ��ʂ̊J��.
    private void OptionOpenORClose()
    {
        // �J���Ƃ�.
        if(_controllerManager._AButtonDown && _selectNum == (int)SelectItem.OPTION)
        {
            _menuManager._openOption = true;
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
        }
        // ����Ƃ�.
        else if(_menuManager._openOption && _controllerManager._BButtonDown)
        {
            _menuManager._openOption = false;
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
        }
    }

    //�I�����Ă��鍀�ڂ̔ԍ�.
    public int GetSelectNumber() { return _selectNum; }
}
