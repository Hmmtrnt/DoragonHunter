using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenkiGaugeUi : MonoBehaviour
{
    // プレイヤー情報.
    private PlayerState _playerState;

    // ゲージ.
    private Image _Gauge;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();

        _Gauge = GetComponent<Image>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _Gauge.fillAmount = _playerState.GetCurrentRenkiGauge() / _playerState.GetMaxRenkiGauge();
    }
}
