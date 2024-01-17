/*選択UIの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUi : MonoBehaviour
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
    // 選択するUIが動くインターバル.
    private int _selectMoveInterval = 0;
    // 最初に選択した瞬間.
    private bool _firstSelect = false;
    // 押しっぱなしにした時の最初にUIが動くインターバル.

    // 押しっぱなしにしている時のUIが動くインターバル.
    private int _pushingInterval = 10;

    // 現在選ばれている選択番号.
    // 1.ゲームスタート.
    // 2.オプション.
    private int _selectNum = (int)SelectItem.GAMESTART;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _titleUpdate = GameObject.Find("GameManager").GetComponent<TitleUpdate>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        
    }

    void Update()
    {
        // PressAnyButtonが押されていないときはスキップ.
        if (!_titleUpdate._pressAnyPush) return;

        SelectUpdate();
        CrossKeyPushFlameCount();
        CrossKeyNoPush();
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

    // 選択している項目を変更.
    private void SelectUpdate()
    {
        // 十字キーを押していないときは処理を通さない.
        if (_controllerManager._UpDownCrossKey == 0) return;
        if(!_firstSelect)
        {
            if (_selectMoveInterval % 100 == 0)
            {
                // 上に押したとき
                if (_controllerManager._UpCrossKey)
                {
                    _selectNum--;
                }
                // 下に押したとき.
                else if (_controllerManager._DownCrossKey)
                {
                    _selectNum++;
                }
                if (_selectMoveInterval == 100)
                {
                    _firstSelect = true;
                }

            }
        }
        else
        {
            if (_selectMoveInterval % _pushingInterval == 0)
            {
                // 上に押したとき
                if (_controllerManager._UpCrossKey)
                {
                    _selectNum--;
                }
                // 下に押したとき.
                else if (_controllerManager._DownCrossKey)
                {
                    _selectNum++;
                }
            }
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

    // 十字キーを押している時間.
    private void CrossKeyPushFlameCount()
    {
        // 十字キーを押している間、時間を増やす.
        if(_controllerManager._UpDownCrossKey != 0)
        {
            _selectMoveInterval++;
        }
    }

    // 十字キーが押されていない.
    private void CrossKeyNoPush()
    {
        if(_controllerManager._UpDownCrossKey == 0)
        {
            _firstSelect = false;
            _selectMoveInterval = 0;
        }
    }
    
    public int GetSelectNumber() { return _selectNum; }
}
