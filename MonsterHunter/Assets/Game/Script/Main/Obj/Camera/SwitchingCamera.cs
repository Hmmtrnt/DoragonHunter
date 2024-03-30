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
    private bool _switchCamera = false;

    void Start()
    {
        _input = GetComponent<ControllerManager>();
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
