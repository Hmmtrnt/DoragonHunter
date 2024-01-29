/*�Q�[����ʑS�̂�UI�̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUi : MonoBehaviour
{
    private enum UIKinds
    {
        MENU,// ���j���[.
        OPTION,// �I�v�V����.
        CLEAR,// �N���A.
        FAILED,// ���s.
        UINUM,// UI�̐�.
    }

    // �v���C���[���.
    private PlayerState _playerState;
    // ��I�����̏���.
    private HuntingEnd _huntingEnd;
    // UI.
    public GameObject[] _ui;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _ui[(int)UIKinds.MENU].SetActive(_playerState.GetOpenMenu());
        _ui[(int)UIKinds.OPTION].SetActive(_playerState.GetOpenOption());
        _ui[(int)UIKinds.CLEAR].SetActive(_huntingEnd.GetQuestClear());
        _ui[(int)UIKinds.FAILED].SetActive(_huntingEnd.GetQuestFailed());
    }
}
