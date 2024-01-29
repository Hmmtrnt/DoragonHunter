/*MainSceneBGM�}�l�[�W���[*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneBGMManager : MonoBehaviour
{
    enum BGM
    {
        MAIN,// MainSceneBGM.
        BGMNum// BGM��.
    }

    // BGM�f��.
    public AudioClip[] _bgm;

    // ����.
    private AudioSource _sourceBGM;

    void Start()
    {
        _sourceBGM = GameObject.Find("GameManager").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Debug.Assert(_sourceBGM != null);   
    }

    // BGM�𗬂��Ă���.
    public void BGMLoopPlay()
    {
        _sourceBGM.Play();
    }

}
