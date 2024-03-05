/*MainSceneBGM�}�l�[�W���[*/

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
        BGMNum  // BGM��.
    }

    // BGM�f��.
    [Header("BGM�f��")]
    [SerializeField, EnumIndex(typeof(BGM))]
    public AudioClip[] _bgm;

    // ����.
    private AudioSource _sourceBGM;

    // ��x�������ʂ�ƈȍ~�ʂ��Ȃ���.
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

    // BGM��ύX.
    public void BGMChange(int BGMNunber)
    {
        if (!_executeFlag) return;

        _sourceBGM.clip = _bgm[BGMNunber];

        _sourceBGM.Play();
        _executeFlag = false;
    }

}
