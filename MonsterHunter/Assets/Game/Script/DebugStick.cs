using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStick : MonoBehaviour
{
    // �Q�[���p�b�h�R���g���[���[
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

        // �J�����̐���.
        Vector3 _cameraForward = Vector3.Scale(_camera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;

        /*�J�����̌�������ړ������擾*/
        // ����.
        Vector3 moveForward = _cameraForward * _input._LeftStickVertical;
        // ��.
        Vector3 moveSide = _camera.transform.right * _input._LeftStickHorizontal;
        // ���x�̑��.
        //Vector3 _moveVelocity = moveForward + moveSide;

        transform.position += new Vector3(moveForward.x, 0.0f, moveSide.z);

        //Vector3 targetPos = new Vector3(_input._LeftStickHorizontal, 0.0f, _input._LeftStickVertical);

        //transform.position = new Vector3(transform.position.x + _input._LeftStickHorizontal, transform.position.y, transform.position.z + _input._LeftStickVertical);

        transform.position = Vector3.MoveTowards(transform.position, _Hunter.transform.position, Time.deltaTime);
    }
}
