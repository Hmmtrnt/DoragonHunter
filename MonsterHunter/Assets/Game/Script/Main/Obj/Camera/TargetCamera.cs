/*�^�[�Q�b�g�J�����̏���*/

using System;
using UnityEngine;
using Cinemachine;

public class TargetCamera : MonoBehaviour
{
    // �J�����ŒǏ]����Ώۂ̏��.
    [Serializable] private struct TargetInfo
    {
        // �Ǐ]�Ώ�.
        [Header("�Ǐ]�Ώ�")]
        public Transform _follow;
        // �W�������킹��Ώ�.
        [Header("�W�������킹��Ώ�")]
        public Transform _lookAt;
    }

    // ���͏��.
    private ControllerManager _input;
    // �J����.
    private CinemachineVirtualCamera _virtualCamera;
    // �Ǐ]�Ώۂ̃��X�g.
    [Header("�Ǐ]�Ώۂ̃��X�g")]
    [SerializeField] private TargetInfo[] _targetList;

    // �I�������^�[�Q�b�g�̔z��.
    private int _currentTarget = 0;

    void Start()
    {
        _input = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // �Ǐ]�Ώۂ̏�񂪂Ȃ��ꍇ�̉������Ȃ�.
        if(_targetList == null || _targetList.Length <= 0) return;

        //Debug.Log(_currentTarget);

        // �J�����̒Ǐ]�Ώۂ̕ύX.
        if (_input._RightStickButtonDown)
        {
            // �Ǐ]�Ώۂ̐؂�ւ�.
            if(++_currentTarget >= _targetList.Length)
            {
                //Debug.Log("�ʂ�");
                _currentTarget = 0;
            }

            // �Ǐ]�Ώۂ̍X�V.
            _virtualCamera.Follow = _targetList[_currentTarget]._follow;
            _virtualCamera.LookAt = _targetList[_currentTarget]._lookAt;

            //_virtualCamera.
            
        }

        
    }
}
