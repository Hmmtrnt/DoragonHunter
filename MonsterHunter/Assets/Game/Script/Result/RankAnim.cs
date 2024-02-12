/*�����N��UI�A�j���[�V����*/

using DG.Tweening;
using DG.Tweening.Core.Easing;
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
    private SEManager _seManager;

    private HuntingEnd _end;

    private ResultUpdate _resultUpdate;

    // ���Ԍo��.
    private int _timeCount = 0;
    // ���݂��Ă��邩�ǂ���.
    private bool[] _isEnable = new bool[(int)UIKinds.MAX_NUM];
    // �����x.
    private byte _alpha = 0;

    void Start()
    {
        _end = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _resultUpdate = GameObject.Find("ResultBackGround").GetComponent<ResultUpdate>();
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
        

        if(_resultUpdate._animEnd)
        {
            AnimEnd();
        }
        else
        {
            Anim();
        }
    }

    // �\����\��.
    private void Draw()
    {
        for (int UINum = 0; UINum < (int)UIKinds.MAX_NUM; UINum++)
        {
            _ui[UINum].SetActive(_isEnable[UINum]);
        }
    }

    // ���Ԍo��.
    private void CountUp()
    {
        _timeCount++;
    }

    // �F�̑��.
    private void Color()
    {
        _image.color = new Color32(255, 255, 255, _alpha);
    }

    // �A�j���[�V����.
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

        if(_timeCount == 260 && _end.GetRank() == 0)
        {
            _isEnable[(int)UIKinds.EFFECT] = true;
        }
        if(_timeCount == 270)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.QUEST_START);
        }
        //RankLogAnim();
    }

    // �A�j���[�V�����I����.
    private void AnimEnd()
    {
        _alpha = 255;
        _rectTransform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
        _rectTransform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        _isEnable[(int)UIKinds.RANK] = true;
        if(_end.GetRank()==0)
        {
            _isEnable[(int)UIKinds.EFFECT] = true;
        }
        
    }

    // �����N�̃A�j���[�V����.
    private void RankLogAnim()
    {
        _rectTransform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.5f);
        _rectTransform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.5f);
    }

    // �����N�̃t�F�[�h
    private void RankFade()
    {
        if (_alpha < 255)
        {
            _alpha += 5;
        }
    }

    // �G�t�F�N�g�̃A�j���[�V����.
    private void EffectAnim()
    {

    }

}
