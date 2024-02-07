/*クエスト終了画面からリザルト表示までの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class QuestEndUpdate : MonoBehaviour
{
    // UIの種類.
    public enum UIKinds
    {
        CLEAR,              // クリア.
        FAILED,             // 失敗.
        // ベルト.
        // クリア時.
        CLEAR_BELT_UP,      // 上.
        CLEAR_BELT_DOWN,    // 下.
        FAILED_BELT_UP,     // 上.
        FAILED_BELT_DOWN,   // 下.
        RESULT_BACKGROUND,  // 結果を表示するときの背景.
        MAXNUM              // 最大数.
    }

    // UIオブジェクト.
    public GameObject[] _ui;
    // 狩猟終了時の情報.
    private HuntingEnd _huntingEnd;
    // 各UIの座標.
    private RectTransform[] _rectTransform = new RectTransform[(int)UIKinds.MAXNUM];
    // 各UIの色.
    private Image _image;
    // SE.
    private SEManager _seManager;

    // UIを表示非表示にするかどうか.
    private bool[] _uiDisplay = new bool[(int)UIKinds.MAXNUM];
    // 終了してからの経過時間．
    private int _endCount = 0;

    // 上下のベルトのアニメーション開始時間.
    private const int _beltMoveStart = 60;
    // スタンプロゴの表示時間.
    private const int _stampLogDisPlayTime = 160;

    // リザルト画面の背景のα値.
    private byte _resultColorA = 0;
    // リザルト画面の背景の限界α値.
    private const byte _resultColorMaxA = 50;

    // SEを一度しか鳴らさない.
    private bool _playSEFlag = true;

    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();

        for(int UINumber = 0; UINumber < (int)UIKinds.MAXNUM; UINumber++)
        {
            _uiDisplay[UINumber] = false;
            _rectTransform[UINumber] = _ui[UINumber].GetComponent<RectTransform>();
        }

        _image = _ui[(int)UIKinds.RESULT_BACKGROUND].GetComponent<Image>();

        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();

        // 上下の枠の初期位置.
        _rectTransform[(int)UIKinds.CLEAR_BELT_UP].anchoredPosition = new Vector3(0, 200,0);
        _rectTransform[(int)UIKinds.CLEAR_BELT_DOWN].anchoredPosition = new Vector3(0, -200,0);
        _rectTransform[(int)UIKinds.FAILED_BELT_UP].anchoredPosition = new Vector3(0, 200, 0);
        _rectTransform[(int)UIKinds.FAILED_BELT_DOWN].anchoredPosition = new Vector3(0, -200, 0);
        _resultColorA = 0;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        StartCount();
        UIDisplayHide();

        // 勝敗によってUIを変更.
        if (_huntingEnd.GetQuestClear())
        {
            UiAnim((int)UIKinds.CLEAR_BELT_UP, (int)UIKinds.CLEAR_BELT_DOWN, (int)UIKinds.CLEAR);
        }
        else if(_huntingEnd.GetQuestFailed())
        {
            UiAnim((int)UIKinds.FAILED_BELT_UP, (int)UIKinds.FAILED_BELT_DOWN, (int)UIKinds.FAILED);
        }
    }

    // UIの表示非表示.
    private void UIDisplayHide()
    {
        for(int kinds = 0; kinds < (int)UIKinds.MAXNUM; kinds++)
        {
            _ui[kinds].SetActive(_uiDisplay[kinds]);
        }
    }

    // クエストを終了した時にさせるアニメーション.
    private void UiAnim(int BeltUp, int BeltDown, int StampNunber)
    {
        BeltDisplay(BeltUp, BeltDown);
        // 上下の枠をアニメーションさせる.
        if (_endCount > _beltMoveStart)
        {
            BeltMove(BeltUp, BeltDown);
        }
        // クエストを終了した時の表現処理.
        if (_endCount > _stampLogDisPlayTime)
        {
            _uiDisplay[StampNunber] = true;
            StampResultAnim(StampNunber);
        }

        // スタンプロゴを押されてから少しずらして非表示にする.
        if (_endCount > 300)
        {
            ImageDisplay(false, BeltUp, BeltDown, StampNunber);
            ImageDisplay(true, (int)UIKinds.RESULT_BACKGROUND);
            AlphaUpdate();
            ResultBackgroundColor();
        }
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
    private void BeltDisplay(int Up, int Down)
    {
        _uiDisplay[Up] = true;
        _uiDisplay[Down] = true;
    }

    // 上下の黒帯の挙動.
    private void BeltMove(int Up, int Down)
    {
        _rectTransform[Up].DOAnchorPos(new Vector3(0.0f, 155.0f, 0.0f), 0.3f).SetEase(Ease.Linear);
        _rectTransform[Down].DOAnchorPos(new Vector3(0.0f, -155.0f, 0.0f), 0.3f).SetEase(Ease.Linear);
    }

    // スタンプロゴの結果表示のアニメーション.
    private void StampResultAnim(int LogNumber)
    {
        _rectTransform[LogNumber].DOScale(new Vector3(3.1f, 3.1f, 3.1f), 0.5f).SetEase(Ease.OutElastic);


        if (!_playSEFlag) return;

        _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.STAMP_PUSH);
        _playSEFlag = false;
    }

    // 指定した画像を表示、非表示にする.
    private void ImageDisplay(bool Active, params int[] UiKind)
    {
        int uiKinds = UiKind[0];

        for(int kinds = 0;  kinds < UiKind.Length; kinds++)
        {
            uiKinds = UiKind[kinds];
            _uiDisplay[uiKinds] = Active;
        }
    }

    // α値を更新.
    private void AlphaUpdate()
    {
        if(_resultColorA < _resultColorMaxA)
        {
            _resultColorA++;
        }
    }

    // リザルト画面の背景の色を代入.
    private void ResultBackgroundColor()
    {
        _image.color = new Color32(255, 255, 255, _resultColorA);
    }
}
