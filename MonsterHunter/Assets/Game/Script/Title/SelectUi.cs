/*選択UIの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUi : MonoBehaviour
{
    private RectTransform _rectTransform;
    // タイトル画面の処理.
    private TitleUpdate _titleUpdate;
    // 現在選ばれている選択番号.
    // 1.ゲームスタート.
    // 2.オプション.
    private int _selectNum = 1;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _titleUpdate = GameObject.Find("GameManager").GetComponent<TitleUpdate>();
        Debug.Log(_rectTransform.anchoredPosition);
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(_selectNum == 1)
        {
            _rectTransform.anchoredPosition = new Vector3(0.0f, -230.0f, 0.0f);
        }
        else if(_selectNum == 2)
        {
            _rectTransform.anchoredPosition = new Vector3(0.0f, -330.0f, 0.0f);
        }
    }

    // pressAnyButtonを押したら選択できるようにする.
    private void SelectUpdate()
    {
        if (!_titleUpdate._pressAnyPush) return;
    }
}
