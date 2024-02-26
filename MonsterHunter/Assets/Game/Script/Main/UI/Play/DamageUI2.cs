/*ダメージ表記*/

using System;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI2 : MonoBehaviour
{
    private Text _damageText;
    private PlayerState _player;

    private RectTransform _rectTransform;

    // フェードアウトするスピード.
    public float _fadeOutSpeed = 1f;
    // 上に移動地.
    public float _moveSpeed = 0.4f;
    // 一度処理を通したら通らないようにする.
    public bool _isProcess = false;

    void Start()
    {
        _damageText = GetComponentInChildren<Text>();
        _player = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _rectTransform = GetComponent<RectTransform>();
        _isProcess = false;
    }

    void Update()
    {
        GetDamage();

        //transform.LookAt(Camera.main.transform);
        //transform.position += Vector3.up * _moveSpeed * Time.deltaTime;

        Vector3 pos = Vector3.up * _moveSpeed * Time.deltaTime;

        _rectTransform.anchoredPosition = pos;

        _damageText.color =
            Color.Lerp(_damageText.color,
            new Color(1.0f, 200.0f / 255.0f, 0.0f, 0.0f),
            _fadeOutSpeed * Time.deltaTime);

        if (_damageText.color.a <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ダメージ量を取得.
    /// </summary>
    public void GetDamage()
    {
        if (!_isProcess)
        {
            _damageText.text = Convert.ToString((int)_player.GetHunterAttack());
            _isProcess = true;
        }
    }
}
