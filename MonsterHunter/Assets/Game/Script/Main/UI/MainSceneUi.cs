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

    // ����V�[���̏��.
    private HuntingSceneManager _huntingSceneManager;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _huntingSceneManager = GameObject.Find("GameManager").GetComponent<HuntingSceneManager>();
    }

    private void FixedUpdate()
    {
        _ui[(int)UIKinds.MENU].SetActive(_huntingSceneManager.GetOpenMenu());
        _ui[(int)UIKinds.OPTION].SetActive(_huntingSceneManager.GetOpenOption());
        _ui[(int)UIKinds.RETIREMENT].SetActive(_huntingSceneManager.GetOpenRetireConfirmation());
        _ui[(int)UIKinds.PAUSE].SetActive(_huntingSceneManager.GetOpenPause());
        _ui[(int)UIKinds.GAMEPLAY].SetActive(_huntingSceneManager.GetGamePlayUI());
        _ui[(int)UIKinds.GREATEATTACKBUTTONGUIDE].SetActive(_playerState.GetApplyRedRenkiGauge());
    }
}
