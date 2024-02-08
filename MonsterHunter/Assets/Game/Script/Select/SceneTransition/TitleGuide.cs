/*�^�C�g����ʂ�*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGuide : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // �{�^���K�C�h.
    public GameObject _guiedButton;
    // �^�C�g����ʂ֖߂�UI.
    public GameObject _sceneTransition;

    // �͈͓��ɂ��邩�ǂ���.
    private bool _collisionStay = false;
    // UI�̊J��.
    private bool _UIOpenAndClose = false;

    void Start()
    {
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _collisionStay = false;
        _UIOpenAndClose = false;
        _guiedButton.SetActive(_collisionStay);
        _sceneTransition.SetActive(_UIOpenAndClose);
    }

    private void Update()
    {
        SceneTransitionUIOpenAndClose();
    }

    private void FixedUpdate()
    {
        UIDraw();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _collisionStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _collisionStay = false;
        }
    }

    // UI�̕`��.
    private void UIDraw()
    {
        _guiedButton.SetActive(_collisionStay);
        _sceneTransition.SetActive(_UIOpenAndClose);
    }

    // �^�C�g����ʂ֖߂�UI�̊J��.
    private void SceneTransitionUIOpenAndClose()
    {
        if(!_collisionStay) { return; }

        if(_controllerManager._AButtonDown)
        {
            _UIOpenAndClose = true;
        }
        else if(_controllerManager._BButtonDown)
        {
             _UIOpenAndClose= false;
        }

    }

    public void SetSceneTransitionUIOpen(bool flag) { _UIOpenAndClose = flag; }

    public bool GetSceneTransitionUIOpen() { return _UIOpenAndClose; }
}
