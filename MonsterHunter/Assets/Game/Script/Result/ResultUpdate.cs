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
        RANK_TABLE,
        CLEAR_TIME,
        GUIDE
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
        LOG,        // ���S.
        MAX_NUM     // �ő吔.
    }

    // �����N�\��UI�ԍ�.
    enum RankTable
    {
        BACKGROUND,     // �w�i.
        // �ȉ��A���S.
        RANK_S,         // S�����N.
        S_MINUTE_TEN,   // S�����N�̏\��.
        S_MINUTE_ONE,   // S�����N�̈ꕪ.
        S_COLON,        // S�����N�^�C����:.
        S_SECOND_TEN,   // S�����N�̏\�b.
        S_SECOND_ONE,   // S�����N�̈�b.
        RANK_A,         // A�����N.
        A_MINUTE_TEN,   // A�����N�̏\��.
        A_MINUTE_ONE,   // A�����N�̈ꕪ.
        A_COLON,        // A�����N�^�C����:.
        A_SECOND_TEN,   // A�����N�̏\�b.
        A_SECOND_ONE,   // A�����N�̈�b.
        RANK_B,         // B�����N.
        B_MINUTE_TEN,   // B�����N�̏\��.
        B_MINUTE_ONE,   // B�����N�̈ꕪ.
        B_COLON,        // B�����N�^�C����:.
        B_SECOND_TEN,   // B�����N�̏\�b.
        B_SECOND_ONE,   // B�����N�̈�b.
        RANK_C,         // C�����N.
        C_MINUTE_TEN,   // C�����N�̏\��.
        C_MINUTE_ONE,   // C�����N�̈ꕪ.
        C_COLON,        // C�����N�^�C����:.
        C_SECOND_TEN,   // C�����N�̏\�b.
        C_SECOND_ONE,   // C�����N�̈�b.

        MAX_NUM
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

    // �����N�\��UI.
    public GameObject[] _rankTableUI;
    private Image[] _rankTableImage = new Image[(int)RankTable.MAX_NUM];
    private RectTransform[] _rankTableTransform = new RectTransform[(int)RankTable.MAX_NUM];
    // �����N�̓����x.
    private byte[] _rankTableColorA = new byte[(int)RankTable.MAX_NUM];

    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;

    // �A�j���[�V�������I��.
    private bool _animEnd = false;

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
        // �����N�\UI�̏�����.
        for(int RankTableUINum = 0; RankTableUINum < (int)RankTable.MAX_NUM; RankTableUINum++)
        {
            _rankTableImage[RankTableUINum] = _rankTableUI[RankTableUINum].GetComponent<Image>();
            _rankTableTransform[RankTableUINum] = _rankTableUI[RankTableUINum].GetComponent <RectTransform>();
            _rankTableColorA[RankTableUINum] = 0;
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
        if(_flameCount ==250)
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
        for(int RankTableUINum = 0; RankTableUINum < (int)RankTable.MAX_NUM; RankTableUINum++)
        {
            _rankTableImage[RankTableUINum].color = new Color32(255,255,255, _rankTableColorA[RankTableUINum]);
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
        if(_flameCount > 70)
        {
            RankTableAnim();
        }
    }

    // �N���A�^�C���̃A�j���[�V����.
    private void ClearTimeAnim()
    {
        if (_clearTimeColorA[(int)ClearTime.BACKGROUND] <= 200)
        {
            _clearTimeColorA[(int)ClearTime.BACKGROUND]+=2;
        }
        _clearTimeTransform[(int)ClearTime.BACKGROUND].DOAnchorPos(new Vector3(-155.0f,40.0f, 0.0f), 1.0f).SetEase(Ease.OutQuint);

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
        _rankTransform[(int)Rank.BACKGROUND].DOAnchorPos(new Vector3(155.0f, 40.0f, 0.0f), 1.0f).SetEase(Ease.OutQuint);
        if (_flameCount < 200) return;


        for(int RankUINum = (int)Rank.STRING; RankUINum < (int)Rank.MAX_NUM; RankUINum++)
        {
            if (_rankColorA[RankUINum] != 255)
            {
                _rankColorA[RankUINum] += 5;
            }
        }
    }

    // �����N�\�̃A�j���[�V����.
    private void RankTableAnim()
    {
        if (_rankTableColorA[(int)RankTable.BACKGROUND] <= 200)
        {
            _rankTableColorA[(int)RankTable.BACKGROUND] += 2;
        }

        for(int RankTableNum = (int)RankTable.RANK_S; RankTableNum < (int)RankTable.MAX_NUM; RankTableNum++)
        {
            if (_rankTableColorA[RankTableNum] != 255) 
            {
                _rankTableColorA[RankTableNum] += 5;
            }
        }

        _rankTableTransform[(int)RankTable.BACKGROUND].DOAnchorPos(new Vector3(-105.0f, -55.0f, 0.0f), 1.0f).SetEase(Ease.OutQuint);
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
            RankTableFinalPositionAndFinalAlpha();
            Debug.Log("�ʂ�");
            _animEnd = true;
        }
    }

    // �N���A�^�C���̍ŏI�ʒu�Ɠ����x���.
    private void ClearTimeFinalPositionAndFinalAlhpa()
    {
        // �w�i���������x���Ⴄ.
        _clearTimeColorA[(int)ClearTime.BACKGROUND] = 200;
        _clearTimeTransform[(int)ClearTime.BACKGROUND].anchoredPosition = new Vector3(-155.0f, 40.0f, 0.0f);
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
        _rankTransform[(int)Rank.BACKGROUND].anchoredPosition = new Vector3(155.0f, 40.0f, 0.0f);
        for (int ClearTimeUINum = (int)Rank.STRING; ClearTimeUINum < (int)Rank.MAX_NUM; ClearTimeUINum++)
        {
            _rankColorA[ClearTimeUINum] = 255;
        }
    }

    // �����N�\�̍ŏI�ʒu�Ɠ����x���.
    private void RankTableFinalPositionAndFinalAlpha()
    {
        // �w�i���������x���Ⴄ.
        _rankTableColorA[(int)RankTable.BACKGROUND] = 200;
        _rankTableTransform[(int)RankTable.BACKGROUND].anchoredPosition = new Vector3(-105.0f, -55.0f, 0.0f);
        for (int ClearTimeUINum = (int)RankTable.RANK_S; ClearTimeUINum < (int)RankTable.MAX_NUM; ClearTimeUINum++)
        {
            _rankTableColorA[ClearTimeUINum] = 255;
        }
    }

    // �A�j���[�V�������I�������̏��擾.
    public bool GetAnimEnd() { return _animEnd; }
}
