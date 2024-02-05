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
        MAX_DIGIT_NUM// 最大桁数.
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
    // クリアタイムの座標取得
    private RectTransform[] _clearTimeTransform = new RectTransform[(int)ClearTimeDigit.MAX_DIGIT_NUM];
    // クリアタイムのスプライト.
    private Image[] _clearTimeSprite = new Image[(int)ClearTimeDigit.MAX_DIGIT_NUM];

    // クエストタイムの時間のスプライト.
    public Sprite[] _timeNum;

    // クリア時間を保存.
    private int[] _questTime = new int[(int)ClearTimeDigit.MAX_DIGIT_NUM];


    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        for(int ClearTimeSpriteNum = 0;  ClearTimeSpriteNum < (int)ClearTimeDigit.MAX_DIGIT_NUM; ClearTimeSpriteNum++)
        {
            _clearTimeTransform[ClearTimeSpriteNum] = _clearTime[ClearTimeSpriteNum].GetComponent<RectTransform>();
            _clearTimeSprite[ClearTimeSpriteNum] = _clearTime[ClearTimeSpriteNum].GetComponent<Image>();
        }

        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ClearTimeSubstitute();
        SpriteTimeChange();
    }

    // クリアタイムを各桁に代入.
    private void ClearTimeSubstitute()
    {
        if(_huntingEnd.GetQuestEnd())
        {
            // 分の十の位取得.
            _questTime[(int)ClearTimeDigit.MINUTE_TEN] = _huntingEnd._Minute / 10;
            // 分の一の位取得.
            _questTime[(int)ClearTimeDigit.MINUTE_ONE] = _huntingEnd._Minute % 10;
            // 秒の十の位取得.
            _questTime[(int)ClearTimeDigit.SECOND_TEN] = _huntingEnd._Second / 10;
            // 秒の一の位取得.
            _questTime[(int)ClearTimeDigit.SECOND_ONE] = _huntingEnd._Second % 10;
        }
    }

    // タイムによって計測時間のスプライトを変更.
    private void SpriteTimeChange()
    {
        for (int TimeDigit = 0; TimeDigit < (int)ClearTimeDigit.MAX_DIGIT_NUM; TimeDigit++)
        {
            //_clearTimeSprite[TimeDigit].sprite = _timeNum[TimeDigit];
            ClearTimeSprite(TimeDigit);
            TimeOneSizeChange(TimeDigit);
        }
    }

    // クリアタイムのスプライト変更.
    private void ClearTimeSprite(int TimeDigit)
    {
        _clearTimeSprite[TimeDigit].sprite = _timeNum[_questTime[TimeDigit]];
        //Debug.Log(_questTime[TimeDigit]);
    }

    // クリアタイムが1を表示するとき違和感があるので大きさを変更.
    private void TimeOneSizeChange(int TimeDigit)
    {
        if (_clearTimeSprite[TimeDigit].sprite == _timeNum[(int)SpriteNumber.ONE])
        {
            _clearTimeTransform[TimeDigit].sizeDelta = new Vector2(20, 40);
        }
        else if(_clearTimeSprite[TimeDigit].sprite != _timeNum[(int)SpriteNumber.ONE])
        {
            _clearTimeTransform[TimeDigit].sizeDelta = new Vector2(30, 40);
        }
    }


}
