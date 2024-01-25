/*一時停止した時にゲーム全体の時間をストップ*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTimeStop : MonoBehaviour
{
    void Start()
    {
        
    }

    // 一時停止させる.
    public void StopTime()
    {
        Time.timeScale = 0.0f;
    }

    // 一時停止を解除.
    public void StartTime()
    {
        Time.timeScale = 1.0f;
    }
    
}
