/*タイムテーブルの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTable : MonoBehaviour
{
    // タイムの桁数.
    enum TimeDigit
    {
        // Sランク.
        S_MINUTE_TEN,   // 十分.
        S_MINUTE_ONE,   // 一分.
        // Aランク.
        A_MINUTE_TEN,
        A_MINUTE_ONE,
        // Bランク.
        B_MINUTE_TEN,
        B_MINUTE_ONE,
        // Cランク.
        C_MINUTE_TEN,
        C_MINUTE_ONE,

        MAX_NUM         // 最大数.
    }

    // タイムの画像
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

        MAX_NUM // 最大数.
    }

    // ランク表の時間.
    public GameObject[] _timeUi;
    // スプライト変更のために取得.
    private Image[] _timeImage;
    // ランク表の画像.
    public Sprite[] _timeSprite;

    // 狩猟中シーンの情報.
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
