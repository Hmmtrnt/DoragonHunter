/*�N�G�X�g���X�g�̃A�j���[�V����*/

using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

public class QuestListAnim : MonoBehaviour
{
    // �A�j���[�V��������UI�̍��ڐ�.
    enum UIAnimNum
    {
        PARCHMENT_ONE,          // �N�G�X�g�p��.
        PARCHMENT_TWO,          // �N�G�X�g�p��(�������Ԗ�).
        PARCHMENT_THREE,        // �N�G�X�g�p��(������O�Ԗ�).
        SELECT_QUEST_NORMAL,    // ���ʂ̓�Փx�̍���.
        NORMAL_STRING,          // ���ʂ̓�Փx�̍��ڂ̕���.
        SELECT_QUEST_HARD,      // ����̓�Փx�̍���.
        HARD_STRING,            // ����̓�Փx�̍��ڂ̕���.
        SELECTED_UI,            // �I�����Ă���UI.
        EXPLANATION,            // �����e�L�X�g.
        MAX_NUM                 // �ő吔.
    }

    // �eUI.
    public GameObject[] _ui;
    // �eUI�̍��W.
    private RectTransform[] _rectTransforms = new RectTransform[(int)UIAnimNum.MAX_NUM];
    // �eUI�̐F.
    private Image[] _images = new Image[(int)UIAnimNum.MAX_NUM];
    // SE.
    private SEManager _seManager;
    // �Q�[���p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // �eUI�̓����x.
    private byte[] _uiColorA = new byte[(int)UIAnimNum.MAX_NUM];

    // �eUI�̕\����\��.
    private bool[] _uiDisplay = new bool[(int)UIAnimNum.MAX_NUM];

    // �N�G�X�g���J���Ă���̌o�ߎ��ԁD
    private int _questOpenCount = 0;


    void Start()
    {
        for(int UINum = 0; UINum < (int)UIAnimNum.MAX_NUM; UINum++)
        {
            _rectTransforms[UINum] = _ui[UINum].GetComponent<RectTransform>();
            _images[UINum] = _ui[UINum].GetComponent<Image>();
            _uiDisplay[UINum] = false;
            _uiColorA[UINum] = 0;

            //_images[UINum].color = new Color32(255, 255, 255, _uiColorA[UINum]);
            
        }
        _questOpenCount = 0;
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
    }

    void Update()
    {
        //DisableUpdate();
        Pos();
    }

    private void FixedUpdate()
    {
        OpenCount();
        Anim();
        UIColor();
        


        //Debug.Log(_rectTransforms[(int)UIAnimNum.PARCHMENT_ONE].anchoredPosition);
    }

    private void OnDisable()
    {
        // ��\���ɂ���Ƃ��ɏ����ʒu�ɐݒ�.
        //UIDisable();

        
    }

    private void OnEnable()
    {
        UIDisable();
    }

    // ��\���ɂȂ�u�Ԃ̏���.
    private void UIDisable()
    {

        _questOpenCount = 0;

        // ���W�̏�����.
        for(int UINum = (int)UIAnimNum.PARCHMENT_ONE; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
        {
            _rectTransforms[UINum].anchoredPosition = new Vector3(-414.0f, 0.0f, 0.0f);
            _rectTransforms[UINum].eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            _uiColorA[UINum] = 0;
            _rectTransforms[UINum].DORestart();
            Debug.Assert(_rectTransforms[UINum] == null);
        }

        
    }

    // UI�̐F����.
    private void UIColor()
    {
        for (int UINum = 0; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
        {
            _images[UINum].color = new Color32(255, 255, 255, _uiColorA[UINum]);
        }
    }

    // UI����\���̎��ɋN��������.
    private void DisableUpdate()
    {
        if(!_controllerManager._BButtonDown) { return; }

        _questOpenCount = 0;

        // ���W�̏�����.
        for (int UINum = (int)UIAnimNum.PARCHMENT_ONE; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
        {
            _rectTransforms[UINum].anchoredPosition = new Vector3(-414.0f, 0.0f, 0.0f);
            _rectTransforms[UINum].eulerAngles = new Vector3(0.0f,0.0f, 0.0f);
            _rectTransforms[UINum].DORestart();
        }
    }

    // �N�G�X�g���X�g���J�����Ƃ��Ɏ��Ԍo�߂�������.
    private void OpenCount()
    {
        _questOpenCount++;
    }

    // �S�̂̃A�j���[�V����.
    private void Anim()
    {
        QuestPaperAnim();
    }

    //���W�m�F.
    private void Pos()
    {
        if (_controllerManager._BButtonDown)
        //if (_controllerManager._XButtonDown)
        {
            for (int UINum = (int)UIAnimNum.PARCHMENT_ONE; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
            {
                _rectTransforms[UINum].anchoredPosition = new Vector3(-414.0f, 0.0f, 0.0f);
                _rectTransforms[UINum].eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                //_uiColorA[UINum] = 0;
                _rectTransforms[UINum].DORestart();
                //Debug.Log("to");
            }
        }
    }

    // �N�G�X�g�̗p���̃A�j���[�V����.
    private void QuestPaperAnim()
    {
        // �A�j���[�V�������J�n����^�C�~���O�w��.
        if (_questOpenCount >= 0)
        {
            //QuestPaperOneAnim();
            QuestPaperAnim((int)UIAnimNum.PARCHMENT_ONE, -10.0f);
        }
        if (_questOpenCount >= 5)
        {
            //QuestPaperTwoAnim();
            QuestPaperAnim((int)UIAnimNum.PARCHMENT_TWO, 0.0f);
        }
        if (_questOpenCount >= 10)
        {
            //QuestPaperThreeAnim();
            QuestPaperAnim((int)UIAnimNum.PARCHMENT_THREE, 10.0f);
        }
    }

    // �N�G�X�g�p���̃A�j���[�V����.
    private void QuestPaperAnim(int num, float RotateZ)
    {
        _rectTransforms[num].DOAnchorPos(new Vector3(414.0f, 0.0f, 0.0f), 0.5f).SetEase(Ease.OutQuad);
        _rectTransforms[num].DORotate(new Vector3(0.0f, 0.0f, RotateZ), 0.5f).SetEase(Ease.OutQuad);

        if (_uiColorA[num] < 255)
        {
            _uiColorA[num] += 5;
        }
    }

    // �N�G�X�g�̍��ڂ̃A�j���[�V����.


    // �N�G�X�g�̐����̃A�j���[�V����.
}
