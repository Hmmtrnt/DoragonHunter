using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneMenuUi : MonoBehaviour
{
    // �v���C���[���.
    private PlayerState _playerState;
    // ���j���[��ʂ̃I�u�W�F�N�g.
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
