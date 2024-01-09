using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopManager : MonoBehaviour
{
    // インスタンス.
    public static HitStopManager instance;

    void Start()
    {
        //instance = this;
        
        if(instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// ヒットストップ開始.
    /// </summary>
    /// <param name="HitStopTime">ヒットストップ時間</param>
    public void StartHitStop(float HitStopTime)
    {
        instance.StartCoroutine(instance.HitStopCoroutine(HitStopTime));
    }

    private IEnumerator HitStopCoroutine(float HitStopTime)
    {
        // 時間停止開始.
        Time.timeScale = 0f;

        // 指定した時間まで停止.
        yield return new WaitForSecondsRealtime(HitStopTime);

        //時間停止終了.
        Time.timeScale = 1f;
    }
}
