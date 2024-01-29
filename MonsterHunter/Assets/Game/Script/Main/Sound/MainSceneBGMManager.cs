/*MainSceneBGMマネージャー*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneBGMManager : MonoBehaviour
{
    enum BGM
    {
        MAIN,// MainSceneBGM.
        BGMNum// BGM数.
    }

    // BGM素材.
    public AudioClip[] _bgm;

    // 発音.
    private AudioSource _sourceBGM;

    void Start()
    {
        _sourceBGM = GameObject.Find("GameManager").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Debug.Assert(_sourceBGM != null);   
    }

    // BGMを流している.
    public void BGMLoopPlay()
    {
        _sourceBGM.Play();
    }

}
