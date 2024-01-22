/*SE�}�l�[�W���[*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public enum HunterSE
    {
        SLASH,// �a����.
        ROUNDSLASH,// �C�n���]�a��.
        SENUM// SE��.
    }

    public enum MonsterSE
    {
        ROAR,// ���K.
        SENUM// SE��.
    }


    // SE�f��.
    public AudioClip[] _hunterAudio;

    public AudioClip _test;

    // ����
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // �����邩�ǂ����m�F.
        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    _audioSource.PlayOneShot(_test);
        //}
    }

    // SE��炷.
    public void PlaySE(int SENunber)
    {
        _audioSource.PlayOneShot(_hunterAudio[SENunber]);
    }
}
