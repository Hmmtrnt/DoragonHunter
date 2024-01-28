/*メニュー画面の表示非表示*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneMenuUi : MonoBehaviour
{
    // プレイヤー情報.
    private PlayerState _playerState;
    // MenuUI.
    private GameObject _menuUI;
    // OptionUI.
    private GameObject _optionMenuUI;
    // victoryUI.
    private GameObject _victoryUI;
    // LoseUI.
    private GameObject _loseUI;

    // 狩猟終了時の処理.
    private HuntingEnd _huntingEnd;
    // 一時停止中のUI
    //private GameObject _pauseKeepUI;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _menuUI = GameObject.Find("Menu").gameObject;
        _optionMenuUI = GameObject.Find("OptionMenu").gameObject;
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        _victoryUI = GameObject.Find("ResultClear").gameObject;
        _loseUI = GameObject.Find("ResultFailed").gameObject;
        //_pauseKeepUI = GameObject.Find("PauseCanvas").gameObject;

        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _menuUI.SetActive(_playerState.GetOpenMenu());
        _optionMenuUI.SetActive(_playerState.GetOpenOption());
        _victoryUI.SetActive(false);
        _loseUI.SetActive(false);
    }
}
