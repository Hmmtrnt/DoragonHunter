using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SESlider : MonoBehaviour
{
    private Image _image;
    // スライダー情報取得.
    private MainSceneOptionSelectUi _optionUI;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _optionUI = GameObject.Find("OptionSelectUI").GetComponent<MainSceneOptionSelectUi>();
    }

    private void FixedUpdate()
    {
        _image.fillAmount = _optionUI.GetVolumeSlider((int)MainSceneOptionSelectUi.SelectSlider.SE);
    }
}
