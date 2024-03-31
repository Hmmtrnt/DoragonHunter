/*MainSceneSEマネージャー*/

using UnityEngine;

public class SEManager : MonoBehaviour
{
    /*UI*/
    public enum UISE
    {
        SELECT,         // 選択UIの移動.
        DECISION,       // 決定.
        REMOVE_PUSH,    // 戻る.
        QUEST_START,    // クエストスタート.
        STAMP_PUSH,     // スタンプを押す.
        QUEST_LIST,     // クエストリストの音.
        SENUM           // SE数.
    }



    /*MainScene*/
    public enum HunterSE
    {
        SLASH,              // 斬撃音.
        ROUNDSLASH,         // 気刃大回転斬り.
        DAMAGE,             // ダメージ.
        DRAWSWORD,          // 抜刀.
        SHEATHINGSWORD,     // 納刀.
        MISSINGSLASH,       // 空振り
        MISSINGROUNDSLASH,  // 空振り(回転斬り).
        DRINK,              // 飲む.
        FOOTSTEPLEFT,       // 足音(左).
        FOOTSTEPRIGHT,      // 足音(右).
        BOUNCE,             // 弾かれた音
        SUCCESSCOUNTER, // カウンター成功時の音.

        SENUM               // SE数.
    }

    public enum MonsterSE
    {
        ROAR,           // 咆哮.
        FOOTSTEP,       // 足音.
        FOOTSMALLSTEP,  // 小さい足音.
        ROTATE,         // 回転時の鳴き声.
        FALTER,         // 怯む声.
        BITE,           // 噛みつく時の鳴き声
        BLESS,          // ブレス.
        GROAN,          // 呻く.
        DOWN,           // 通れた時の声.

        SENUM           // SE数.
    }

    public enum AudioNumber
    {
        AUDIO2D,// どの距離にいても同じ音量.
        AUDIO3D,// 距離に応じて音量変化.

        AUDIONUM// AudioSourceの数.
    }


    // SE素材.
    // UI.
    [Header("UIの効果音")]
    [SerializeField, EnumIndex(typeof(UISE))]
    public AudioClip[] _UiAudio;
    // ハンター.
    [Header("ハンターの効果音")]
    [SerializeField, EnumIndex(typeof(HunterSE))]
    public AudioClip[] _hunterAudio;
    // モンスター.
    [Header("モンスターの効果音")]
    [SerializeField, EnumIndex(typeof(MonsterSE))]
    public AudioClip[] _monsterAudio;

    // 発音.
    private AudioSource[] _source = new AudioSource[(int)AudioNumber.AUDIONUM];

    void Start()
    {
        _source[0] = GameObject.Find("2DAudioSource").GetComponent<AudioSource>();
        _source[1] = GameObject.Find("3DAudioSource").GetComponent<AudioSource>();
    }

    void Update()
    {
        // 音が鳴るかどうか確認.
        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    _audioSource.PlayOneShot(_medicineConsume);
        //}
    }

    // UIのSEを鳴らす.
    public void UIPlaySE(int Audio, int SENuber)
    {
        _source[Audio].PlayOneShot(_UiAudio[SENuber]);
    }

    // プレイヤーのSEを鳴らす.
    public void HunterPlaySE(int Audio, int SENunber)
    {
        _source[Audio].PlayOneShot(_hunterAudio[SENunber]);
    }
    // モンスターのSEを鳴らす.
    public void MonsterPlaySE(int Audio, int SENunber)
    {
        _source[Audio].PlayOneShot(_monsterAudio[SENunber]);
    }
}
