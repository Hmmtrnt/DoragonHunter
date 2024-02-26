/*�A�C�e���̐���\��*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ItemNum : MonoBehaviour
{
    enum ItemNumber
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

    // ����\���摜.
    public GameObject[] _number;
    // ���̉摜�̍��W.
    private RectTransform[] _rectTransform = new RectTransform[(int)ItemNumber.MAXNUM];

    // �\��������摜.
    private bool[] _disPlayNunber = new bool[(int)ItemNumber.MAXNUM];

    // �񕜖�̐��̏��擾.
    private PlayerState _playerState;

    void Start()
    {
        for(int Number = 0; Number < (int)ItemNumber.MAXNUM; Number++)
        {
            _rectTransform[Number] = _number[Number].GetComponent<RectTransform>();
        }
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        for(int Number = 0; Number < (int)ItemNumber.MAXNUM; Number++)
        {
            _disPlayNunber[Number] = false;
            _number[Number].SetActive(_disPlayNunber[Number]);
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        NumberDraw();
        NumberActive();
    }

    // ���̕`��
    private void NumberDraw()
    {
        for(int Number = 0; Number < (int)ItemNumber.MAXNUM; Number++)
        {
            _number[Number].SetActive(_disPlayNunber[Number]);
        }
    }

    // ���ɂ���ĕ`�悷��摜�̕ύX.
    private void NumberActive()
    {
        // ���ɂ���ĕ�����ύX.
        for(int ItemNum = 0; ItemNum < (int)ItemNumber.MAXNUM ; ItemNum++)
        {
            if(_playerState.GetCureMedicineNum() == ItemNum)
            {
                _disPlayNunber[ItemNum] = true;

            }
            else
            {
                _disPlayNunber[ItemNum] = false;
            }
        }

        // �A�C�e������10�̎����̈ʒu�����炵�ĕ`��B
        if(_playerState.GetCureMedicineNum() == (int)ItemNumber.MAXNUM)
        {
            _disPlayNunber[(int)ItemNumber.ZERO] = true;
            _disPlayNunber[(int)ItemNumber.ONE] = true;

            _rectTransform[(int)ItemNumber.ZERO].anchoredPosition = new Vector2(5.5f, 0.0f);
            _rectTransform[(int)ItemNumber.ONE].anchoredPosition = new Vector2(-7.2f, 0.0f);
        }
        else
        {
            _rectTransform[(int)ItemNumber.ZERO].anchoredPosition = new Vector2(0.0f, 0.0f);
            _rectTransform[(int)ItemNumber.ONE].anchoredPosition = new Vector2(-2.0f, 0.0f);
        }


    }
}
