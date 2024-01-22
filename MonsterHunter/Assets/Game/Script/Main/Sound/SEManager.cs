/*SEƒ}ƒl[ƒWƒƒ[*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public enum HunterSE
    {
        SLASH,// aŒ‚‰¹.
        ROUNDSLASH,// ‹Cn‘å‰ñ“]a‚è.
        SENUM// SE”.
    }

    public enum MonsterSE
    {
        ROAR,// ™ôšK.
        SENUM// SE”.
    }


    // SE‘fŞ.
    public AudioClip[] _hunterAudio;

    public AudioClip _test;

    // ”­‰¹
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ‰¹‚ª–Â‚é‚©‚Ç‚¤‚©Šm”F.
        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    _audioSource.PlayOneShot(_test);
        //}
    }

    // SE‚ğ–Â‚ç‚·.
    public void PlaySE(int SENunber)
    {
        _audioSource.PlayOneShot(_hunterAudio[SENunber]);
    }
}
