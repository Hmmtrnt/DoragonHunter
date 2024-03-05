/*MainSceneBGMマネージャー*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public enum BGM
    {
        TITLE,  // TitleScene.
        SELECT, // SelectScene.
        MAIN,   // MainScene.
        VICTORY,// Victory.
        FAILED, // Failed.
        BGMNum  // BGM数.
    }

    // BGM素材.
    [Header("BGM素材")]
    [SerializeField, EnumIndex(typeof(BGM))]
    public AudioClip[] _bgm;

    // 発音.
    private AudioSource _sourceBGM;

    // 一度処理が通ると以降通さない為.
    private bool _executeFlag = true;

    void Start()
    {
        _sourceBGM = GameObject.Find("GameManager").GetComponent<AudioSource>();
        _executeFlag = true;
    }

    private void FixedUpdate()
    {
        Debug.Assert(_sourceBGM != null);   
    }

    // BGMを変更.
    public void BGMChange(int BGMNunber)
    {
        if (!_executeFlag) return;

        _sourceBGM.clip = _bgm[BGMNunber];

        _sourceBGM.Play();
        _executeFlag = false;
    }

}
