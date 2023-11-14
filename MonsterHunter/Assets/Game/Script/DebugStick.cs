using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStick : MonoBehaviour
{
    // ゲームパッドコントローラー
    private ControllerManager _input;
    private GameObject _Hunter;



    // Start is called before the first frame update
    void Start()
    {
        _input = GameObject.FindWithTag("Manager").GetComponent<ControllerManager>();
        _Hunter = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Camera _camera = GameObject.Find("Camera").GetComponent<Camera>();

        Vector3 _moveDirection = new Vector3(_input._LeftStickHorizontal, 0.0f, _input._LeftStickVertical);
        _moveDirection.Normalize();

        // カメラの正面.
        Vector3 _cameraForward = Vector3.Scale(_camera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;

        /*カメラの向きから移動方向取得*/
        // 正面.
        Vector3 moveForward = _cameraForward * _input._LeftStickVertical;
        // 横.
        Vector3 moveSide = _camera.transform.right * _input._LeftStickHorizontal;
        // 速度の代入.
        //Vector3 _moveVelocity = moveForward + moveSide;

        transform.position += new Vector3(moveForward.x, 0.0f, moveSide.z);

        //Vector3 targetPos = new Vector3(_input._LeftStickHorizontal, 0.0f, _input._LeftStickVertical);

        //transform.position = new Vector3(transform.position.x + _input._LeftStickHorizontal, transform.position.y, transform.position.z + _input._LeftStickVertical);

        transform.position = Vector3.MoveTowards(transform.position, _Hunter.transform.position, Time.deltaTime);
    }
}
