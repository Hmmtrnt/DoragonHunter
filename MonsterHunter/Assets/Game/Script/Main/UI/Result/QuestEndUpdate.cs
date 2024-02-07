/*�N�G�X�g�I����ʂ��烊�U���g�\���܂ł̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class QuestEndUpdate : MonoBehaviour
{
    // UI�̎��.
    public enum UIKinds
    {
        CLEAR,              // �N���A.
        FAILED,             // ���s.
        // �x���g.
        // �N���A��.
        CLEAR_BELT_UP,      // ��.
        CLEAR_BELT_DOWN,    // ��.
        FAILED_BELT_UP,     // ��.
        FAILED_BELT_DOWN,   // ��.
        RESULT_BACKGROUND,  // ���ʂ�\������Ƃ��̔w�i.
        MAXNUM              // �ő吔.
    }

    // UI�I�u�W�F�N�g.
    public GameObject[] _ui;
    // ��I�����̏��.
    private HuntingEnd _huntingEnd;
    // �eUI�̍��W.
    private RectTransform[] _rectTransform = new RectTransform[(int)UIKinds.MAXNUM];
    // �eUI�̐F.
    private Image _image;
    // SE.
    private SEManager _seManager;

    // UI��\����\���ɂ��邩�ǂ���.
    private bool[] _uiDisplay = new bool[(int)UIKinds.MAXNUM];
    // �I�����Ă���̌o�ߎ��ԁD
    private int _endCount = 0;

    // �㉺�̃x���g�̃A�j���[�V�����J�n����.
    private const int _beltMoveStart = 60;
    // �X�^���v���S�̕\������.
    private const int _stampLogDisPlayTime = 160;

    // ���U���g��ʂ̔w�i�̃��l.
    private byte _resultColorA = 0;
    // ���U���g��ʂ̔w�i�̌��E���l.
    private const byte _resultColorMaxA = 50;

    // SE����x�����炳�Ȃ�.
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

        // �㉺�̘g�̏����ʒu.
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

        // ���s�ɂ����UI��ύX.
        if (_huntingEnd.GetQuestClear())
        {
            UiAnim((int)UIKinds.CLEAR_BELT_UP, (int)UIKinds.CLEAR_BELT_DOWN, (int)UIKinds.CLEAR);
        }
        else if(_huntingEnd.GetQuestFailed())
        {
            UiAnim((int)UIKinds.FAILED_BELT_UP, (int)UIKinds.FAILED_BELT_DOWN, (int)UIKinds.FAILED);
        }
    }

    // UI�̕\����\��.
    private void UIDisplayHide()
    {
        for(int kinds = 0; kinds < (int)UIKinds.MAXNUM; kinds++)
        {
            _ui[kinds].SetActive(_uiDisplay[kinds]);
        }
    }

    // �N�G�X�g���I���������ɂ�����A�j���[�V����.
    private void UiAnim(int BeltUp, int BeltDown, int StampNunber)
    {
        BeltDisplay(BeltUp, BeltDown);
        // �㉺�̘g���A�j���[�V����������.
        if (_endCount > _beltMoveStart)
        {
            BeltMove(BeltUp, BeltDown);
        }
        // �N�G�X�g���I���������̕\������.
        if (_endCount > _stampLogDisPlayTime)
        {
            _uiDisplay[StampNunber] = true;
            StampResultAnim(StampNunber);
        }

        // �X�^���v���S��������Ă��班�����炵�Ĕ�\���ɂ���.
        if (_endCount > 300)
        {
            ImageDisplay(false, BeltUp, BeltDown, StampNunber);
            ImageDisplay(true, (int)UIKinds.RESULT_BACKGROUND);
            AlphaUpdate();
            ResultBackgroundColor();
        }
    }

    // �N�G�X�g�I�����Ă���J�E���g�J�n.
    private void StartCount()
    {
        if(_huntingEnd.GetQuestClear() ||  _huntingEnd.GetQuestFailed())
        {
            _endCount++;
        }
    }

    // �㉺�̘g��\��
    private void BeltDisplay(int Up, int Down)
    {
        _uiDisplay[Up] = true;
        _uiDisplay[Down] = true;
    }

    // �㉺�̍��т̋���.
    private void BeltMove(int Up, int Down)
    {
        _rectTransform[Up].DOAnchorPos(new Vector3(0.0f, 155.0f, 0.0f), 0.3f).SetEase(Ease.Linear);
        _rectTransform[Down].DOAnchorPos(new Vector3(0.0f, -155.0f, 0.0f), 0.3f).SetEase(Ease.Linear);
    }

    // �X�^���v���S�̌��ʕ\���̃A�j���[�V����.
    private void StampResultAnim(int LogNumber)
    {
        _rectTransform[LogNumber].DOScale(new Vector3(3.1f, 3.1f, 3.1f), 0.5f).SetEase(Ease.OutElastic);


        if (!_playSEFlag) return;

        _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.STAMP_PUSH);
        _playSEFlag = false;
    }

    // �w�肵���摜��\���A��\���ɂ���.
    private void ImageDisplay(bool Active, params int[] UiKind)
    {
        int uiKinds = UiKind[0];

        for(int kinds = 0;  kinds < UiKind.Length; kinds++)
        {
            uiKinds = UiKind[kinds];
            _uiDisplay[uiKinds] = Active;
        }
    }

    // ���l���X�V.
    private void AlphaUpdate()
    {
        if(_resultColorA < _resultColorMaxA)
        {
            _resultColorA++;
        }
    }

    // ���U���g��ʂ̔w�i�̐F����.
    private void ResultBackgroundColor()
    {
        _image.color = new Color32(255, 255, 255, _resultColorA);
    }
}
