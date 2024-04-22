/*�^�C���e�[�u���̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTable : MonoBehaviour
{
    // �^�C���̌���.
    enum TimeDigit
    {
        // S�����N.
        S_MINUTE_TEN,   // �\��.
        S_MINUTE_ONE,   // �ꕪ.
        // A�����N.
        A_MINUTE_TEN,
        A_MINUTE_ONE,
        // B�����N.
        B_MINUTE_TEN,
        B_MINUTE_ONE,
        // C�����N.
        C_MINUTE_TEN,
        C_MINUTE_ONE,

        MAX_NUM         // �ő吔.
    }

    // �^�C���̉摜
    enum TimeSprite
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

        MAX_NUM // �ő吔.
    }

    // �����N�\�̎���.
    public GameObject[] _timeUi;
    // �X�v���C�g�ύX�̂��߂Ɏ擾.
    private Image[] _timeImage;
    // �����N�\�̉摜.
    public Sprite[] _timeSprite;

    // ����V�[���̏��.
    private HuntingSceneManager _huntingSceneManager;

    void Start()
    {
        _huntingSceneManager = GameObject.Find("GameManager").GetComponent<HuntingSceneManager>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
