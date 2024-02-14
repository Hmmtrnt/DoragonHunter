/*�v���C���[�̃q�b�g�X�g�b�v*/

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
    /// �q�b�g�X�g�b�v�J�n.
    /// </summary>
    /// <param name="HitStopTime">�q�b�g�X�g�b�v����</param>
    public void StartHitStop(float HitStopTime)
    {
        StartCoroutine(HitStopCoroutine(HitStopTime));
    }

    private IEnumerator HitStopCoroutine(float HitStopTime)
    {
        // �v���C���[�̃t���[�����̒�~.
        _playerState._currentHitStop = true;
        // �A�j���[�V�����̒�~.
        _anim.speed = 0.0f;

        // �w�肵�����Ԃ܂Œ�~.
        yield return new WaitForSecondsRealtime(HitStopTime);

        // �v���C���[�̃t���[�����̒�~.
        _playerState._currentHitStop = false;
        // �A�j���[�V�����̒�~.
        _anim.speed = 1.0f;
    }
}
