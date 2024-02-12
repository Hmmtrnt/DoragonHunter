/*クエストリスト内の処理*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestListUpdate : MonoBehaviour
{
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // シーン遷移管理.
    private SceneTransitionManager _sceneTransitionManager;
    // 選択したUI.
    private SelectSceneSelectUi _SelectUi;
    // SE.
    private SEManager _seManager;
    // フェード.
    private Fade _fade;
    // 決定ボタンを押したかどうか.
    private bool _decidePush = false;
    // クエストの難易度.
    private bool _hard = false;

    void Start()
    {
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _sceneTransitionManager = GameObject.Find("GameManager").GetComponent<SceneTransitionManager>();
        _SelectUi = GameObject.Find("SelectDraw").GetComponent<SelectSceneSelectUi>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
    }

    void Update()
    {
        DecidePush();
    }

    private void FixedUpdate()
    {
        if (_decidePush)
        {
            SceneTransition();
        }
        Difficulty();
    }

    // 難易度の設定.
    private void Difficulty()
    {
        if (_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.EASY)
        {
            _hard = false;
        }
        else if (_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.HATD)
        {
            _hard = true;
        }
    }

    // 決定した時の処理.
    private void DecidePush()
    {
        if (_controllerManager._AButtonDown)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.QUEST_START);
            _decidePush = true;
            _fade._isFading = false;
        }
    }

    // シーン遷移を行う.
    private void SceneTransition()
    {
        if (!_decidePush) return;

        // シーン遷移
        SceneManager.sceneLoaded += SceneTransitionUpdate;
        if (_fade._fadeEnd)
        {
            _sceneTransitionManager.MainScene();
        }
    }

    // シーン遷移時に行う処理.
    private void SceneTransitionUpdate(Scene next, LoadSceneMode mode)
    {
        // シーン遷移先にあるスクリプト追加.
        MainSceneManager mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();

        // 難易度を選択した情報を代入.
        mainSceneManager._hitPointMany = _hard;

        SceneManager.sceneLoaded -= SceneTransitionUpdate;
    }

    
}
