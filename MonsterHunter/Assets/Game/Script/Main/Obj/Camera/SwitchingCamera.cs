/*�J�����؂�ւ�*/

using UnityEngine;
using Cinemachine;

public class SwitchingCamera : MonoBehaviour
{
    // ��]����J����.
    [SerializeField] private CinemachineVirtualCameraBase _rotateCamera;
    // �^�[�Q�b�g�J����.
    [SerializeField] private CinemachineVirtualCameraBase _targetCamera;
    // ���͏��.
    private ControllerManager _input;
    // �J�����̐؂�ւ����.
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

    // �J�����̐؂�ւ�.
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

    // �J�����̐؂�ւ����Ƃ��̗D��x�ύX.
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
