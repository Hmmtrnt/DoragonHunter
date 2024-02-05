/*�N�G�X�g�N���A�^�C����`��*/

using UnityEngine;
using UnityEngine.UI;

public class ClearTime : MonoBehaviour
{
    // �N���A�^�C���̌���.
    enum ClearTimeDigit
    {
        MINUTE_TEN, // 10��.
        MINUTE_ONE, // 1��.
        SECOND_TEN, // 10�b.
        SECOND_ONE, // 1�b.
        MAX_DIGIT_NUM// �ő包��.
    }

    // �����̃X�v���C�g.
    enum SpriteNumber
    {
        ZERO,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        MAXNUM
    }

    // �N�G�X�g�I�����̏��.
    private HuntingEnd _huntingEnd;
    // �N���A�^�C����UI.
    public GameObject[] _clearTime;
    // �N���A�^�C���̍��W�擾
    private RectTransform[] _clearTimeTransform = new RectTransform[(int)ClearTimeDigit.MAX_DIGIT_NUM];
    // �N���A�^�C���̃X�v���C�g.
    private Image[] _clearTimeSprite = new Image[(int)ClearTimeDigit.MAX_DIGIT_NUM];
    // �N�G�X�g�^�C���̎��Ԃ̃X�v���C�g.
    public Sprite[] _timeNum;

    // �N���A���Ԃ�ۑ�.
    private int[] _questTime = new int[(int)ClearTimeDigit.MAX_DIGIT_NUM];

    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        for (int ClearTimeSpriteNum = 0; ClearTimeSpriteNum < (int)ClearTimeDigit.MAX_DIGIT_NUM; ClearTimeSpriteNum++)
        {
            _clearTimeTransform[ClearTimeSpriteNum] = _clearTime[ClearTimeSpriteNum].GetComponent<RectTransform>();
            _clearTimeSprite[ClearTimeSpriteNum] = _clearTime[ClearTimeSpriteNum].GetComponent<Image>();
        }
    }

    private void FixedUpdate()
    {
        // �N���A�^�C���̕`��.
        ClearTimeSubstitute();
        SpriteTimeChange();
    }

    // �N���A�^�C�����e���ɑ��.
    private void ClearTimeSubstitute()
    {
        if (_huntingEnd.GetQuestEnd())
        {
            // ���̏\�̈ʎ擾.
            _questTime[(int)ClearTimeDigit.MINUTE_TEN] = _huntingEnd._Minute / 10;
            // ���̈�̈ʎ擾.
            _questTime[(int)ClearTimeDigit.MINUTE_ONE] = _huntingEnd._Minute % 10;
            // �b�̏\�̈ʎ擾.
            _questTime[(int)ClearTimeDigit.SECOND_TEN] = _huntingEnd._Second / 10;
            // �b�̈�̈ʎ擾.
            _questTime[(int)ClearTimeDigit.SECOND_ONE] = _huntingEnd._Second % 10;
        }
    }

    // �^�C���ɂ���Čv�����Ԃ̃X�v���C�g��ύX.
    private void SpriteTimeChange()
    {
        for (int TimeDigit = 0; TimeDigit < (int)ClearTimeDigit.MAX_DIGIT_NUM; TimeDigit++)
        {
            //_clearTimeSprite[TimeDigit].sprite = _timeNum[TimeDigit];
            ClearTimeSprite(TimeDigit);
            TimeOneSizeChange(TimeDigit);
        }
    }

    // �N���A�^�C���̃X�v���C�g�ύX.
    private void ClearTimeSprite(int TimeDigit)
    {
        _clearTimeSprite[TimeDigit].sprite = _timeNum[_questTime[TimeDigit]];
        //Debug.Log(_questTime[TimeDigit]);
    }

    // �N���A�^�C����1��\������Ƃ���a��������̂ő傫����ύX.
    private void TimeOneSizeChange(int TimeDigit)
    {
        if (_clearTimeSprite[TimeDigit].sprite == _timeNum[(int)SpriteNumber.ONE])
        {
            _clearTimeTransform[TimeDigit].sizeDelta = new Vector2(20, 40);
        }
        else if (_clearTimeSprite[TimeDigit].sprite != _timeNum[(int)SpriteNumber.ONE])
        {
            _clearTimeTransform[TimeDigit].sizeDelta = new Vector2(30, 40);
        }
    }
}
