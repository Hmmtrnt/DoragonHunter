/*プレハブの自然消滅*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEraser : MonoBehaviour
{
    // 存在している時間.
    private int _currentTime = 0;

    private void Update()
    {
        _currentTime++;
        // ある程度時間がたつとエフェクトを消去.
        if(_currentTime >= 100)
        {
            Destroy(gameObject);
        }
    }
}
