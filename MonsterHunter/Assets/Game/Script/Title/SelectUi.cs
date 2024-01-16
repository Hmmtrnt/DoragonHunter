/*選択UIの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUi : MonoBehaviour
{
    private RectTransform _rectTransform;
    // タイトル画面の処理.
    private TitleUpdate _titleUpdate;
    // 入力情報.
    private ControllerManager _controllerManager;
    // 選択するUIが動くインターバル.
    private int _selectMoveInterval = 0;
    private float _selectMoveTime = 2;
    // 選択UIを押してすぐに動いたかどうか.
    private bool _selectMoveFirst = false;

    // 現在選ばれている選択番号.
    // 1.ゲームスタート.
    // 2.オプション.
    private int _selectNum = 1;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _titleUpdate = GameObject.Find("GameManager").GetComponent<TitleUpdate>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        
    }

    void Update()
    {
        SelectUpdate();
    }

    private void FixedUpdate()
    {
        

        if(_selectNum == 1)
        {
            _rectTransform.anchoredPosition = new Vector3(500.0f, -200.0f, 0.0f);
        }
        else if(_selectNum == 2)
        {
            _rectTransform.anchoredPosition = new Vector3(500.0f, -300.0f, 0.0f);
        }

        //Debug.Log(_selectMoveTime);
    }

    // pressAnyButtonを押したら選択できるようにする.
    private void SelectUpdate()
    {
        if (!_titleUpdate._pressAnyPush) return;

        if(_controllerManager._UpDownCrossKey != 0)
        {
            _selectMoveInterval++;
            //if (_selectNum == 1)
            //{
            //    _selectNum = 2;
            //}
            //else if (_selectNum == 2)
            //{
            //    _selectNum = 1;
            //}

            StartCoroutine(SelectMoveCoroutine(_selectMoveTime));
        }
        else if(_controllerManager._UpDownCrossKey == 0)
        {
            _selectMoveInterval = 0;
        }
    }

    // 選択UIの挙動
    private void SelectMove()
    {
        if(_selectMoveInterval % 120 == 0)
        {
            
        }
    }



    private IEnumerator SelectMoveCoroutine(float selectTime)
    {
        Debug.Log("通った");
        if (_selectMoveInterval % 120 == 0)
        {
            if (_selectNum == 1)
            {
                _selectNum = 2;
            }
            else if (_selectNum == 2)
            {
                _selectNum = 1;
            }
        }
        

        yield return new WaitForSecondsRealtime(selectTime);

        

        if(_selectMoveInterval % 20 == 0)
        {
            if (_selectNum == 1)
            {
                _selectNum = 2;
            }
            else if (_selectNum == 2)
            {
                _selectNum = 1;
            }
        }
    }
}
