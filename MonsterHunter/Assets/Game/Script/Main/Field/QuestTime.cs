/*クエストの時間の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestTime : MonoBehaviour
{
    // 時間.
    private float _timer = 0;

    // 秒.
    private float _second = 0;
    // 分.
    private float _minutes = 0;


    void Start()
    {
        transform.localEulerAngles = Vector3.zero;
        _timer = 0;
        //Time.time = 0;
    }

    void Update()
    {
        Timing();
        //_timer += Time.deltaTime;
        //Debug.Log(_timer);
        //_timer = _timer / 60.0f;
        // 長針の処理.
        transform.localEulerAngles = new Vector3(0, 0, -360 / 60.0f * _minutes);
        // 短針の固定化.
        GameObject.Find("HourHand").transform.localEulerAngles = new Vector3(0, 0, -360 / 60.0f * 50.0f);

        //Debug.Log("time" + Time.time);
        //Debug.Log("Timer" + _timer);
    }

    private void FixedUpdate()
    {
        Debug.Log(_minutes + ":" +  _second);
    }

    // クエスト時間を計測.
    private void Timing()
    {
        _second += Time.deltaTime;

        if(_second >= 60)
        {
            _minutes++;
            _second = 0;
        }
    }

    public int GetSecond() { return (int)_second; }

    public int GetMinutes() { return (int)_minutes; }

}
