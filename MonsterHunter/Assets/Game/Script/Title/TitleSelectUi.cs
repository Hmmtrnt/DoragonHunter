/*選択UIの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSelectUi : MonoBehaviour
{
    // 選択項目数.
    private enum SelectItem
    {
        GAMESTART,  // ゲームスタート.
        OPTION,     // 設定.
        MAXITEMNUM  // 項目の最大数.
    }

    // UIの座標.
    private RectTransform _rectTransform;
    // タイトル画面の処理.
    private TitleUpdate _titleUpdate;
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // 選択するUIの関数.
    private Menu _menu;
    // UI全体の制御.
    private TitleMenuManager _menuManager;
    // SEマネージャー.
    private SEManager _seManager;

    // 現在選ばれている選択番号.
    // 1.ゲームスタート.
    // 2.オプション.
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
        // PressAnyButtonが押されていないとき、設定画面を開いている時はスキップ.
        if (!_titleUpdate._pressAnyPush || _menuManager._openOption) return;

        _menu.SelectMove(_controllerManager._UpDownCrossKey, ref _selectNum);
        _menu.CrossKeyPushFlameCount(_controllerManager._UpDownCrossKey);
        _menu.CrossKeyNoPush(_controllerManager._UpDownCrossKey);
    }

    private void FixedUpdate()
    {
        // PressAnyButtonが押されていないとき、設定画面を開いている時はスキップ.
        if (!_titleUpdate._pressAnyPush || _menuManager._openOption) return;
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXITEMNUM);
        SelectPosition();
    }

    // 選択している項目の座標を代入.
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

    // 設定画面の開閉.
    private void OptionOpenORClose()
    {
        // 開くとき.
        if(_controllerManager._AButtonDown && _selectNum == (int)SelectItem.OPTION)
        {
            _menuManager._openOption = true;
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
        }
        // 閉じるとき.
        else if(_menuManager._openOption && _controllerManager._BButtonDown)
        {
            _menuManager._openOption = false;
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
        }
    }

    //選択している項目の番号.
    public int GetSelectNumber() { return _selectNum; }
}
