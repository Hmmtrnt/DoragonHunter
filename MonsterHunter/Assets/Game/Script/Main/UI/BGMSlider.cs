using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{
    private Image _imageSlider;
    // �X���C�_�[���擾.
    private MainSceneOptionSelectUi _optionUI;
    // �A���J�[.
    private RectTransform _anchor;


    void Start()
    {
        _imageSlider = GetComponent<Image>();
        _optionUI = GameObject.Find("OptionSelectUI").GetComponent<MainSceneOptionSelectUi>();
        _anchor = GameObject.Find("BGMButton").GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        _imageSlider.fillAmount = _optionUI.GetVolumeSlider((int)MainSceneOptionSelectUi.SelectSlider.BGM);

        // �T�C�g���Q�l.
        _anchor.anchorMin = new Vector2(_imageSlider.fillAmount, _anchor.anchorMin.y);
        _anchor.anchorMax = new Vector2(_imageSlider.fillAmount, _anchor.anchorMax.y);
        _anchor.anchoredPosition = Vector2.zero;
    }
}
