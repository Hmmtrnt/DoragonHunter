/*狩猟終了時の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HuntingEnd : MonoBehaviour
{
    // メインシーンの情報.
    private MainSceneManager _mainSceneManager;

    // モンスターの情報.
    private Monster _monsterState;
    // ハンターの情報.
    private Player _playerState;
    // シーン遷移.
    private SceneTransitionManager _sceneTransitionManager;
    // パッド入力情報.
    private ControllerManager _controllerManager;
    // クエスト時間取得.
    private QuestTime _questTime;
    // リザルト更新処理.
    private ResultUpdate _resultUpdate;
    // SE.
    private SEManager _seManager;
    // フェード.
    private Fade _fade;

    // 狩猟成功したか.
    private bool _QuestClear = false;
    // 狩猟失敗したか.
    private bool _QuestFailed = false;
    // クエストを終了下かどうか.
    public bool _questEnd = false;
    // セレクト画面に戻るかどうか.
    private bool _selectSceneRemove = false;

    // クエストの時間を保存.
    public int _Minute = 0;
    public int _Second = 0;

    // 現在のランク.
    private int _currentRank = 0;

    // 狩猟成功してシーン遷移を行うまでの時間.
    private int _startSceneTransitionCount = 0;

    void Start()
    {
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
        _monsterState = GameObject.Find("Dragon").GetComponent<Monster>();
        _playerState = GameObject.Find("Hunter").GetComponent<Player>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _controllerManager = GetComponent<ControllerManager>();
        _questTime = GameObject.Find("LongTimeShaft").GetComponent<QuestTime>();
        _resultUpdate = GameObject.Find("ResultBackGround").GetComponent<ResultUpdate>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
        _startSceneTransitionCount = 1000;
        _selectSceneRemove = false;
    }

    private void Update()
    {
        SceneTransition();
    }

    private void FixedUpdate()
    {
        RankDown();
        HuntingEndBranch();
        
        QuestResult();
    }

    // シーン遷移を行う.
    private void SceneTransition()
    {
        // フェード開始.
        if(_resultUpdate.GetAnimEnd())
        {
            // 選択画面.
            if (_controllerManager._AButtonDown)
            {
                _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
                _selectSceneRemove = true;
                _fade._isFading = false;
            }
            // タイトル画面.
            else if (_controllerManager._BButtonDown)
            {
                _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
                _selectSceneRemove = false;
                _fade._isFading = false;
            }
        }
        // シーン遷移.
        if(_fade._fadeEnd && _selectSceneRemove)
        {
            _sceneTransitionManager.SelectScene();
        }
        else if(_fade._fadeEnd && !_selectSceneRemove) 
        {
            _sceneTransitionManager.TitleScene();
        }

    }

    // シーン遷移時に行う処理.
    private void SceneTransitionUpdate(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプト追加.
        ResultUpdate resultBranch = GameObject.Find("GameManager").GetComponent<ResultUpdate>();

        // クエスト終了時のクエスト状態を代入.
        //resultBranch._questClear = _QuestClear;


        SceneManager.sceneLoaded -= SceneTransitionUpdate;
    }

    // モンスター又、プレイヤーの体力が0になった時.クエストのクリアどうか変更.
    // デバッグ状態.
    private void HuntingEndBranch()
    {
        // 敵の体力が尽きたらクリア.
        if(_monsterState.GetHitPoint() == 0)
        {
            _QuestClear = true;
        }
        // ハンターの体力が尽きる、また時間が過ぎたら失敗.
        else if(_playerState.GetHitPoint() == 0 || _Minute >= 50)
        {
            _QuestClear= false;
        }


        if (_playerState.GetHitPoint()==0 || _monsterState.GetHitPoint() == 0 || _Minute >= 50)
        {
            _questEnd = true;
            _Minute = _questTime.GetMinutes();
            _Second = _questTime.GetSecond();
        }
    }

    // 時間経過でランクダウン.
    private void RankDown()
    {
        // ハードモードのタイム.
        if (_mainSceneManager._hitPointMany)
        {
            if (_Minute >= 5)
            {
                _currentRank = 1;
            }
            else if (_Minute >= 9)
            {
                _currentRank = 2;
            }
            else if (_Minute >= 13)
            {
                _currentRank = 3;
            }
        }
        // ノーマルモードのタイム.
        else if (!_mainSceneManager._hitPointMany)
        {
            if (_Minute >= 3)
            {
                _currentRank = 1;
            }
            else if (_Minute >= 6)
            {
                _currentRank = 2;
            }
            else if (_Minute >= 10)
            {
                _currentRank = 3;
            }
        }
    }

    // カウント開始.
    private void CountStart()
    {
        // クエストをクリアしていないときにスキップ.
        if (!_QuestClear) return;
        _startSceneTransitionCount--;
    }

    // クエストの結果を決定.
    private void QuestResult()
    {
        if(_playerState.GetHitPoint()<=0)
        {
            _QuestFailed = true;
            _mainSceneManager._openGamePlayUi = false;
        }
        else if(_monsterState.GetHitPoint()<=0) 
        {
            _QuestClear = true;
            _mainSceneManager._openGamePlayUi = false;
        }
    }

    // クエストをクリアしたかどうか.
    public bool GetQuestClear() { return _QuestClear; }
    // クエストを失敗したかどうか.
    public bool GetQuestFailed() { return _QuestFailed; }
    // クエストを終了したかどうか.
    public bool GetQuestEnd() { return _questEnd; }
    // クエスト終了のランクを取得.
    public int GetRank() { return _currentRank; }

}
