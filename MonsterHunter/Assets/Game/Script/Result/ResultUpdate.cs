/*���U���g���ʂ̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    enum ClearTimeDigit
    {
        MINUTE_TEN, // 10��.
        MINUTE_ONE, // 1��.
        SECOND_TEN, // 10�b.
        SECOND_ONE, // 1�b.
    }


    enum SpriteNumber
    {
        // ���̃X�v���C�g.
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
    // ���U���g��ʂ�UI.
    public GameObject[] _ui;
    // �N���A�^�C����UI.
    public GameObject[] _clearTime;
    // �N�G�X�g�^�C���̎��Ԃ̃X�v���C�g.
    public Sprite[] _timeNum;

    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    // �^�C���ɂ���Čv�����Ԃ̃X�v���C�g��ύX.
    private void SpriteTimeChange()
    {
        
    }
}
