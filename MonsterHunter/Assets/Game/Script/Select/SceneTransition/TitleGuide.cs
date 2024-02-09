/*タイトル画面へ*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGuide : MonoBehaviour
{
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // ボタンガイド.
    public GameObject _guiedButton;
    // タイトル画面へ戻るUI.
    public GameObject _sceneTransition;

    // 範囲内にいるかどうか.
    private bool _collisionStay = false;
    // UIの開閉.
    private bool _UIOpenAndClose = false;

    // 閉じてからカウント開始.
    private int _closeCount = 0;

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

    // カウント開始.
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

    // UIの描画.
    private void UIDraw()
    {
        _guiedButton.SetActive(_collisionStay);
        _sceneTransition.SetActive(_UIOpenAndClose);
    }

    // タイトル画面へ戻るUIの開閉.
    private void SceneTransitionUIOpenAndClose()
    {
        if(!_collisionStay) { return; }

        if(_controllerManager._AButtonDown && !_UIOpenAndClose && _closeCount == 0)
        {
            _UIOpenAndClose = true;
        }
        else if(_controllerManager._BButtonDown)
        {
             _UIOpenAndClose= false;
        }

    }

    public void SetSceneTransitionUIOpen(bool flag) 
    { 
        _UIOpenAndClose = flag;
        _closeCount = 5;
    }

    public bool GetSceneTransitionUIOpen() { return _UIOpenAndClose; }
}
