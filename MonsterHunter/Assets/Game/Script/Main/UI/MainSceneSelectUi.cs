/*メインシーンの選択UIの処理*/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneSelectUi : MonoBehaviour
{
    private enum SelectItem
    {
        OPTION,// 設定.
        PAUSE,// 一時停止.
        RETIREMENT,// リタイア.
        MAXNUM// 項目数.
    }

    // UIの座標.
    private RectTransform _rectTransform;
    // 選択するUIの関数.
    private Menu _menu;
    // プレイヤー情報.
    private PlayerState _playerState;
    // パッドの入力情報.
    private ControllerManager _controllerManager;

    // 現在選ばれている選択番号.
    private int _selectNum = (int)SelectItem.OPTION;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
    }

    void Update()
    {
        // メニュー画面を開いた時に処理.
        if (!_playerState.GetOpenMenu()) return;
        _menu.SelectMove(ref _selectNum);
        _menu.CrossKeyPushFlameCount();
        _menu.CrossKeyNoPush();
        CloseInit();
    }

    private void FixedUpdate()
    {
        // メニュー画面を開いた時に処理.
        if(!_playerState.GetOpenMenu()) return;
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXNUM);
        SelectPosition();
    }

    // 選択されている項目の座標を代入.
    private void SelectPosition()
    {
        if(_selectNum == (int)SelectItem.OPTION)
        {
            _rectTransform.anchoredPosition = new Vector3(100.0f, -20.0f, 0.0f);
        }
        else if(_selectNum == (int)SelectItem.PAUSE)
        {
            _rectTransform.anchoredPosition = new Vector3(100.0f, -60.0f, 0.0f);
        }
        else if(_selectNum == (int)SelectItem.RETIREMENT)
        {
            _rectTransform.anchoredPosition = new Vector3(100.0f, -100.0f, 0.0f);
        }
    }

    // 閉じるときに初期化.
    private void CloseInit()
    {
        if(_controllerManager._BButtonDown)
        {
            _selectNum = (int)SelectItem.OPTION;
        }
    }


    // 選択している項目の番号.
    public int GetSelectNumber() { return _selectNum; }
}
