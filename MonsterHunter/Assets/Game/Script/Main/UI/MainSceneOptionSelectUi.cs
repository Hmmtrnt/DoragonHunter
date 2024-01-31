/*MainSceneのOption画面の選択UIの制御*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainSceneOptionSelectUi : MonoBehaviour
{

    public enum SelectItem
    {
        MASTER, // マスター音量.
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

    // UIの座標.
    private RectTransform _rectTransform;
    // 選択するUIの関数.
    private Menu _menu;
    // パッドの入力情報.
    private ControllerManager _controllerManager;

    // 音量調節のスライダー.
    private float[] _volumeSlider = new float[3];
    // スライダーの移動量.
    private float _sliderMovement = 0.001f;

    // 現在選ばれている選択番号.
    public int _selectNum = (int)SelectItem.MASTER;

    // 十字キーの左右を押している時間.
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

    // 左右の十字キーを押している時間.
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

    // 押している間、スライダーの移動量を増加.
    private void SliderMovementIncrease()
    {
        CrossKeyCount();
        // 移動量をリセット.
        if (_controllerManager._RightLeftCrossKey == 0)
        {
            _sliderMovement = 0.001f;
        }
        // しばらく押し続けないと増えない.
        if (_pushCount < 60) return;
        // 移動量の増加.
        if (_controllerManager._RightLeftCrossKey != 0)
        {
            _sliderMovement += 0.0001f;
        }
        // 最大移動量.
        if(_sliderMovement > 0.01f)
        {
            _sliderMovement = 0.01f;
        }
    }

    // 選択されている項目の座標を代入.
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

    // 閉じるときに初期化.
    private void CloseInit()
    {
        if (_controllerManager._BButtonDown)
        {
            _selectNum = (int)SelectItem.MASTER;
        }
    }

    // スライダーを動かす.
    private void SliderMove()
    {
        // 右.
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
        // 左.
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
            // 値の限界値.
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
