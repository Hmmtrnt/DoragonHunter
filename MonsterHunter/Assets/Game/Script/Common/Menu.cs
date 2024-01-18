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

    private void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
    }

    // �\���L�[�������Ă��鎞�Ԍo��.
    public void CrossKeyPushFlameCount()
    {
        if(_controllerManager._UpDownCrossKey != 0)
        {
            _selectMoveInterval++;
        }
    }

    // �\���L�[��������Ă��Ȃ��Ƃ��̏���.
    public void CrossKeyNoPush()
    {
        if(_controllerManager._UpDownCrossKey == 0)
        {
            _firstSelect = false;
            _selectMoveInterval = 0;
        }
    }

    /// <summary>
    /// �I������UI�̋���.
    /// </summary>
    /// <param name="SelectNum">�I���������ڂ̔ԍ�</param>
    public void SelectMove(ref int SelectNum)
    {
        // �\���L�[�������Ă��Ȃ��Ƃ��͏�����ʂ��Ȃ�.
        if (_controllerManager._UpDownCrossKey == 0) return;
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
            if (_controllerManager._UpCrossKey)
            {
                SelectNum--;
            }
            // ���ɉ������Ƃ�.
            else if (_controllerManager._DownCrossKey)
            {
                SelectNum++;
            }
        }
    }
}
