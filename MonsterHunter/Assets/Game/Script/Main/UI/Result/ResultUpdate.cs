/*���U���g��ʂ̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ResultUpdate : MonoBehaviour
{
    // UI�̎��.
    public enum UIKinds
    {
        CLEAR,      // �N���A.
        FAILED,     // ���s.
        // �x���g.
        BELTUP,     // ��.
        BELTDOWN,   // ��.
        MAXNUM      // �ő吔.
    }

    // UI�I�u�W�F�N�g.
    public GameObject[] _ui;
    // ��I�����̏��.
    private HuntingEnd _huntingEnd;
    // �eUI�̍��W.
    private RectTransform[] _rectTransform = new RectTransform[(int)UIKinds.MAXNUM];
    // UI��\����\���ɂ��邩�ǂ���.
    private bool[] _uiDisplayHide = new bool[(int)UIKinds.MAXNUM];
    // �I�����Ă���̌o�ߎ��ԁD
    private int _endCount = 0;

    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();

        for(int UINumber = 0; UINumber < (int)UIKinds.MAXNUM; UINumber++)
        {
            _uiDisplayHide[UINumber] = false;
            _rectTransform[UINumber] = _ui[UINumber].GetComponent<RectTransform>();
        }

        _rectTransform[(int)UIKinds.BELTUP].anchoredPosition = new Vector3(0, 200,0);
        _rectTransform[(int)UIKinds.BELTDOWN].anchoredPosition = new Vector3(0, -200,0);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        StartCount();
        UIDisplayHide();

        if (_huntingEnd.GetQuestClear())
        {
            Clear();
        }
        else if(_huntingEnd.GetQuestFailed())
        {
            Failed();
        }
    }

    // UI�̕\����\��.
    private void UIDisplayHide()
    {
        _ui[(int)UIKinds.CLEAR].SetActive(_uiDisplayHide[(int)UIKinds.CLEAR]);
        _ui[(int)UIKinds.FAILED].SetActive(_uiDisplayHide[(int)UIKinds.FAILED]);
        _ui[(int)UIKinds.BELTUP].SetActive(_uiDisplayHide[(int)UIKinds.BELTUP]);
        _ui[(int)UIKinds.BELTDOWN].SetActive(_uiDisplayHide[(int)UIKinds.BELTDOWN]);
    }

    // �N�G�X�g���N���A�������ɌĂяo��.
    private void Clear()
    {
        // �N�G�X�g���N���A�������̕\������.
        BeltDisplay();

        if(_endCount > 60)
        {
            BeltMove();
        }
        if(_endCount > 160)
        {
            _uiDisplayHide[(int)UIKinds.CLEAR] = true;
            StampResultAnim((int)UIKinds.CLEAR);
        }
        



    }

    // �N�G�X�g�����s�������ɌĂяo��.
    private void Failed()
    {
        // �N�G�X�g�����s�������̕\������.
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
    private void BeltDisplay()
    {
        _uiDisplayHide[(int)UIKinds.BELTUP] = true;
        _uiDisplayHide[(int)UIKinds.BELTDOWN] = true;
    }

    // �㉺�̍��т̋���.
    private void BeltMove()
    {
        _rectTransform[(int)UIKinds.BELTUP].DOAnchorPos(new Vector3(0.0f, 150.0f, 0.0f), 0.3f).SetEase(Ease.Linear);
        _rectTransform[(int)UIKinds.BELTDOWN].DOAnchorPos(new Vector3(0.0f, -150.0f, 0.0f), 0.3f).SetEase(Ease.Linear);
    }

    // �X�^���v���S�̌��ʕ\���̃A�j���[�V����.
    private void StampResultAnim(int LogNumber)
    {
        _rectTransform[LogNumber].DOScale(new Vector3(3.1f, 3.1f, 3.1f), 0.5f).SetEase(Ease.OutElastic);
    }
}
