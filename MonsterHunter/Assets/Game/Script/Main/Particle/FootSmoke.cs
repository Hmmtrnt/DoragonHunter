/*‘«‰Œ‚Ìˆ—*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSmoke : MonoBehaviour
{
    // ¶‘¶ŠÔ
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
