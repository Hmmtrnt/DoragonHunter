/*タイトル画面へ戻るかのメニュー画面*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;

public class TitleTransitionMenu : MonoBehaviour
{
    // 選択項目数.
    enum SelectItem
    {
        NO, 
        YES,
        MAX_NUM
    }

    // タイトル画面に戻るUI.
    private TitleGuide _titleGuide;

    // UIの座標
    private RectTransform _rectTransform;
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // 選択するUIの関数.
    private Menu _menu;
    // SE.
    private SEManager _seManager;
    // フェード.
    private Fade _fade;
    // シーン遷移管理.
    private SceneTransitionManager _sceneTransitionManager;
    // 

    // 現在選ばれている選択番号.
    private int _selectNum = (int)SelectItem.NO;

    void Start()
    {
        _titleGuide = GameObject.Find("TitleGuide").GetComponent<TitleGuide>();
        _rectTransform = GetComponent<RectTransform>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
        _sceneTransitionManager = GameObject.Find("GameManager").GetComponent<SceneTransitionManager>();
    }

    void Update()
    {
        _menu.SelectMove(_controllerManager._RightLeftCrossKey, ref _selectNum);
        _menu.CrossKeyPushFlameCount(_controllerManager._RightLeftCrossKey);
        _menu.CrossKeyNoPush(_controllerManager._RightLeftCrossKey);
        TitleSceneTransition();
        UINoDecide();
    }

    private void FixedUpdate()
    {
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAX_NUM);
        SelectPosition();
    }

    // 選択されている項目の座標を代入.
    private void SelectPosition()
    {
        if(_selectNum == (int)SelectItem.NO)
        {
            _rectTransform.anchoredPosition = new Vector3(-280.0f,-60.0f,0.0f);
        }
        else if(_selectNum == (int)SelectItem.YES)
        {
            _rectTransform.anchoredPosition = new Vector3(280.0f, -60.0f, 0.0f);
        }
    }

    // はいを押したときのシーン遷移処理.
    private void TitleSceneTransition()
    {
        if (_controllerManager._AButtonDown && 
            _selectNum == (int)SelectItem.YES)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
            _fade._isFading = false;
        }
        if (_fade._fadeEnd)
        {
            _sceneTransitionManager.TitleScene();
        }
    }

    // いいえを押したときの処理.
    private void UINoDecide()
    {
        if(_controllerManager._AButtonDown &&
            _selectNum == (int)SelectItem.NO &&
            _fade._isFading)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
            _titleGuide.SetSceneTransitionUIOpen(false);
        }
    }

}
