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
    public GameObject[] _UI;
    // �eUI�̍��W.
    private RectTransform[] _rectTransforms = new RectTransform[(int)UIAnimNum.MAX_NUM];
    // SE.
    private SEManager _seManager;

    // �eUI�̕\����\��.
    private bool[] _uiDisplay = new bool[(int)UIAnimNum.MAX_NUM];

    // �N�G�X�g���J���Ă���̌o�ߎ��ԁD
    private int _questOpenCount = 0;


    void Start()
    {
        for(int UINum = 0; UINum < (int)UIAnimNum.MAX_NUM; UINum++)
        {
            _rectTransforms[UINum] = _UI[UINum].GetComponent<RectTransform>();
            _uiDisplay[UINum] = false;
        }
        _questOpenCount = 0;
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        OpenCount();
    }

    private void OnDisable()
    {
        // ��\���ɂ���Ƃ��ɏ����ʒu�ɐݒ�.
        UIDisable();
    }

    // ��\���ɂȂ�u�Ԃ̏���.
    private void UIDisable()
    {
        _questOpenCount = 0;
    }

    // �N�G�X�g���X�g���J�����Ƃ��Ɏ��Ԍo�߂�������.
    private void OpenCount()
    {
        _questOpenCount++;
    }

    // �S�̂̃A�j���[�V����.
    private void Anim(int layerIndex)
    {
        
    }

    // �N�G�X�g�̗p���̃A�j���[�V����.
    private void QuestPaperAnim()
    {
        if(_questOpenCount == 0 )
        {
            _rectTransforms[(int)UIAnimNum.PARCHMENT_ONE].DOAnchorPos(new Vector3(414.0f, 0.0f, 0.0f), 0.5f).SetEase(Ease.OutQuad);

        }
    }

    // �N�G�X�g�̍��ڂ̃A�j���[�V����.

    // �N�G�X�g�̐����̃A�j���[�V����.
}
