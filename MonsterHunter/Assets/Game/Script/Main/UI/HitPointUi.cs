// Hpゲージ

using UnityEngine;
using UnityEngine.UI;

public class HitPointUi : MonoBehaviour
{
    // プレイヤー情報.
    private Player _playerState;

    // ゲージ.
    private Image _Gauge;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<Player>();

        _Gauge = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        _Gauge.fillAmount = _playerState.GetHitPoint() / _playerState.GetMaxHitPoint();
    }

}
