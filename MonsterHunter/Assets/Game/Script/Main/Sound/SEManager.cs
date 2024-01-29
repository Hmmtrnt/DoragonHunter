/*MainSceneSE�}�l�[�W���[*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    /*TitleScene*/
    public enum TitleSE
    {
        SELECT,// �I��UI�̈ړ�.
        DECISION,// ����.
        QUESTSTART,// �N�G�X�g�X�^�[�g.
        SENUM// SE��.
    }



    /*MainScene*/
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

    public enum AudioNumber
    {
        AUDIO2D,// �ǂ̋����ɂ��Ă���������.
        AUDIO3D,// �����ɉ����ĉ��ʕω�.
        AUDIONUM// AudioSource�̐�.
    }


    // SE�f��.
    /*TitleScene*/
    // UI.
    public AudioClip[] _titleUiAudio;
    /*MainScene*/
    // �n���^�[.
    public AudioClip[] _hunterAudio;
    // �����X�^�[.
    public AudioClip[] _monsterAudio;

    // ����
    //private AudioSource _hunterSE;
    //private AudioSource _monsterSE;

    // ����.
    private AudioSource[] _source = new AudioSource[2];

    void Start()
    {
        //_hunterSE = GameObject.Find("2DAudioSource").GetComponent<AudioSource>();
        //_monsterSE = GameObject.Find("3DAudioSource").GetComponent<AudioSource>();
        _source[0] = GameObject.Find("2DAudioSource").GetComponent<AudioSource>();
        _source[1] = GameObject.Find("3DAudioSource").GetComponent<AudioSource>();
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
    public void HunterPlaySE(int Audio, int SENunber)
    {
        _source[Audio].PlayOneShot(_hunterAudio[SENunber]);
    }
    // �����X�^�[��SE��炷.
    public void MonsterPlaySE(int Audio, int SENunber)
    {
        _source[Audio].PlayOneShot(_monsterAudio[SENunber]);
    }
}
