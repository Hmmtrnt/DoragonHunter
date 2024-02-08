/*ƒ^ƒCƒgƒ‹‰æ–Ê‚Ö*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGuide : MonoBehaviour
{
    // ”ÍˆÍ“à‚É‚¢‚é‚©‚Ç‚¤‚©.
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
