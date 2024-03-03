/*リザルト結果の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultUpdate : MonoBehaviour
{
    enum UIKinds
    {
        // 各表示の背景.
        TITLE, 
        RANK,
        GUIDE,
        CLEAR_TIME,
        RANK_EFFECT,
        MAX_NUM
    }

    // クリアタイムのUI番号.
    enum ClearTime
    {
        BACKGROUND, // 背景.
        STRING,     // 文字.
        MINUTE_TEN, // 十分.
        MINUTE_ONE, // 一分.
        COLON,      // :
        SECONDS_TEN,// 十秒.
        SECONDS_ONE,// 一秒.
        MAX_NUM      // 最大数.
    }

    // ランクの結果表示のUI番号.
    enum Rank
    {
        BACKGROUND, // 背景.
        STRING,     // 文字列.
        MAX_NUM     // 最大数.
    }

    private HuntingEnd _huntingEnd;
    private QuestEndUpdate _questEndUpdate;

    // リザルト画面のUI.
    public GameObject[] _ui;
    // クリアタイムのUI.
    public GameObject[] _clearTimeUI;
    private Image[] _clearTimeImage = new Image[(int)ClearTime.MAX_NUM];
    private RectTransform[] _clearTimeTransform = new RectTransform[(int)ClearTime.MAX_NUM];
    // クリアタイムの透明度.
    private byte[] _clearTimeColorA = new byte[(int)ClearTime.MAX_NUM];

    // ランクのUI.
    public GameObject[] _rankUI;
    private Image[] _rankImage = new Image[(int)Rank.MAX_NUM];
    private RectTransform[] _rankTransform = new RectTransform[(int)Rank.MAX_NUM];
    // ランクの透明度.
    private byte[] _rankColorA = new byte[(int)Rank.MAX_NUM];
    
    // パッドの入力情報.
    private ControllerManager _controllerManager;

    // アニメーションが終了.
    public bool _animEnd = false;

    // リザルト画面の経過時間.
    private int _flameCount = 0;

    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        _questEndUpdate = GameObject.Find("ResultUi").GetComponent<QuestEndUpdate>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        
        // クリアタイムUIの初期化.
        for(int ClearTimeUINum = 0;  ClearTimeUINum < (int)ClearTime.MAX_NUM; ClearTimeUINum++)
        {
            _clearTimeImage[ClearTimeUINum] = _clearTimeUI[ClearTimeUINum].GetComponent<Image>();
            _clearTimeTransform[ClearTimeUINum] = _clearTimeUI[ClearTimeUINum].GetComponent<RectTransform>();
            _clearTimeColorA[ClearTimeUINum] = 0;
        }
        // ランクUIの初期化.
        for(int RankUINum = 0;  RankUINum < (int)Rank.MAX_NUM; RankUINum++) 
        {
            _rankImage[RankUINum] = _rankUI[RankUINum].GetComponent<Image>();
            _rankTransform[RankUINum] = _rankUI[RankUINum].GetComponent<RectTransform>();
            _rankColorA[RankUINum] = 0;
        }
        
        _animEnd = false;
        
    }

    void Update()
    {
        AnimSkip();
    }

    private void FixedUpdate()
    {
        FlameCount();
        ClearTimeUIColor();
        UIAlpha();
    }

    // 経過時間を加算.
    private void FlameCount()
    {
        _flameCount++;
        if(_flameCount ==400)
        {
            _animEnd=true;
        }
    }

    // UIに透明度を代入.
    private void ClearTimeUIColor()
    {
        for(int ClearTimeUINum = 0;ClearTimeUINum < (int)ClearTime.MAX_NUM; ClearTimeUINum++)
        {
            _clearTimeImage[ClearTimeUINum].color = new Color32(255, 255, 255, _clearTimeColorA[ClearTimeUINum]);
        }
        for(int rankUINum = 0; rankUINum < (int)Rank.MAX_NUM; rankUINum++)
        {
            _rankImage[rankUINum].color = new Color32(255, 255, 255, _rankColorA[rankUINum]);
        }
    }

    // 各UIの透明度.
    private void UIAlpha()
    {
        // アニメーションが終了したらスキップ.
        if (_animEnd) return;

        if(_flameCount > 50)
        {
            ClearTimeAnim();
        }
        if(_flameCount > 60)
        {
            RankAnim();
        }
    }

    // クリアタイムのアニメーション.
    private void ClearTimeAnim()
    {
        if (_clearTimeColorA[(int)ClearTime.BACKGROUND] <= 200)
        {
            _clearTimeColorA[(int)ClearTime.BACKGROUND]+=2;
        }
        _clearTimeTransform[(int)ClearTime.BACKGROUND].DOAnchorPos(new Vector3(160.0f,70.0f, 0.0f), 1.0f).SetEase(Ease.OutQuint);

        if (_flameCount < 150) return;


        for(int ClearTimeUINum = (int)ClearTime.STRING; ClearTimeUINum < (int)ClearTime.MAX_NUM; ClearTimeUINum++)
        {
            if (_clearTimeColorA[ClearTimeUINum] != 255)
            {
                _clearTimeColorA[ClearTimeUINum] += 5;
            }
        }
    }

    // ランクのアニメーション.
    private void RankAnim()
    {
        if (_rankColorA[(int)Rank.BACKGROUND] <= 200)
        {
            _rankColorA[(int)Rank.BACKGROUND]+=2;
        }
        _rankTransform[(int)Rank.BACKGROUND].DOAnchorPos(new Vector3(-135.0f, -30.0f, 0.0f), 1.0f).SetEase(Ease.OutQuint);
        if (_flameCount < 200) return;


        for (int RankUINum = (int)Rank.STRING; RankUINum < (int)Rank.MAX_NUM; RankUINum++)
        {
            if (_rankColorA[RankUINum] != 255)
            {
                _rankColorA[RankUINum] += 5;
            }
        }

        if (_rankColorA[(int)Rank.STRING] != 255)
        {
            _rankColorA[(int)Rank.STRING] += 5;
        }
    }

    // リザルト画面のアニメーションスキップ.
    private void AnimSkip()
    {
        // アニメーションが終了すると処理をしない.
        if (_animEnd) return;

        if (_controllerManager._PressAnyButton)
        {
            ClearTimeFinalPositionAndFinalAlhpa();
            RankFinalPositionAndFinalAlpha();
            
            _animEnd = true;
        }
    }

    // クリアタイムの最終位置と透明度代入.
    private void ClearTimeFinalPositionAndFinalAlhpa()
    {
        // 背景だけ透明度が違う.
        _clearTimeColorA[(int)ClearTime.BACKGROUND] = 200;
        _clearTimeTransform[(int)ClearTime.BACKGROUND].anchoredPosition = new Vector3(160.0f, 70.0f, 0.0f);
        for (int ClearTimeUINum = (int)ClearTime.STRING; ClearTimeUINum < (int)ClearTime.MAX_NUM; ClearTimeUINum++)
        {
            _clearTimeColorA[ClearTimeUINum] = 255;
        }
    }

    // ランクの最終位置と透明度代入.
    private void RankFinalPositionAndFinalAlpha()
    {
        // 背景だけ透明度が違う.
        _rankColorA[(int)Rank.BACKGROUND] = 200;
        _rankTransform[(int)Rank.BACKGROUND].anchoredPosition = new Vector3(-135.0f, -30.0f, 0.0f);
        for (int ClearTimeUINum = (int)Rank.STRING; ClearTimeUINum < (int)Rank.MAX_NUM; ClearTimeUINum++)
        {
            _rankColorA[ClearTimeUINum] = 255;
        }
    }


    // アニメーションが終了下かの情報取得.
    public bool GetAnimEnd() { return _animEnd; }
}
