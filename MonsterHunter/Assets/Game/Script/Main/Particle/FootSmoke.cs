/*足煙の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSmoke : MonoBehaviour
{
    // 生存時間
    private int _countLife;

    private void FixedUpdate()
    {
        _countLife++;

        // パーティクルを消去する.
        if (_countLife == 50)
        {
            Destroy(gameObject);
        }
    }
}
