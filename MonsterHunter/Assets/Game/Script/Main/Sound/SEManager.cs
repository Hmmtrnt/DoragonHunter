/*SEマネージャー*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public enum HunterSE
    {
        SLASH,          // 斬撃音.
        ROUNDSLASH,     // 気刃大回転斬り.
        DAMAGE,         // ダメージ.
        DRAWSWORD,      // 抜刀.
        SHEATHINGSWORD, // 納刀.
        MISSINGSLASH,   // 空振り
        SENUM           // SE数.
    }

    public enum MonsterSE
    {
        ROAR,       // 咆哮.
        FOOTSTEP,   // 足音.
        SENUM       // SE数.
    }


    // SE素材.
    // ハンター.
    public AudioClip[] _hunterAudio;
    // モンスター.
    public AudioClip[] _monsterAudio;

    // 発音
    private AudioSource _hunterSE;
    private AudioSource _monsterSE;

    void Start()
    {
        _hunterSE = GameObject.Find("HunterAudio").GetComponent<AudioSource>();
        _monsterSE = GameObject.Find("Dragon").GetComponent<AudioSource>();
    }

    void Update()
    {
        // 音が鳴るかどうか確認.
        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    _audioSource.PlayOneShot(_test);
        //}
    }

    // プレイヤーのSEを鳴らす.
    public void HunterPlaySE(int SENunber)
    {
        _hunterSE.PlayOneShot(_hunterAudio[SENunber]);
    }
    // モンスターのSEを鳴らす.
    public void MonsterPlaySE(int SENunber)
    {
        _monsterSE.PlayOneShot(_monsterAudio[SENunber]);
    }
}
