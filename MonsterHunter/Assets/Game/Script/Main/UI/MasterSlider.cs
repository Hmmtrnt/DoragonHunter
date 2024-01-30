using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterSlider : MonoBehaviour
{
    private Image _imageSlider;
    // �X���C�_�[���擾.
    private MainSceneOptionSelectUi _optionUI;
    // �A���J�[.
    private RectTransform _anchor;

    // Start is called before the first frame update
    void Start()
    {
        _imageSlider = GetComponent<Image>();
        _optionUI = GameObject.Find("OptionSelectUI").GetComponent<MainSceneOptionSelectUi>();
        _anchor = GameObject.Find("MasterButton").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        _imageSlider.fillAmount = _optionUI.GetVolumeSlider((int)MainSceneOptionSelectUi.SelectSlider.MASTER);

        // �T�C�g���Q�l.
        _anchor.anchorMin = new Vector2(_imageSlider.fillAmount, _anchor.anchorMin.y);
        _anchor.anchorMax = new Vector2(_imageSlider.fillAmount, _anchor.anchorMax.y);
        _anchor.anchoredPosition = Vector2.zero;
    }
}
