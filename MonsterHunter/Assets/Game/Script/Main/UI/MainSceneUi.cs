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

    // シーンの情報.
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
