/*SE�}�l�[�W���[*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public enum HunterSE
    {
        SLASH,          // �a����.
        ROUNDSLASH,     // �C�n���]�a��.
        DAMAGE,         // �_���[�W.
        DRAWSWORD,      // ����.
        SHEATHINGSWORD, // �[��.
        MISSINGSLASH,   // ��U��
        SENUM           // SE��.
    }

    public enum MonsterSE
    {
        ROAR,       // ���K.
        FOOTSTEP,   // ����.
        SENUM       // SE��.
    }


    // SE�f��.
    // �n���^�[.
    public AudioClip[] _hunterAudio;
    // �����X�^�[.
    public AudioClip[] _monsterAudio;

    // ����
    private AudioSource _hunterSE;
    private AudioSource _monsterSE;

    void Start()
    {
        _hunterSE = GameObject.Find("HunterAudio").GetComponent<AudioSource>();
        _monsterSE = GameObject.Find("Dragon").GetComponent<AudioSource>();
    }

    void Update()
    {
        // �����邩�ǂ����m�F.
        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    _audioSource.PlayOneShot(_test);
        //}
    }

    // �v���C���[��SE��炷.
    public void HunterPlaySE(int SENunber)
    {
        _hunterSE.PlayOneShot(_hunterAudio[SENunber]);
    }
    // �����X�^�[��SE��炷.
    public void MonsterPlaySE(int SENunber)
    {
        _monsterSE.PlayOneShot(_monsterAudio[SENunber]);
    }
}
