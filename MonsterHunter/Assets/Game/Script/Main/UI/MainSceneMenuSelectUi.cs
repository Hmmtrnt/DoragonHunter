/*メインシーンの選択UIの処理*/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneMenuSelectUi : MonoBehaviour
{
    public enum SelectItem
    {
        OPTION,     // 設定.
        PAUSE,      // 一時停止.
        RETIRE,     // リタイア.
        MAXNUM      // 項目数.
    }

    // UIの座標.
    private RectTransform _rectTransform;
    // 選択するUIの関数.
    private Menu _menu;
    // プレイヤー情報.
    private PlayerState _playerState;
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // メインシーンの情報.
    private MainSceneManager _mainSceneManager;


    // 現在選ばれている選択番号.
    public int _selectNum = (int)SelectItem.OPTION;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
        _selectNum = (int)SelectItem.OPTION;
    }

    private void OnEnable()
    {
        _selectNum = (int)SelectItem.OPTION;
    }

    void Update()
    {
        // メニュー画面を閉じているまた、オプション画面を開いている時にスキップ処理.
        if (!_mainSceneManager.GetOpenMenu() || _mainSceneManager.GetOpenOption() ||
            _mainSceneManager.GetOpenRetireConfirmation()) return;
        _menu.SelectMove(_controllerManager._UpDownCrossKey, ref _selectNum);
        _menu.CrossKeyPushFlameCount(_controllerManager._UpDownCrossKey);
        _menu.CrossKeyNoPush(_controllerManager._UpDownCrossKey);
        OpenOption();
        OpenRetire();
    }

    private void FixedUpdate()
    {
        // メニュー画面を閉じているまた、オプション画面を開いている時にスキップ処理.
        if (!_mainSceneManager.GetOpenMenu() || _mainSceneManager.GetOpenOption() ||
            _mainSceneManager.GetOpenRetireConfirmation()) return;
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
        else if(_selectNum == (int)SelectItem.RETIRE)
        {
            _rectTransform.anchoredPosition = new Vector3(100.0f, -100.0f, 0.0f);
        }
    }

    // 設定画面を開く.
    private void OpenOption()
    {
        // 設定画面を選んで決定.
        if(_controllerManager._AButtonDown && _selectNum == (int)SelectItem.OPTION)
        {
            _mainSceneManager._openOption = true;
        }
    }

    // リタイア確認画面を開く.
    private void OpenRetire()
    {
        if(_controllerManager._AButtonDown && _selectNum ==(int)SelectItem.RETIRE)
        {
            _mainSceneManager._openRetireConfirmation = true;
        }
    }


    // 選択している項目の番号.
    public int GetSelectNumber() { return _selectNum; }
}
