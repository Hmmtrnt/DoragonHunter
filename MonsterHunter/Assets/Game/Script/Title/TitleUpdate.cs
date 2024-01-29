/*タイトル全体の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUpdate : MonoBehaviour
{
    // ゲームパッドの入力情報.
    private ControllerManager _controllerManager;
    // シーン遷移管理.
    private SceneTransitionManager _sceneTransitionManager;
    // PRESSANYBUTTONオブジェクト.
    private GameObject _pressAnyButton;
    // GAMESTARTオブジェクト.
    private GameObject _gameStart;
    // OPTIONオブジェクト.
    private GameObject _option;
    // 選択枠の影.
    public GameObject[] _selectShadow;
    // 選択UIの処理.
    private TitleSelectUi _selectUi;
    // タイトルSEの処理.
    private SEManager _seManager;

    // PressAnyButtonを押したときtrueにする.
    public bool _pressAnyPush = false;

    void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _pressAnyButton = GameObject.Find("PRESSANYBUTTON").gameObject;
        _gameStart = GameObject.Find("GAMESTART").gameObject;
        _option = GameObject.Find("OPTION").gameObject;
        _selectUi = GameObject.Find("Select").GetComponent<TitleSelectUi>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();

        _gameStart.SetActive(false);
        _option.SetActive(false);
        _selectShadow[1].SetActive(false);

    }

    void Update()
    {
        SceneTransition();
        // メインシーンへ遷移
        // デバッグ用.
        //if (Input.anyKeyDown)
        //{
        //    _sceneTransitionManager.MainScene();
        //}
        if (_pressAnyPush) return;

        PressAnyPushflag();
    }

    private void FixedUpdate()
    {
        UiDraw();
    }

    // GameStartを押したらメインシーンへ(デバッグ用).
    private void SceneTransition()
    {
        // PressAnyButtonが押されていなければスキップ.
        if (!_pressAnyPush) return;
        if(_controllerManager._AButtonDown && _selectUi.GetSelectNumber() == 0)
        {
            _sceneTransitionManager.SelectScene();
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
        }
        
    }

    // PressAnyBottonを押したときGameStartとOPTIONのUIを描画するためにflagをtrue.
    private void PressAnyPushflag()
    {
        if(_controllerManager._PressAnyButton)
        {
            _pressAnyPush = true;
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
        }
    }

    // UIの描画.
    private void UiDraw()
    {
        if(_pressAnyPush)
        {
            _pressAnyButton.SetActive(false);
            _gameStart.SetActive(true);
            _option.SetActive(true);
            _selectShadow[1].SetActive(true);
        }
    }
}

