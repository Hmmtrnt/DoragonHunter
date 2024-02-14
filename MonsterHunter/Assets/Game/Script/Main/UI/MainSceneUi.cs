/*�Q�[����ʑS�̂�UI�̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUi : MonoBehaviour
{
    private enum UIKinds
    {
        MENU,       // ���j���[.
        OPTION,     // �I�v�V����.
        RETIREMENT, // ���^�C�A�m�F���.
        PAUSE,      // �ꎞ��~�����.
        GAMEPLAY,   // �v���C���ɕ\�������UI. 
        UINUM,      // UI�̐�.
    }

    // �v���C���[���.
    private Player _playerState;
    // UI.
    public GameObject[] _ui;

    // �V�[���̏��.
    private MainSceneManager _mainSceneManager;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<Player>();
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _ui[(int)UIKinds.MENU].SetActive(_mainSceneManager.GetOpenMenu());
        _ui[(int)UIKinds.OPTION].SetActive(_mainSceneManager.GetOpenOption());
        _ui[(int)UIKinds.RETIREMENT].SetActive(_mainSceneManager.GetOpenRetireConfirmation());
        _ui[(int)UIKinds.PAUSE].SetActive(_mainSceneManager.GetOpenPause());
        _ui[(int)UIKinds.GAMEPLAY].SetActive(_mainSceneManager.GetGamePlayUI());
    }
}
