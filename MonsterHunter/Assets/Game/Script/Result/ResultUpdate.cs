/*���U���g���ʂ̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultUpdate : MonoBehaviour
{
    enum UIKinds
    {
        // �e�\���̔w�i.
        TITLE, 
        RANK,
        GUIDE,
        CLEAR_TIME,
        RANK_EFFECT,
        MAX_NUM
    }

    // �N���A�^�C����UI�ԍ�.
    enum ClearTime
    {
        BACKGROUND, // �w�i.
        STRING,     // ����.
        MINUTE_TEN, // �\��.
        MINUTE_ONE, // �ꕪ.
        COLON,      // :
        SECONDS_TEN,// �\�b.
        SECONDS_ONE,// ��b.
        MAX_NUM      // �ő吔.
    }

    // �����N�̌��ʕ\����UI�ԍ�.
    enum Rank
    {
        BACKGROUND, // �w�i.
        STRING,     // ������.
        MAX_NUM     // �ő吔.
    }

    private HuntingEnd _huntingEnd;
    private QuestEndUpdate _questEndUpdate;

    // ���U���g��ʂ�UI.
    public GameObject[] _ui;
    // �N���A�^�C����UI.
    public GameObject[] _clearTimeUI;
    private Image[] _clearTimeImage = new Image[(int)ClearTime.MAX_NUM];
    private RectTransform[] _clearTimeTransform = new RectTransform[(int)ClearTime.MAX_NUM];
    // �N���A�^�C���̓����x.
    private byte[] _clearTimeColorA = new byte[(int)ClearTime.MAX_NUM];

    // �����N��UI.
    public GameObject[] _rankUI;
    private Image[] _rankImage = new Image[(int)Rank.MAX_NUM];
    private RectTransform[] _rankTransform = new RectTransform[(int)Rank.MAX_NUM];
    // �����N�̓����x.
    private byte[] _rankColorA = new byte[(int)Rank.MAX_NUM];
    
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;

    // �A�j���[�V�������I��.
    public bool _animEnd = false;

    // ���U���g��ʂ̌o�ߎ���.
    private int _flameCount = 0;

    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        _questEndUpdate = GameObject.Find("ResultUi").GetComponent<QuestEndUpdate>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        
        // �N���A�^�C��UI�̏�����.
        for(int ClearTimeUINum = 0;  ClearTimeUINum < (int)ClearTime.MAX_NUM; ClearTimeUINum++)
        {
            _clearTimeImage[ClearTimeUINum] = _clearTimeUI[ClearTimeUINum].GetComponent<Image>();
            _clearTimeTransform[ClearTimeUINum] = _clearTimeUI[ClearTimeUINum].GetComponent<RectTransform>();
            _clearTimeColorA[ClearTimeUINum] = 0;
        }
        // �����NUI�̏�����.
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

    // �o�ߎ��Ԃ����Z.
    private void FlameCount()
    {
        _flameCount++;
        if(_flameCount ==400)
        {
            _animEnd=true;
        }
    }

    // UI�ɓ����x����.
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

    // �eUI�̓����x.
    private void UIAlpha()
    {
        // �A�j���[�V�������I��������X�L�b�v.
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

    // �N���A�^�C���̃A�j���[�V����.
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

    // �����N�̃A�j���[�V����.
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

    // ���U���g��ʂ̃A�j���[�V�����X�L�b�v.
    private void AnimSkip()
    {
        // �A�j���[�V�������I������Ə��������Ȃ�.
        if (_animEnd) return;

        if (_controllerManager._PressAnyButton)
        {
            ClearTimeFinalPositionAndFinalAlhpa();
            RankFinalPositionAndFinalAlpha();
            
            _animEnd = true;
        }
    }

    // �N���A�^�C���̍ŏI�ʒu�Ɠ����x���.
    private void ClearTimeFinalPositionAndFinalAlhpa()
    {
        // �w�i���������x���Ⴄ.
        _clearTimeColorA[(int)ClearTime.BACKGROUND] = 200;
        _clearTimeTransform[(int)ClearTime.BACKGROUND].anchoredPosition = new Vector3(160.0f, 70.0f, 0.0f);
        for (int ClearTimeUINum = (int)ClearTime.STRING; ClearTimeUINum < (int)ClearTime.MAX_NUM; ClearTimeUINum++)
        {
            _clearTimeColorA[ClearTimeUINum] = 255;
        }
    }

    // �����N�̍ŏI�ʒu�Ɠ����x���.
    private void RankFinalPositionAndFinalAlpha()
    {
        // �w�i���������x���Ⴄ.
        _rankColorA[(int)Rank.BACKGROUND] = 200;
        _rankTransform[(int)Rank.BACKGROUND].anchoredPosition = new Vector3(-135.0f, -30.0f, 0.0f);
        for (int ClearTimeUINum = (int)Rank.STRING; ClearTimeUINum < (int)Rank.MAX_NUM; ClearTimeUINum++)
        {
            _rankColorA[ClearTimeUINum] = 255;
        }
    }


    // �A�j���[�V�������I�������̏��擾.
    public bool GetAnimEnd() { return _animEnd; }
}
