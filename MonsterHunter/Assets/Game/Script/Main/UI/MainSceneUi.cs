/*ゲーム画面全体のUIの処理*/

using UnityEngine;

public class MainSceneUi : MonoBehaviour
{
    private enum UIKinds
    {
        MENU,       // メニュー.
        OPTION,     // オプション.
        RETIREMENT, // リタイア確認画面.
        PAUSE,      // 一時停止中画面.
        GAMEPLAY,   // プレイ中に表示されるUI. 
        GREATEATTACKBUTTONGUIDE,// 必殺技のボタンガイド.
        UINUM,      // UIの数.
    }

    // プレイヤー情報.
    private PlayerState _playerState;
    // UI.
    public GameObject[] _ui;

    // 狩猟中シーンの情報.
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
