using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float m = Time.time;
        m = m / 60.0f;
        transform.localEulerAngles = new Vector3(0, 0, -360 / 60.0f * m);
        GameObject.Find("HourHand").transform.localEulerAngles = new Vector3(0, 0, -360 / 60.0f * 50.0f);
    }
}
