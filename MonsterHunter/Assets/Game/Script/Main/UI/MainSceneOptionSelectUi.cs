/*MainSceneのOption画面の選択UIの制御*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneOptionSelectUi : MonoBehaviour
{

    public enum SelectItem
    {
        MASTER, // マスター音量.
        BGM,    // BGM
        SE,     // SE
        MAXNUM
    }

    // UIの座標.
    private RectTransform _rectTransform;
    // 選択するUIの関数.
    private Menu _menu;
    // パッドの入力情報.
    private ControllerManager _controllerManager;

    // 現在選ばれている選択番号.
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
}
