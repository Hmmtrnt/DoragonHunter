/*�A�C�e���̐���\��*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ItemNum : MonoBehaviour
{
    enum ItemNumber
    {
        ONE,
        TWO, 
        THREE, 
        FOUR, 
        FIVE, 
        SIX, 
        SEVEN, 
        EIGHT, 
        NINE, 
        ZERO, 
        MAXNUM
    }

    // ����\���摜
    public GameObject[] _number;
    // �\��������摜.
    private bool[] _disPlayNunber = new bool[(int)ItemNumber.MAXNUM];

    // �񕜖�̐��̏��擾.
    private PlayerState _playerState;

    void Start()
    {
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
        
    }

    // ���ɂ���ĕ`�悷��摜�̕ύX.
    private void 
}
