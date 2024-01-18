/*選択UIの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSelectUi : MonoBehaviour
{
    public enum SelectItem
    {
        GAMESTART,  // ゲームスタート.
        OPTION,     // 設定.
        MAXITEMNUM  // 項目の最大数.
    }

    private RectTransform _rectTransform;
    // タイトル画面の処理.
    private TitleUpdate _titleUpdate;
    // 入力情報.
    private ControllerManager _controllerManager;
    // 選択するUIの関数.
    private Menu _selectUiUpdate;

    // 現在選ばれている選択番号.
    // 1.ゲームスタート.
    // 2.オプション.
    private int _selectNum = (int)SelectItem.GAMESTART;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _titleUpdate = GameObject.Find("GameManager").GetComponent<TitleUpdate>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _selectUiUpdate = GameObject.Find("GameManager").GetComponent<Menu>();
    }

    void Update()
    {
        // PressAnyButtonが押されていないときはスキップ.
        if (!_titleUpdate._pressAnyPush) return;

        _selectUiUpdate.SelectMove(ref _selectNum);
        _selectUiUpdate.CrossKeyPushFlameCount();
        _selectUiUpdate.CrossKeyNoPush();
    }

    private void FixedUpdate()
    {
        // PressAnyButtonが押されていないときはスキップ.
        if (!_titleUpdate._pressAnyPush) return;
        SelectNumLimit();
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

    // 項目の限界値を越えた時の処理.
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

    public int GetSelectNumber() { return _selectNum; }
}
