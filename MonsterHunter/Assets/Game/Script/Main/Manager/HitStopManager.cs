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
    /// �q�b�g�X�g�b�v�J�n.
    /// </summary>
    /// <param name="HitStopTime">�q�b�g�X�g�b�v����</param>
    public void StartHitStop(float HitStopTime)
    {
        StartCoroutine(HitStopCoroutine(HitStopTime));
    }

    private IEnumerator HitStopCoroutine(float HitStopTime)
    {
        // ���Ԓ�~�J�n.
        //Time.timeScale = 0f;
        //_anim.StopRecording();
        _playerState._currentHitStop = true;
        _anim.speed = 0.0f;

        // �w�肵�����Ԃ܂Œ�~.
        yield return new WaitForSecondsRealtime(HitStopTime);

        //���Ԓ�~�I��.
        //Time.timeScale = 1f;
        //_anim.StartPlayback();
        _playerState._currentHitStop = false;
        _anim.speed = 1.0f;
    }
}
