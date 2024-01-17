/*�I��UI�̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUi : MonoBehaviour
{
    public enum SelectItem
    {
        GAMESTART,  // �Q�[���X�^�[�g.
        OPTION,     // �ݒ�.
        MAXITEMNUM  // ���ڂ̍ő吔.
    }

    private RectTransform _rectTransform;
    // �^�C�g����ʂ̏���.
    private TitleUpdate _titleUpdate;
    // ���͏��.
    private ControllerManager _controllerManager;
    // �I������UI�������C���^�[�o��.
    private int _selectMoveInterval = 0;
    // �ŏ��ɑI�������u��.
    private bool _firstSelect = false;
    // �������ςȂ��ɂ������̍ŏ���UI�������C���^�[�o��.

    // �������ςȂ��ɂ��Ă��鎞��UI�������C���^�[�o��.
    private int _pushingInterval = 10;

    // ���ݑI�΂�Ă���I��ԍ�.
    // 1.�Q�[���X�^�[�g.
    // 2.�I�v�V����.
    private int _selectNum = (int)SelectItem.GAMESTART;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _titleUpdate = GameObject.Find("GameManager").GetComponent<TitleUpdate>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        
    }

    void Update()
    {
        // PressAnyButton��������Ă��Ȃ��Ƃ��̓X�L�b�v.
        if (!_titleUpdate._pressAnyPush) return;

        SelectUpdate();
        CrossKeyPushFlameCount();
        CrossKeyNoPush();
    }

    private void FixedUpdate()
    {
        // PressAnyButton��������Ă��Ȃ��Ƃ��̓X�L�b�v.
        if (!_titleUpdate._pressAnyPush) return;
        SelectNumLimit();
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

    // �I�����Ă��鍀�ڂ�ύX.
    private void SelectUpdate()
    {
        // �\���L�[�������Ă��Ȃ��Ƃ��͏�����ʂ��Ȃ�.
        if (_controllerManager._UpDownCrossKey == 0) return;
        if(!_firstSelect)
        {
            if (_selectMoveInterval % 100 == 0)
            {
                // ��ɉ������Ƃ�
                if (_controllerManager._UpCrossKey)
                {
                    _selectNum--;
                }
                // ���ɉ������Ƃ�.
                else if (_controllerManager._DownCrossKey)
                {
                    _selectNum++;
                }
                if (_selectMoveInterval == 100)
                {
                    _firstSelect = true;
                }

            }
        }
        else
        {
            if (_selectMoveInterval % _pushingInterval == 0)
            {
                // ��ɉ������Ƃ�
                if (_controllerManager._UpCrossKey)
                {
                    _selectNum--;
                }
                // ���ɉ������Ƃ�.
                else if (_controllerManager._DownCrossKey)
                {
                    _selectNum++;
                }
            }
        }
    }

    // ���ڂ̌��E�l���z�������̏���.
    private void SelectNumLimit()
    {
        if(_selectNum < 0)
        {
            _selectNum = (int)SelectItem.OPTION;
        }
        else if(_selectNum >= (int)SelectItem.MAXITEMNUM)
        {
            _selectNum = 0;
        }
    }

    // �\���L�[�������Ă��鎞��.
    private void CrossKeyPushFlameCount()
    {
        // �\���L�[�������Ă���ԁA���Ԃ𑝂₷.
        if(_controllerManager._UpDownCrossKey != 0)
        {
            _selectMoveInterval++;
        }
    }

    // �\���L�[��������Ă��Ȃ�.
    private void CrossKeyNoPush()
    {
        if(_controllerManager._UpDownCrossKey == 0)
        {
            _firstSelect = false;
            _selectMoveInterval = 0;
        }
    }
    
    public int GetSelectNumber() { return _selectNum; }
}
