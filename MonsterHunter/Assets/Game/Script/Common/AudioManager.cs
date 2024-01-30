/*音量の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer _audioMixer;

    // 音量調節の為のUI.
    public Image _MasterSlider;
    public Image _BGMSlider;
    public Image _SESlider;

    private float _MasterVolume = 0;
    private float _BGMVolume = 0;
    private float _SEVolume = 0;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        VolumeAssignment();
        _audioMixer.SetFloat("Master", _MasterVolume);
        _audioMixer.SetFloat("BGM", _BGMVolume);
        _audioMixer.SetFloat("SE", _SEVolume);
    }

    // ボリュームの代入.
    private void VolumeAssignment()
    {
        _MasterVolume = _MasterSlider.fillAmount * 80 - 80;
        _BGMVolume = _BGMSlider.fillAmount *  80 - 80;
        _SEVolume = _SESlider.fillAmount * 80 - 80;
    }
}
