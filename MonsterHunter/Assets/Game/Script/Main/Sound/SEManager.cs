/*SE�}�l�[�W���[*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    enum SE
    {
        Slash,// �a����.
        RoundSlash,// �C�n���]�a��.
        SENum// SE��.
    }

    // SE�f��.
    public AudioClip[] _audio;

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
        _audioSource.PlayOneShot(_audio[SENunber]);
    }
}
