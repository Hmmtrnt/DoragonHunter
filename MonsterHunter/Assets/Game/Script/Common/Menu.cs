/*�g���ł��낤UI�̊֐�*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;

    // �I������UI�̘A�����ē����C���^�[�o��.
    private int _selectMoveInterval = 0;
    // �ŏ��̃C���^�[�o�����I������u��.
    private bool _firstSelect = false;
    // �ŏ��̃C���^�[�o��.
    private int _firstInterval = 50;
    // ����������ۂ̃C���^�[�o��.
    private int _pushingInterval = 5;
    // �T�E���h.
    private SEManager _seManager;

    private void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
    }

    // �\���L�[�������Ă��鎞�Ԍo��.
    public void CrossKeyPushFlameCount(float CrossKeyInputInformation)
    {
        if(CrossKeyInputInformation != 0)
        {
            _selectMoveInterval++;
        }
    }

    // �\���L�[��������Ă��Ȃ��Ƃ��̏���.
    public void CrossKeyNoPush(float CrossKeyInputInformation)
    {
        if(CrossKeyInputInformation == 0)
        {
            _firstSelect = false;
            _selectMoveInterval = 0;
        }
    }

    /// <summary>
    /// �I������UI�̋���.
    /// </summary>
    /// <param name="SelectNum">�I���������ڂ̔ԍ�</param>
    /// <param name="CrossKeyInputInformation ">�\���L�[�̓��͏��</param>
    public void SelectMove(float CrossKeyInputInformation, ref int SelectNum)
    {
        // �\���L�[�������Ă��Ȃ��Ƃ��͏�����ʂ��Ȃ�.
        if (CrossKeyInputInformation == 0) return;
        // �ŏ��̃C���^�[�o�����I������珈����ύX.
        if (!_firstSelect)
        {
            if (_selectMoveInterval == _firstInterval)
            {
                _firstSelect = true;
            }
            SelectNumberChange(_firstInterval, ref SelectNum);
        }
        else
        {
            SelectNumberChange(_pushingInterval, ref SelectNum);
        }
    }

    // �I��ԍ��̕ύX.
    private void SelectNumberChange(int Interval, ref int SelectNum)
    {
        if(_selectMoveInterval % Interval == 0)
        {
            // ��ɉ������Ƃ�.
            if (_controllerManager._UpCrossKey || _controllerManager._RightCrossKey)
            {
                SelectNum--;
                _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.SELECT);
            }
            // ���ɉ������Ƃ�.
            else if (_controllerManager._DownCrossKey || _controllerManager._LeftCrossKey)
            {
                SelectNum++;
                _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.SELECT);
            }
        }
    }

    /// <summary>
    /// �I��ԍ��̌��E�l.
    /// </summary>
    /// <param name="SelectNum">�I������Ă��鍀�ڂ̔ԍ�</param>
    public void SelectNumLimit(ref int SelectNum, int MaxItemNum)
    {
        if(SelectNum < 0)
        {
            SelectNum = MaxItemNum - 1;
        }
        else if(SelectNum >= MaxItemNum)
        {
            SelectNum = 0;
        }
    }
}
