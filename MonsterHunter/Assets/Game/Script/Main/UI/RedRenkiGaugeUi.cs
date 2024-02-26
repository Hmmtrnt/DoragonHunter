/*—û‹CƒQ[ƒW*/
using UnityEngine;
using UnityEngine.UI;

public class RedRenkiGaugeUi : MonoBehaviour
{
    private PlayerState _playerState;
    private Image _gauge;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();

        _gauge = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        _gauge.fillAmount = _playerState.GetCurrentRedRenkiGauge() / _playerState.GetMaxRedRenkiGauge();
    }
}
