/*アイテムの数を表示*/

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

    // 数を表す画像.
    public GameObject[] _number;
    // 数の画像の座標.
    private RectTransform[] _rectTransform = new RectTransform[(int)ItemNumber.MAXNUM];

    // 表示させる画像.
    private bool[] _disPlayNunber = new bool[(int)ItemNumber.MAXNUM];

    // 回復薬の数の情報取得.
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

    // 数の描画
    private void NumberDraw()
    {
        for(int Number = 0; Number < (int)ItemNumber.MAXNUM; Number++)
        {
            _number[Number].SetActive(_disPlayNunber[Number]);
        }
    }

    // 数によって描画する画像の変更.
    private void NumberActive()
    {
        // 数によって文字を変更.
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

        // アイテム数が10個の時数の位置をずらして描画。
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
