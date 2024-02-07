/*タイムテーブルの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTable : MonoBehaviour
{
    // タイムの桁数.
    enum TimeDigit
    {
        // Sランク.
        S_MINUTE_TEN,   // 分.
        S_MINUTE_ONE,
        S_SECOND_TEN,   // 秒.
        S_SECOND_ONE,
        // Aランク.
        A_MINUTE_TEN,
        A_MINUTE_ONE,
        A_SECOND_TEN,
        A_SECOND_ONE,
        // Bランク.
        B_MINUTE_TEN,
        B_MINUTE_ONE,
        B_SECOND_TEN,
        B_SECOND_ONE,
        // Cランク.
        C_MINUTE_TEN,
        C_MINUTE_ONE,
        C_SECOND_TEN,
        C_SECOND_ONE,

        MAX_NUM         // 最大数.
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
