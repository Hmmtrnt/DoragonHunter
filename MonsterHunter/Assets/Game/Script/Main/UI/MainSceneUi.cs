/*ゲーム画面全体のUIの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUi : MonoBehaviour
{
    private enum UIKinds
    {
        MENU,       // メニュー.
        OPTION,     // オプション.
        RETIREMENT, // リタイア確認画面.
        PAUSE,      // 一時停止中画面.
        CLEAR,      // クリア.
        FAILED,     // 失敗.
        UINUM,      // UIの数.
    }

    // プレイヤー情報.
    private PlayerState _playerState;
    // 狩猟終了時の処理.
    private HuntingEnd _huntingEnd;
    // UI.
    public GameObject[] _ui;

    // シーンの情報.
    private MainSceneManager _mainSceneManager;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
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
        _ui[(int)UIKinds.CLEAR].SetActive(_huntingEnd.GetQuestClear());
        _ui[(int)UIKinds.FAILED].SetActive(_huntingEnd.GetQuestFailed());
    }
}
