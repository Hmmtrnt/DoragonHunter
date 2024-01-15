using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopManager : MonoBehaviour
{
    private PlayerState _playerState;
    private Animator _anim;

    private void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _anim = GameObject.Find("Hunter").GetComponent<Animator>();
    }

    /// <summary>
    /// ヒットストップ開始.
    /// </summary>
    /// <param name="HitStopTime">ヒットストップ時間</param>
    public void StartHitStop(float HitStopTime)
    {
        StartCoroutine(HitStopCoroutine(HitStopTime));
    }

    private IEnumerator HitStopCoroutine(float HitStopTime)
    {
        // 時間停止開始.
        //Time.timeScale = 0f;
        //_anim.StopRecording();
        _playerState._currentHitStop = true;
        _anim.speed = 0.0f;

        // 指定した時間まで停止.
        yield return new WaitForSecondsRealtime(HitStopTime);

        //時間停止終了.
        //Time.timeScale = 1f;
        //_anim.StartPlayback();
        _playerState._currentHitStop = false;
        _anim.speed = 1.0f;
    }
}
