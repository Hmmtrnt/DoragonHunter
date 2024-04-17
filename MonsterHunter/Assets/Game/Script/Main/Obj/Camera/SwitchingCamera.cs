/*カメラ切り替え*/

using UnityEngine;
using Cinemachine;

public class SwitchingCamera : MonoBehaviour
{
    // 回転するカメラ.
    [SerializeField] private CinemachineVirtualCameraBase _rotateCamera;
    // ターゲットカメラ.
    [SerializeField] private CinemachineVirtualCameraBase _targetCamera;
    // 入力情報.
    private ControllerManager _input;
    // カメラの切り替え情報.
    public bool _switchCamera = false;
    // SE.
    private SEManager _seManager;


    void Start()
    {
        _input = GetComponent<ControllerManager>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
    }

    void Update()
    {
        SwitchCamera();
        SwitchPriority();
    }

    // カメラの切り替え.
    private void SwitchCamera()
    {
        if (_input._RightStickButtonDown)
        {
            if (_switchCamera)
            {
                _switchCamera = false;
            }
            else
            {
                _switchCamera = true;
            }
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.SELECT);
        }
    }

    // カメラの切り替えたときの優先度変更.
    private void SwitchPriority()
    {
        if (_switchCamera)
        {
            _rotateCamera.Priority = 0;
            _targetCamera.Priority = 1;
        }
        else
        {
            _rotateCamera.Priority = 1;
            _targetCamera.Priority = 0;
        }
    }
}
