/*SEマネージャー*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public enum HunterSE
    {
        SLASH,// 斬撃音.
        ROUNDSLASH,// 気刃大回転斬り.
        SENUM// SE数.
    }

    public enum MonsterSE
    {
        ROAR,// 咆哮.
        SENUM// SE数.
    }


    // SE素材.
    public AudioClip[] _hunterAudio;

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
        _audioSource.PlayOneShot(_hunterAudio[SENunber]);
    }
}
