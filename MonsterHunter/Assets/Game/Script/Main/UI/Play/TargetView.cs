/*�^�[�Q�b�g�A�C�R����`�悷�邩�ǂ���*/

using UnityEngine;

public class TargetView : MonoBehaviour
{
    // �J�����؂�ւ��̏��.
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
