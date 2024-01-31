/*選択画面全体の処理*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectUpdate : MonoBehaviour
{
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // シーン遷移管理.
    private SceneTransitionManager _sceneTransitionManager;
    // 選択したUI.
    private SelectSceneSelectUi _SelectUi;
    // 難易度説明のテキスト.
    private Text _explanationText;
    // 決定ボタンを押したかどうか.
    private bool _decidePush = false;
    // クエストの難易度.
    private bool _hard = false;

    void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _SelectUi = GameObject.Find("SelectDraw").GetComponent<SelectSceneSelectUi>();
        _explanationText = GameObject.Find("DifficultText").GetComponent<Text>();
    }

    void Update()
    {
        DecidePush();
        TitleTransitionScene();
    }

    private void FixedUpdate()
    {
        if(_decidePush)
        {
            SceneTransition();
        }
        Difficulty();
        ExplanationDraw();
    }

    // 難易度の設定.
    private void Difficulty()
    {
        if(_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.EASY)
        {
            _hard = false;
        }
        else if(_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.HATD)
        {
            _hard = true;
        }
    }


    // 決定した時の処理.
    private void DecidePush()
    {
        if(_controllerManager._AButtonDown)
        {
            _decidePush = true;
        }
    }

    // タイトルシーンに戻る.
    private void TitleTransitionScene()
    {
        if (_controllerManager._BButtonDown)
        {
            _sceneTransitionManager.TitleScene();
        }
    }

    // シーン遷移を行う.
    private void SceneTransition()
    {
        if (!_decidePush) return;

        // シーン遷移
        SceneManager.sceneLoaded += SceneTransitionUpdate;

        _sceneTransitionManager.MainScene();
    }

    // シーン遷移時に行う処理.
    private void SceneTransitionUpdate(Scene next, LoadSceneMode mode)
    {
        // シーン遷移先にあるスクリプト追加.
        MonsterState monsterState　= GameObject.Find("Dragon").GetComponent<MonsterState>();

        // 難易度を選択した情報を代入.
        monsterState._HitPointMany = _hard;

        SceneManager.sceneLoaded -= SceneTransitionUpdate;
    }

    // 難易度説明の表記(デバッグ用)
    private void ExplanationDraw()
    {
        if(_SelectUi.GetSelectNumber()==(int)SelectSceneSelectUi.SelectItem.EASY)
        {
            _explanationText.text = "モンスターの体力が少なく\n短時間で気軽にプレイできます。";
        }
        else if(_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.HATD)
        {
            _explanationText.text = "モンスターの体力が多く\n長時間がっつりとプレイできます。";
        }
        
    }
}
