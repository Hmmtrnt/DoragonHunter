/*タイトル画面へ*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGuide : MonoBehaviour
{
    // 範囲内にいるかどうか.
    private bool _collisionStay = false;

    void Start()
    {
        _collisionStay=false;
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _collisionStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _collisionStay = false;
        }
    }
}
