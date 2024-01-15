/*ハンターの攻撃エフェクト*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHitEffect : MonoBehaviour
{
    // 存在している時間.
    private int _currentTime = 0;

    private void FixedUpdate()
    {
        _currentTime++;
        // ある程度時間がたつとエフェクトを消去.
        if(_currentTime >= 100)
        {
            Destroy(gameObject);
        }
    }
}
