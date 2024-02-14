/*プレイヤーのヒットストップ*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitStopManager : MonoBehaviour
{
    private Player _playerState;
    private Animator _anim;

    private void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<Player>();
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
        // プレイヤーのフレーム数の停止.
        _playerState._currentHitStop = true;
        // アニメーションの停止.
        _anim.speed = 0.0f;

        // 指定した時間まで停止.
        yield return new WaitForSecondsRealtime(HitStopTime);

        // プレイヤーのフレーム数の停止.
        _playerState._currentHitStop = false;
        // アニメーションの停止.
        _anim.speed = 1.0f;
    }
}
