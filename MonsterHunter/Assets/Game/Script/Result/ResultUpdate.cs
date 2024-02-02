/*リザルト結果の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUpdate : MonoBehaviour
{
    enum UIKinds
    {
        // 各表示の背景.
        TITLE, 
        RANK,
        RANK_TABLE,
        CLEAR_TIME,
        GUIDE

    }

    enum ClearTimeDigit
    {
        MINUTE_TEN, // 10分.
        MINUTE_ONE, // 1分.
        SECOND_TEN, // 10秒.
        SECOND_ONE, // 1秒.
    }


    enum SpriteNumber
    {
        // 数のスプライト.
        ZERO,
        ONE,
        TWO, 
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,

        MAXNUM
        
    }


    // クエスト終了時の情報.
    private HuntingEnd _huntingEnd;
    // リザルト画面のUI.
    public GameObject[] _ui;
    // クリアタイムのUI.
    public GameObject[] _clearTime;
    // クエストタイムの時間のスプライト.
    public Sprite[] _timeNum;

    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    // タイムによって計測時間のスプライトを変更.
    private void SpriteTimeChange()
    {
        
    }
}
