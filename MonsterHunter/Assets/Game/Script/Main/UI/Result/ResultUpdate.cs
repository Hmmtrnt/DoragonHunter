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
        CLEAR,              // �N���A.
        FAILED,             // ���s.
        // �x���g.
        // �N���A��.
        CLEAR_BELT_UP,      // ��.
        CLEAR_BELT_DOWN,    // ��.
        FAILED_BELT_UP,     // ��.
        FAILED_BELT_DOWN,   // ��.
        MAXNUM              // �ő吔.
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

        _rectTransform[(int)UIKinds.CLEAR_BELT_UP].anchoredPosition = new Vector3(0, 200,0);
        _rectTransform[(int)UIKinds.CLEAR_BELT_DOWN].anchoredPosition = new Vector3(0, -200,0);
        _rectTransform[(int)UIKinds.FAILED_BELT_UP].anchoredPosition = new Vector3(0, 200, 0);
        _rectTransform[(int)UIKinds.FAILED_BELT_DOWN].anchoredPosition = new Vector3(0, -200, 0);

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
        _ui[(int)UIKinds.CLEAR].SetActive(_uiDisplayHide[(int)UIKinds.CLEAR]);
        _ui[(int)UIKinds.FAILED].SetActive(_uiDisplayHide[(int)UIKinds.FAILED]);
        _ui[(int)UIKinds.CLEAR_BELT_UP].SetActive(_uiDisplayHide[(int)UIKinds.CLEAR_BELT_UP]);
        _ui[(int)UIKinds.CLEAR_BELT_DOWN].SetActive(_uiDisplayHide[(int)UIKinds.CLEAR_BELT_DOWN]);
        _ui[(int)UIKinds.FAILED_BELT_UP].SetActive(_uiDisplayHide[(int)UIKinds.FAILED_BELT_UP]);
        _ui[(int)UIKinds.FAILED_BELT_DOWN].SetActive(_uiDisplayHide[(int)UIKinds.FAILED_BELT_DOWN]);
    }

    // �N�G�X�g���I���������ɂ�����A�j���[�V����.
    private void UiAnim(int BeltUp, int BeltDown, int StampNunber)
    {
        BeltDisplay(BeltUp, BeltDown);
        if (_endCount > 60)
        {
            BeltMove(BeltUp, BeltDown);
        }
        // �N�G�X�g���N���A�������̕\������.
        if (_endCount > 160)
        {
            _uiDisplayHide[StampNunber] = true;
            StampResultAnim(StampNunber);
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
        _uiDisplayHide[Up] = true;
        _uiDisplayHide[Down] = true;
    }

    // �㉺�̍��т̋���.
    private void BeltMove(int Up, int Down)
    {
        _rectTransform[Up].DOAnchorPos(new Vector3(0.0f, 150.0f, 0.0f), 0.3f).SetEase(Ease.Linear);
        _rectTransform[Down].DOAnchorPos(new Vector3(0.0f, -150.0f, 0.0f), 0.3f).SetEase(Ease.Linear);
    }

    // �X�^���v���S�̌��ʕ\���̃A�j���[�V����.
    private void StampResultAnim(int LogNumber)
    {
        _rectTransform[LogNumber].DOScale(new Vector3(3.1f, 3.1f, 3.1f), 0.5f).SetEase(Ease.OutElastic);
    }
}
