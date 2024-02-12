/*ランクのUIアニメーション*/

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankAnim : MonoBehaviour
{
    enum UIKinds
    {
        RANK,
        EFFECT,
        MAX_NUM
    }

    public GameObject[] _ui;
    private Image _image;
    private RectTransform _rectTransform;

    private HuntingEnd _end;
    // 時間経過.
    private int _timeCount = 0;
    // 存在しているかどうか.
    private bool[] _isEnable = new bool[(int)UIKinds.MAX_NUM];
    // 透明度.
    private byte _alpha = 0;

    void Start()
    {
        _end = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        _rectTransform = _ui[(int)UIKinds.RANK].GetComponent<RectTransform>();
        _timeCount = 0;
        _alpha = 0;

        _image = _ui[(int)UIKinds.RANK].GetComponent<Image>();
        for(int UINum = 0; UINum < (int)UIKinds.MAX_NUM; UINum++)
        {
            _isEnable[UINum] = false;
            _ui[UINum].SetActive(false);
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CountUp();
        Draw();
        Color();
        Anim();
    }

    // 表示非表示.
    private void Draw()
    {
        for (int UINum = 0; UINum < (int)UIKinds.MAX_NUM; UINum++)
        {
            _ui[UINum].SetActive(_isEnable[UINum]);
        }
    }

    // 時間経過.
    private void CountUp()
    {
        _timeCount++;
    }

    // 色の代入.
    private void Color()
    {
        _image.color = new Color32(255, 255, 255, _alpha);
    }

    // アニメーション.
    private void Anim()
    {
        if(_timeCount == 250)
        {
            _isEnable[(int)UIKinds.RANK] = true;
            RankLogAnim();
        }
        if(_timeCount >= 250)
        {
            RankFade();
            //RankLogAnim();
        }

        if(_timeCount == 300)
        {
            _isEnable[(int)UIKinds.EFFECT] = true;
        }
        //RankLogAnim();
    }

    // ランクのアニメーション.
    private void RankLogAnim()
    {
        _rectTransform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.5f);
        _rectTransform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.5f);
    }

    // ランクのフェード
    private void RankFade()
    {
        if (_alpha < 255)
        {
            _alpha += 5;
        }
    }

    // エフェクトのアニメーション.
    private void EffectAnim()
    {

    }

}
