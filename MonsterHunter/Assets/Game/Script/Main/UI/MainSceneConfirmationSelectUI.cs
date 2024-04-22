/*確認画面の選択UIの制御*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneConfirmationSelectUI : MonoBehaviour
{
    public enum SelectItem
    {
        NO,     // いいえ.
        YES,    // はい.
        MAXNUM  // 項目数.
    }

    // UIの座標.
    private RectTransform _rectTransform;
    // 選択するUIの関数.
    private Menu _menu;
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // シーン遷移の情報.
    private SceneTransitionManager _sceneTransitionManager;
    // メインシーンの情報.
    private HuntingSceneManager _huntingSceneManager;
    // SE.
    private SEManager _seManager;
    // フェード.
    private Fade _fade;

    // 現在選ばれている選択番号.
    public int _selectNum = (int)SelectItem.NO;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _sceneTransitionManager = GameObject.Find("GameManager").GetComponent<SceneTransitionManager>();
        _huntingSceneManager = GameObject.Find("GameManager").GetComponent<HuntingSceneManager>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
        _selectNum = (int)SelectItem.NO;
    }

    private void OnEnable()
    {
        _selectNum = (int)SelectItem.NO;
    }

    void Update()
    {
        _menu.SelectMove(_controllerManager._RightLeftCrossKey , ref _selectNum);
        _menu.CrossKeyPushFlameCount(_controllerManager._RightLeftCrossKey);
        _menu.CrossKeyNoPush(_controllerManager._RightLeftCrossKey);
        SelectDecision();
    }

    private void FixedUpdate()
    {
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXNUM);
        SelectPosition();
    }

    // 選択されている項目の座標を入力.
    private void SelectPosition()
    {
        if (_selectNum == (int)SelectItem.NO)
        {
            _rectTransform.anchoredPosition = new Vector3(-75.0f, -20.0f, 0.0f);
        }
        else if (_selectNum == (int)SelectItem.YES)
        {
            _rectTransform.anchoredPosition = new Vector3(75.0f, -20.0f, 0.0f);
        }
    }

    // 選択されている項目の決定.
    private void SelectDecision()
    {
        if(_selectNum == (int)SelectItem.NO && _controllerManager._AButtonDown)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
            _huntingSceneManager._openRetireConfirmation = false;
        }
        else if(_selectNum == (int)SelectItem.YES && _controllerManager._AButtonDown) 
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
            _fade._isFading = false;
            
        }
        if(_fade._fadeEnd && _selectNum == (int)SelectItem.YES)
        {
            _sceneTransitionManager.SelectScene();
        }
    }
}
