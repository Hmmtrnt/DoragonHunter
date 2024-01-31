/*クエストの時間の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestTime : MonoBehaviour
{
    // 時間.
    private float _timer = 0;

    void Start()
    {
        transform.localEulerAngles = Vector3.zero;
        _timer = 0;
        //Time.time = 0;
    }

    void Update()
    {
        _timer = Time.time;
        _timer = _timer / 60.0f;
        // 長針の処理.
        transform.localEulerAngles = new Vector3(0, 0, -360 / 60.0f * _timer);
        // 短針の固定化.
        GameObject.Find("HourHand").transform.localEulerAngles = new Vector3(0, 0, -360 / 60.0f * 50.0f);

        Debug.Log("time" + Time.time);
        //Debug.Log("Timer" + _timer);
    }
}
