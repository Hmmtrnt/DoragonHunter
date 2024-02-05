/*���U���g��ʂ̃��S*/

using UnityEngine;
using UnityEngine.UI;

public class RankLog : MonoBehaviour
{
    // �����N�̎��.
    enum SpriteRank
    {
        S_RANK,
        A_RANK,
        B_RANK,
        C_RANK,
        MAX_RANK
    }

    // �N�G�X�g�I�����̏��擾.
    private HuntingEnd _huntingEnd;
    // �X�v���C�g�ύX�̂��߂Ɏ擾.
    private Image _rankLog;
    // ���W���擾.
    private RectTransform _rectTransform;
    // �����N�̃X�v���C�g.
    public Sprite[] _rank;

    // �����N�̒i�K(�����������قǃ����N������).
    private int _rankStep;


    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        _rankLog = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RankSpriteSubstitute();
    }

    // �X�v���C�g����.
    private void RankSpriteSubstitute()
    {
        // �����N�̌��ʂ���.
        _rankStep = _huntingEnd.GetRank();
        _rankLog.sprite = _rank[_rankStep];
        RankSizeChange();
    }

    // �����N�ɂ���ĕ`��̑傫����ύX.
    private void RankSizeChange()
    {
        if(_huntingEnd.GetRank() == (int)SpriteRank.S_RANK)
        {
            _rectTransform.sizeDelta = new Vector2(276, 319);
        }
        else if (_huntingEnd.GetRank() == (int)SpriteRank.A_RANK)
        {
            _rectTransform.sizeDelta = new Vector2(210, 252);
        }
        else if (_huntingEnd.GetRank() == (int)SpriteRank.B_RANK)
        {
            _rectTransform.sizeDelta = new Vector2(190, 241);
        }
        else if (_huntingEnd.GetRank() == (int)SpriteRank.C_RANK)
        {
            _rectTransform.sizeDelta = new Vector2(205, 200);
        }
    }
}
