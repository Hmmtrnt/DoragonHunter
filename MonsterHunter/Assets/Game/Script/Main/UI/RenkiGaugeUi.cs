using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenkiGaugeUi : MonoBehaviour
{
    // �v���C���[���.
    private PlayerState _playerState;

    // �Q�[�W.
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
