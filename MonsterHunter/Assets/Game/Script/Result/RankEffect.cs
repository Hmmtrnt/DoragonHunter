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

    private Sequence _sequence;
    public GameObject[] _effect;
    private Image[] _image = new Image[(int)EffectNum.MAX_NUM];
    private RectTransform[] _rectTransform = new RectTransform[(int)EffectNum.MAX_NUM];
    // エフェクトの再生時間.
    private int _EffectCount = 0;
    private float _effectScale = 1.0f;

    void Start()
    {
        _sequence = DOTween.Sequence();
        for (int UINum = 0;  UINum < (int)EffectNum.MAX_NUM; UINum++)
        {
            _image[UINum] = _effect[UINum].GetComponent<Image>();
            _rectTransform[UINum] = _effect[UINum].GetComponent<RectTransform>();
        }
        _EffectCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Anim();
        EffectCountUp();
    }

    // 再生時間を進ませる.
    private void EffectCountUp()
    {
        _EffectCount++;
    }

    // アニメーション.
    private void Anim()
    {
        if(_EffectCount == 200)
        {
            EffectAnimOne((int)EffectNum.EFFECT1, new Vector3(80.0f, 66.0f, 0.0f), 0.5f);
            EffectAnimOne((int)EffectNum.EFFECT2, new Vector3(-77.0f, -80.0f, 0.0f), 0.5f);
            EffectAnimOne((int)EffectNum.EFFECT3, new Vector3(-77.0f, 65.0f, 0.0f), 0.5f);
            EffectAnimOne((int)EffectNum.EFFECT4, new Vector3(50.0f, -70.0f, 0.0f), 0.5f);
        }
        if(_EffectCount == 250)
        {
            IdleEffectAnim((int)EffectNum.EFFECT1, new Vector3(0.7f, 0.7f, 0.7f), 1.0f);
            IdleEffectAnim((int)EffectNum.EFFECT2, new Vector3(0.9f, 0.9f, 0.9f), 1.0f);
            IdleEffectAnim((int)EffectNum.EFFECT3, new Vector3(0.6f, 0.6f, 0.6f), 1.0f);
            IdleEffectAnim((int)EffectNum.EFFECT4, new Vector3(0.8f, 0.8f, 0.8f), 1.0f);
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
