/*MainScene��Option��ʂ̑I��UI�̐���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainSceneOptionSelectUi : MonoBehaviour
{

    public enum SelectItem
    {
        MASTER, // �}�X�^�[����.
        BGM,    // BGM
        SE,     // SE
        MAXNUM
    }

    public enum SelectSlider
    {
        MASTER,
        BGM, 
        SE,
        MAXNUM
    }

    // UI�̍��W.
    private RectTransform _rectTransform;
    // �I������UI�̊֐�.
    private Menu _menu;
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;

    // ���ʒ��߂̃X���C�_�[.
    private float[] _volumeSlider = new float[3];
    // �X���C�_�[�̈ړ���.
    private float _sliderMovement = 0.001f;

    // ���ݑI�΂�Ă���I��ԍ�.
    public int _selectNum = (int)SelectItem.MASTER;

    // �\���L�[�̍��E�������Ă��鎞��.
    private int _pushCount;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _selectNum = (int)SelectItem.MASTER;
        for(int SliderNum = 0; SliderNum < _volumeSlider.Length; SliderNum++)
        {
            _volumeSlider[SliderNum] = 1.0f;
        }
    }

    void Update()
    {
        _menu.SelectMove(_controllerManager._UpDownCrossKey, ref _selectNum);
        _menu.CrossKeyPushFlameCount(_controllerManager._UpDownCrossKey);
        _menu.CrossKeyNoPush(_controllerManager._UpDownCrossKey);
        SliderMovementIncrease();
        CloseInit();
        SliderMove();
    }

    private void FixedUpdate()
    {
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXNUM);
        SelectPosition();
    }

    // ���E�̏\���L�[�������Ă��鎞��.
    private void CrossKeyCount()
    {
        if(_controllerManager._RightLeftCrossKey != 0)
        {
            _pushCount++;
        }
        else if(_controllerManager._RightLeftCrossKey == 0)
        {
            _pushCount = 0;
        }
    }

    // �����Ă���ԁA�X���C�_�[�̈ړ��ʂ𑝉�.
    private void SliderMovementIncrease()
    {
        CrossKeyCount();
        // �ړ��ʂ����Z�b�g.
        if (_controllerManager._RightLeftCrossKey == 0)
        {
            _sliderMovement = 0.001f;
        }
        // ���΂炭���������Ȃ��Ƒ����Ȃ�.
        if (_pushCount < 60) return;
        // �ړ��ʂ̑���.
        if (_controllerManager._RightLeftCrossKey != 0)
        {
            _sliderMovement += 0.0001f;
        }
        // �ő�ړ���.
        if(_sliderMovement > 0.01f)
        {
            _sliderMovement = 0.01f;
        }
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

    // �X���C�_�[�𓮂���.
    private void SliderMove()
    {
        // �E.
        if(_controllerManager._RightLeftCrossKey > 0)
        {

            if (_selectNum == (int)SelectItem.MASTER)
            {
                _volumeSlider[(int)SelectSlider.MASTER] += _sliderMovement;
            }
            else if(_selectNum == (int)SelectItem.BGM)
            {
                _volumeSlider[(int)SelectSlider.BGM] += _sliderMovement;
            }
            else if(_selectNum == (int)SelectItem.SE)
            {
                
                _volumeSlider[(int)SelectSlider.SE] += _sliderMovement;
            }
        }
        // ��.
        else if(_controllerManager._RightLeftCrossKey < 0)
        {
            if (_selectNum == (int)SelectItem.MASTER)
            {
                _volumeSlider[(int)SelectSlider.MASTER] -= _sliderMovement;
            }
            else if(_selectNum == (int)SelectItem.BGM)
            {
                _volumeSlider[(int)SelectSlider.BGM] -= _sliderMovement;
            }
            else if (_selectNum == (int)SelectItem.SE)
            {
                
                _volumeSlider[(int)SelectSlider.SE] -= _sliderMovement;
            }
        }
        
        for (int SelectSliderNum = 0; SelectSliderNum < (int)SelectSlider.MAXNUM; SelectSliderNum++)
        {
            // �l�̌��E�l.
            if (_volumeSlider[SelectSliderNum] > 1.0f)
            {
                _volumeSlider[SelectSliderNum] = 1.0f;
            }
            else if (_volumeSlider[SelectSliderNum] < 0.0f)
            {
                _volumeSlider[SelectSliderNum] = 0.0f;
            }
        }
    }

    public float GetVolumeSlider(int sliderNum) { return _volumeSlider[sliderNum]; }
}
