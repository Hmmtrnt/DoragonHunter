/*ターゲットアイコンを描画するかどうか*/

using UnityEngine;

public class TargetView : MonoBehaviour
{
    // カメラ切り替えの情報.
    public SwitchingCamera _switchCamera;
    public GameObject _targetIcon;

    void Update()
    {
        if (_switchCamera._switchCamera)
        {
            _targetIcon.SetActive(true);
        }
        else
        {
            _targetIcon.SetActive(false);
        }
    }
}
