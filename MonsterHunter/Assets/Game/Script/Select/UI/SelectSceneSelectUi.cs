/*選択画面の選択UI*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneSelectUi : MonoBehaviour
{
    // 選択項目数.
    private enum SelectItem
    {
        EASY,       // イージーモード.
        HATD,       // ハードモード.
        MAXITEMNUM  // 項目の最大数.
    }

    // UIの座標.
    private RectTransform _rectTransform;
    // パッドの入植情報.
    private ControllerManager _controllerManager;
    // 選択するUIの関数.
    private Menu _menu;

    // 現在選ばれている選択番号.
    // 1.EASY.
    // 2.HARD.
    private int _selectNum = (int)SelectItem.EASY;


    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
    }

    void Update()
    {
        _menu.SelectMove(ref _selectNum);
        _menu.CrossKeyPushFlameCount();
        _menu.CrossKeyNoPush();
    }

    private void FixedUpdate()
    {
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXITEMNUM);
        SelectPosition();

    }

    // 選択している項目の座標を代入.
    private void SelectPosition()
    {
        if(_selectNum == (int)SelectItem.EASY)
        {
            _rectTransform.anchoredPosition = new Vector3(-500, 250, 0);
        }
        else if(_selectNum == (int)SelectItem.HATD)
        {
            _rectTransform.anchoredPosition = new Vector3(-500, 0, 0);
        }
    }

     //選択している項目の番号.
    public int GetSelectNumber() { return _selectNum; }
}
