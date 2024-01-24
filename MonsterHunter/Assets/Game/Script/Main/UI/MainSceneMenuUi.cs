using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneMenuUi : MonoBehaviour
{
    // プレイヤー情報.
    private PlayerState _playerState;
    // メニュー画面のオブジェクト.
    private GameObject _menuCanvas;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _menuCanvas = GameObject.Find("Menu").gameObject;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _menuCanvas.SetActive(_playerState.GetOpenMenu());
    }
}
