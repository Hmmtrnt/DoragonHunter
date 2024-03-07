/*‰¹—Ê‚Ìˆ—*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer _audioMixer;

    // ‰¹—Ê’²ß‚Ìˆ×‚ÌUI.
    public Image _MasterSlider;
    public Image _BGMSlider;
    public Image _SESlider;

    private float _MasterVolume = 0;
    private float _BGMVolume = 0;
    private float _SEVolume = 0;

    private void FixedUpdate()
    {
        VolumeAssignment();
        _audioMixer.SetFloat("Master", _MasterVolume);
        _audioMixer.SetFloat("BGM", _BGMVolume);
        _audioMixer.SetFloat("SE", _SEVolume);
    }

    // ƒ{ƒŠƒ…[ƒ€‚Ì‘ã“ü.
    private void VolumeAssignment()
    {
        _MasterVolume = _MasterSlider.fillAmount * 80 - 80;
        _BGMVolume = _BGMSlider.fillAmount *  80 - 80;
        _SEVolume = _SESlider.fillAmount * 80 - 80;
    }
}
