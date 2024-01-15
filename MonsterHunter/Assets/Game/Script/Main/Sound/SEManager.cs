/*SEマネージャー*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    enum SE
    {
        Slash,// 斬撃音.
        RoundSlash,// 気刃大回転斬り.
        SENum// SE数.
    }

    // SE素材.
    public AudioClip[] _audio;

    public AudioClip _test;

    // 発音
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 音が鳴るかどうか確認.
        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    _audioSource.PlayOneShot(_test);
        //}
    }

    // SEを鳴らす.
    public void PlaySE(int SENunber)
    {
        _audioSource.PlayOneShot(_audio[SENunber]);
    }
}
