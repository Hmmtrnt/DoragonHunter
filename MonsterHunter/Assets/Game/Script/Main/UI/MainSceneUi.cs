/*�Q�[����ʑS�̂�UI�̏���*/

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
        GREATEATTACKBUTTONGUIDE,// �K�E�Z�̃{�^���K�C�h.
        UINUM,      // UI�̐�.
    }

    // �v���C���[���.
    private PlayerState _playerState;
    // UI.
    public GameObject[] _ui;

    // �V�[���̏��.
    private MainSceneManager _mainSceneManager;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
    }

    private void FixedUpdate()
    {
        _ui[(int)UIKinds.MENU].SetActive(_mainSceneManager.GetOpenMenu());
        _ui[(int)UIKinds.OPTION].SetActive(_mainSceneManager.GetOpenOption());
        _ui[(int)UIKinds.RETIREMENT].SetActive(_mainSceneManager.GetOpenRetireConfirmation());
        _ui[(int)UIKinds.PAUSE].SetActive(_mainSceneManager.GetOpenPause());
        _ui[(int)UIKinds.GAMEPLAY].SetActive(_mainSceneManager.GetGamePlayUI());
        _ui[(int)UIKinds.GREATEATTACKBUTTONGUIDE].SetActive(_playerState.GetApplyRedRenkiGauge());
    }
}
