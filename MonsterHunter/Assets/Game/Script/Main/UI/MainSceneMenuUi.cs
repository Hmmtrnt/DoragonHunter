/*���j���[��ʂ̕\����\��*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneMenuUi : MonoBehaviour
{
    // �v���C���[���.
    private PlayerState _playerState;
    // MenuUI.
    private GameObject _menuUI;
    // OptionUI.
    private GameObject _optionMenuUI;
    // �ꎞ��~����UI
    private GameObject _pauseKeepUI;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _menuUI = GameObject.Find("Menu").gameObject;
        _optionMenuUI = GameObject.Find("OptionMenu").gameObject;
        _pauseKeepUI = GameObject.Find("PauseCanvas").gameObject;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _menuUI.SetActive(_playerState.GetOpenMenu());
        _optionMenuUI.SetActive(_playerState.GetOpenOption());
    }
}
