/*足煙の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSmoke : MonoBehaviour
{
    // 生存時間
    private int _countLife;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        _countLife++;

        if (_countLife == 200)
        {
            Destroy(gameObject);
        }
    }
}
