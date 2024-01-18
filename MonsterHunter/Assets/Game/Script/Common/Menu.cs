/*使うであろうUIの関数*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // パッドの入力情報.
    private ControllerManager _controllerManager;

    // 選択するUIの連続して動くインターバル.
    private int _selectMoveInterval = 0;
    // 最初のインターバルが終わった瞬間.
    private bool _firstSelect = false;
    // 最初のインターバル.
    private int _firstInterval = 50;
    // 押し続ける際のインターバル.
    private int _pushingInterval = 5;

    private void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
    }

    // 十字キーを押している時間経過.
    public void CrossKeyPushFlameCount()
    {
        if(_controllerManager._UpDownCrossKey != 0)
        {
            _selectMoveInterval++;
        }
    }

    // 十字キーが押されていないときの処理.
    public void CrossKeyNoPush()
    {
        if(_controllerManager._UpDownCrossKey == 0)
        {
            _firstSelect = false;
            _selectMoveInterval = 0;
        }
    }

    /// <summary>
    /// 選択したUIの挙動.
    /// </summary>
    /// <param name="SelectNum">選択した項目の番号</param>
    public void SelectMove(ref int SelectNum)
    {
        // 十字キーを押していないときは処理を通さない.
        if (_controllerManager._UpDownCrossKey == 0) return;
        // 最初のインターバルが終わったら処理を変更.
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

    // 選択番号の変更.
    private void SelectNumberChange(int Interval, ref int SelectNum)
    {
        if(_selectMoveInterval % Interval == 0)
        {
            // 上に押したとき.
            if (_controllerManager._UpCrossKey)
            {
                SelectNum--;
            }
            // 下に押したとき.
            else if (_controllerManager._DownCrossKey)
            {
                SelectNum++;
            }
        }
    }
}
