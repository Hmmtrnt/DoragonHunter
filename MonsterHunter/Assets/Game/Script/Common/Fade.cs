/*�t�F�[�h����*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private Image _image;
    // �t�F�[�h�����Ă��邩�ǂ���.
    public bool _isFading = true;
    public bool _fadeEnd = false;

    // �t�F�[�h�̓����x.
    private byte _colorA = 0;

    // �t�F�[�h�C���X�s�[�h.
    public byte _fadeInSpeed = 0;
    // �t�F�[�h�C���I��.
    private float _fadeInFinish = 0.05f;
    // �t�F�[�h�A�E�g�X�s�[�h.
    public byte _fadeOutSpeed = 0;
    // �t�F�[�h�A�E�g�I��.
    private float _fadeOutFinish = 0.95f;

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

    // �t�F�[�h�X�V����.
    private void FadeUpdate()
    {
        FadeColor();

        // �t�F�[�h�C��.
        if (_isFading)
        {
            FadeIn();
        }
        // �t�F�[�h�A�E�g.
        else
        {
            FadeOut();
        }
    }

    // �t�F�[�h�C��.
    private void FadeIn()
    {
        // �����x��0�̎��̓X�L�b�v.
        if (_image.color.a <= _fadeInFinish)
        {
            _colorA = 0;
            
            return;
        }
        _colorA -= _fadeInSpeed;
    }

    // �t�F�[�h�A�E�g.
    private void FadeOut()
    {
        if (_image.color.a >= _fadeOutFinish) 
        {
            _colorA = 255;
            _fadeEnd = true;
            return;
        }
        _colorA += _fadeOutSpeed;
    }

    // ���݂̃t�F�[�h�̐F.
    private void FadeColor()
    {
        _image.color = new Color32(0, 0, 0, _colorA);
    }
}
