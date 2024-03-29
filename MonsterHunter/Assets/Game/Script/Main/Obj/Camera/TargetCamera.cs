/*ターゲットカメラの処理*/

using System;
using UnityEngine;
using Cinemachine;

public class TargetCamera : MonoBehaviour
{
    // カメラで追従する対象の情報.
    [Serializable] private struct TargetInfo
    {
        // 追従対象.
        [Header("追従対象")]
        public Transform _follow;
        // 標準を合わせる対象.
        [Header("標準を合わせる対象")]
        public Transform _lookAt;
    }

    // 入力情報.
    private ControllerManager _input;
    // カメラ.
    private CinemachineVirtualCamera _virtualCamera;
    // 追従対象のリスト.
    [Header("追従対象のリスト")]
    [SerializeField] private TargetInfo[] _targetList;

    // 選択されるターゲットの配列.
    private int _currentTarget = 0;

    void Start()
    {
        _input = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // 追従対象の情報がない場合の何もしない.
        if(_targetList == null || _targetList.Length <= 0) return;

        //Debug.Log(_currentTarget);

        // カメラの追従対象の変更.
        if (_input._RightStickButtonDown)
        {
            // 追従対象の切り替え.
            if(++_currentTarget >= _targetList.Length)
            {
                //Debug.Log("通る");
                _currentTarget = 0;
            }

            // 追従対象の更新.
            _virtualCamera.Follow = _targetList[_currentTarget]._follow;
            _virtualCamera.LookAt = _targetList[_currentTarget]._lookAt;

            //_virtualCamera.
            
        }

        
    }
}
