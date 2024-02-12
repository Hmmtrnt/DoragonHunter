/*ランクのエフェクト*/

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RankEffect : MonoBehaviour
{
    enum EffectNum
    {
        EFFECT1,
        EFFECT2, 
        EFFECT3,
        EFFECT4, 
        MAX_NUM
    }

    private ResultUpdate _resultUpdate;
    private Sequence _sequence;
    public GameObject[] _effect;
    private Image[] _image = new Image[(int)EffectNum.MAX_NUM];
    private RectTransform[] _rectTransform = new RectTransform[(int)EffectNum.MAX_NUM];
    // エフェクトの再生時間.
    private int _EffectCount = 0;
    private float _effectScale = 1.0f;
    private byte[] _alpha = new byte[(int)EffectNum.MAX_NUM];

    void Start()
    {
        _sequence = DOTween.Sequence();
        _resultUpdate = GameObject.Find("ResultBackGround").GetComponent<ResultUpdate>();
        for (int UINum = 0;  UINum < (int)EffectNum.MAX_NUM; UINum++)
        {
            _image[UINum] = _effect[UINum].GetComponent<Image>();
            _rectTransform[UINum] = _effect[UINum].GetComponent<RectTransform>();
            _alpha[UINum] = 0;
            _image[UINum].color = new Color32(255, 255, 255, _alpha[UINum]);
        }
        _EffectCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Color();
        Fade();
        if(!_resultUpdate._animEnd)
        {
            Anim();
        }
        else
        {
            AnimEnd();
        }
        
        EffectCountUp();
    }

    // 再生時間を進ませる.
    private void EffectCountUp()
    {
        _EffectCount++;
    }

    // 色の代入.
    private void Color()
    {
        for (int UINum = 0; UINum < (int)EffectNum.MAX_NUM; UINum++)
        {
            _image[UINum].color = new Color32(255,255,255, _alpha[UINum]);
            
        }
    }

    // アニメーション.
    private void Anim()
    {
        if(_EffectCount == 1)
        {
            EffectAnimOne((int)EffectNum.EFFECT1, new Vector3(80.0f, 66.0f, 0.0f), 0.5f);
            EffectAnimOne((int)EffectNum.EFFECT2, new Vector3(-77.0f, -80.0f, 0.0f), 0.5f);
            EffectAnimOne((int)EffectNum.EFFECT3, new Vector3(-77.0f, 65.0f, 0.0f), 0.5f);
            EffectAnimOne((int)EffectNum.EFFECT4, new Vector3(50.0f, -70.0f, 0.0f), 0.5f);
        }
        if(_EffectCount == 50)
        {
            IdleEffectAnim((int)EffectNum.EFFECT1, new Vector3(0.8f, 0.8f, 0.8f), 1.0f);
            IdleEffectAnim((int)EffectNum.EFFECT2, new Vector3(1.0f, 1.0f, 1.0f), 1.0f);
            IdleEffectAnim((int)EffectNum.EFFECT3, new Vector3(0.5f, 0.5f, 0.5f), 1.0f);
            IdleEffectAnim((int)EffectNum.EFFECT4, new Vector3(0.7f, 0.7f, 0.7f), 1.0f);
        }
    }

    // アニメーション終了時.
    private void AnimEnd()
    {
        _rectTransform[(int)EffectNum.EFFECT1].anchoredPosition = new Vector3(80.0f, 66.0f, 0.0f);
        _rectTransform[(int)EffectNum.EFFECT2].anchoredPosition = new Vector3(-77.0f, -80.0f, 0.0f);
        _rectTransform[(int)EffectNum.EFFECT3].anchoredPosition = new Vector3(-77.0f, 65.0f, 0.0f);
        _rectTransform[(int)EffectNum.EFFECT4].anchoredPosition = new Vector3(50.0f, -70.0f, 0.0f);
        if (_EffectCount == 60)
        {
            IdleEffectAnim((int)EffectNum.EFFECT1, new Vector3(0.8f, 0.8f, 0.8f), 1.0f);
            IdleEffectAnim((int)EffectNum.EFFECT2, new Vector3(1.0f, 1.0f, 1.0f), 1.0f);
            IdleEffectAnim((int)EffectNum.EFFECT3, new Vector3(0.5f, 0.5f, 0.5f), 1.0f);
            IdleEffectAnim((int)EffectNum.EFFECT4, new Vector3(0.7f, 0.7f, 0.7f), 1.0f);
        }
    }

    // フェード.
    private void Fade()
    {
        for (int UINum = 0; UINum < (int)EffectNum.MAX_NUM; UINum++)
        {
            if (_alpha[UINum] < 255)
            {
                _alpha[UINum] += 5;
            }
        }
    }

    // エフェクトのアニメーション.
    private void EffectAnimOne(int EffectNum, Vector3 Position, float Time)
    {
        _sequence.Append(_rectTransform[EffectNum].DOAnchorPos(Position, Time).SetEase(Ease.OutQuad))
            .Play();
    }

    // 待機時のエフェクトのアニメーション.
    private void IdleEffectAnim(int EffectNum, Vector3 Scale, float Time)
    {
        //_sequence.Append(_rectTransform[EffectNum].DOScale(Scale, Time)
        //    .SetLoops(-1, LoopType.Yoyo))
        //    .Play();

        _rectTransform[EffectNum].DOScale(Scale, Time)
            .SetLoops(-1, LoopType.Yoyo);
        //_rectTransform[EffectNum].DOLocalMoveX(1f, 2f).SetLoops(-1, LoopType.Yoyo);
    }


}
