/*ëIëUIÇÃèàóù*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUi : MonoBehaviour
{
    RectTransform _rectTransform;
    RectTransform _test;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _test = GameObject.Find("PressAnyBotton").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 pos = _rectTransform.position;
        //pos.x = 0;
        //pos.y = 0;
        _rectTransform.position = _test.position;
    }
}
