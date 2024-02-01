/*アイテムの数を表示*/

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

    // 数を表す画像
    public GameObject[] _number;
    // 表示させる画像.
    private bool[] _disPlayNunber = new bool[(int)ItemNumber.MAXNUM];

    // 回復薬の数の情報取得.
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

    // 数によって描画する画像の変更.
    private void 
}
