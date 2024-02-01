/*リザルト画面の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ResultUpdate : MonoBehaviour
{
    // UIの種類.
    public enum UIKinds
    {
        CLEAR,      // クリア.
        FAILED,     // 失敗.
        // ベルト.
        BELTUP,     // 上.
        BELTDOWN,   // 下.
        MAXNUM      // 最大数.
    }

    // UIオブジェクト.
    public GameObject[] _ui;
    // 狩猟終了時の情報.
    private HuntingEnd _huntingEnd;
    // 各UIの座標.
    private RectTransform[] _rectTransform = new RectTransform[(int)UIKinds.MAXNUM];
    // UIを表示非表示にするかどうか.
    private bool[] _uiDisplayHide = new bool[(int)UIKinds.MAXNUM];
    // 終了してからの経過時間．
    private int _endCount = 0;

    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();

        for(int UINumber = 0; UINumber < (int)UIKinds.MAXNUM; UINumber++)
        {
            _uiDisplayHide[UINumber] = false;
            _rectTransform[UINumber] = _ui[UINumber].GetComponent<RectTransform>();
        }

        _rectTransform[(int)UIKinds.BELTUP].anchoredPosition = new Vector3(0, 200,0);
        _rectTransform[(int)UIKinds.BELTDOWN].anchoredPosition = new Vector3(0, -200,0);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        StartCount();
        UIDisplayHide();

        if (_huntingEnd.GetQuestClear())
        {
            Clear();
        }
        else if(_huntingEnd.GetQuestFailed())
        {
            Failed();
        }
    }

    // UIの表示非表示.
    private void UIDisplayHide()
    {
        _ui[(int)UIKinds.CLEAR].SetActive(_uiDisplayHide[(int)UIKinds.CLEAR]);
        _ui[(int)UIKinds.FAILED].SetActive(_uiDisplayHide[(int)UIKinds.FAILED]);
        _ui[(int)UIKinds.BELTUP].SetActive(_uiDisplayHide[(int)UIKinds.BELTUP]);
        _ui[(int)UIKinds.BELTDOWN].SetActive(_uiDisplayHide[(int)UIKinds.BELTDOWN]);
    }

    // クエストをクリアした時に呼び出す.
    private void Clear()
    {
        // クエストをクリアした時の表現処理.
        BeltDisplay();

        if(_endCount > 60)
        {
            BeltMove();
        }
        if(_endCount > 160)
        {
            _uiDisplayHide[(int)UIKinds.CLEAR] = true;
            StampResultAnim((int)UIKinds.CLEAR);
        }
        



    }

    // クエストを失敗した時に呼び出す.
    private void Failed()
    {
        // クエストを失敗した時の表現処理.
    }

    // クエスト終了してからカウント開始.
    private void StartCount()
    {
        if(_huntingEnd.GetQuestClear() ||  _huntingEnd.GetQuestFailed())
        {
            _endCount++;
        }
    }

    // 上下の枠を表示
    private void BeltDisplay()
    {
        _uiDisplayHide[(int)UIKinds.BELTUP] = true;
        _uiDisplayHide[(int)UIKinds.BELTDOWN] = true;
    }

    // 上下の黒帯の挙動.
    private void BeltMove()
    {
        _rectTransform[(int)UIKinds.BELTUP].DOAnchorPos(new Vector3(0.0f, 150.0f, 0.0f), 0.3f).SetEase(Ease.Linear);
        _rectTransform[(int)UIKinds.BELTDOWN].DOAnchorPos(new Vector3(0.0f, -150.0f, 0.0f), 0.3f).SetEase(Ease.Linear);
    }

    // スタンプロゴの結果表示のアニメーション.
    private void StampResultAnim(int LogNumber)
    {
        _rectTransform[LogNumber].DOScale(new Vector3(3.1f, 3.1f, 3.1f), 0.5f).SetEase(Ease.OutElastic);
    }
}
