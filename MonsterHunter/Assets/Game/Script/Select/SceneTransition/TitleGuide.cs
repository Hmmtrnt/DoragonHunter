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
    // ���ʉ�.
    private SEManager _seManager;
    // �t�F�[�h.
    private Fade _fade;

    // �͈͓��ɂ��邩�ǂ���.
    private bool _collisionStay = false;
    // UI�̊J��.
    private bool _UIOpenAndClose = false;

    // ���Ă���J�E���g�J�n.
    private int _closeCount = 0;

    void Start()
    {
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _collisionStay = false;
        _UIOpenAndClose = false;
        _guiedButton.SetActive(_collisionStay);
        _sceneTransition.SetActive(_UIOpenAndClose);
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
    }

    private void Update()
    {
        SceneTransitionUIOpenAndClose();
    }

    private void FixedUpdate()
    {
        UIDraw();
        CountDown();
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

    // �J�E���g�J�n.
    private void CountDown()
    {
        if (_closeCount > 0) 
        {
            _closeCount--;
        }
        if(_closeCount <= 0 )
        {
            _closeCount=0;
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

        if(_controllerManager._AButtonDown && !_UIOpenAndClose && _closeCount == 0)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
            _UIOpenAndClose = true;
        }
        else if(_controllerManager._BButtonDown && _fade._isFading)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
            _UIOpenAndClose = false;
        }

    }

    public void SetSceneTransitionUIOpen(bool flag) 
    { 
        _UIOpenAndClose = flag;
        _closeCount = 5;
    }

    public bool GetSceneTransitionUIOpen() { return _UIOpenAndClose; }
}
