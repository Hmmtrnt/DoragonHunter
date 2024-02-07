using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private Image _image;
    // フェードをしているかどうか.
    public bool _isFading = true;
    public bool _fadeEnd = false;

    // フェードの透明度.
    private byte _colorA = 0;

    // フェードインスピード.
    public byte _fadeInSpeed = 0;
    // フェードアウトスピード.
    public byte _fadeOutSpeed = 0;

    void Start()
    {
        _image = GetComponent<Image>();
        _isFading = true;

        _colorA = 255;

    }

    private void FixedUpdate()
    {
        FadeUpdate();
    }

    // フェード更新処理.
    private void FadeUpdate()
    {
        FadeColor();

        // フェードイン.
        if (_isFading)
        {
            FadeIn();
        }
        // フェードアウト.
        else
        {
            FadeOut();
        }
    }

    // フェードイン.
    private void FadeIn()
    {
        // 透明度が0の時はスキップ.
        if (_image.color.a <= 0.05f)
        {
            _colorA = 0;
            
            return;
        }
        _colorA -= _fadeInSpeed;
    }

    // フェードアウト.
    private void FadeOut()
    {
        if (_image.color.a >= 0.95f) 
        {
            _colorA = 255;
            _fadeEnd = true;
            return;
        }
        _colorA += _fadeOutSpeed;
    }

    // 現在のフェードの色.
    private void FadeColor()
    {
        _image.color = new Color32(0, 0, 0, _colorA);
    }

    // 透明度の取得.
    public byte GetAlpha() { return _colorA; }
}
