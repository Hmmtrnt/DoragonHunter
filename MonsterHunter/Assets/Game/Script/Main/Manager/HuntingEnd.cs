/*狩猟終了時の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HuntingEnd : MonoBehaviour
{
    // モンスターの情報.
    private MonsterState _monsterState;
    // ハンターの情報.
    private PlayerState _playerState;
    // シーン遷移.
    private SceneTransitionManager _sceneTransitionManager;

    // 狩猟成功したか.
    private bool _QuestClear = false;

    // 狩猟成功してシーン遷移を行うまでの時間.
    private int _startSceneTransitionCount = 0;

    void Start()
    {
        _monsterState = GameObject.Find("Dragon").GetComponent<MonsterState>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _startSceneTransitionCount = 1000;
    }

    private void FixedUpdate()
    {
        HuntingEndBranch();


        SceneTransition();
    }

    // シーン遷移を行う.
    private void SceneTransition()
    {
        // デバッグ用シーン遷移.
        if (_monsterState.GetHitPoint() == 0 || _playerState.GetHitPoint() == 0)
        {
            // シーン切り替え時にイベント登録.
            SceneManager.sceneLoaded += GameSceneLoaded;

            // シーン切り替え.
            _sceneTransitionManager.ResultScene();
        }
    }

    // シーン遷移時に行う処理.
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプト追加.
        ResultBranch resultBranch = GameObject.Find("GameManager").GetComponent<ResultBranch>();

        // クエスト終了時のクエスト状態を代入.
        resultBranch._questClear = _QuestClear;


        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    // モンスター又、プレイヤーの体力が0になった時.クエストのクリアどうか変更.
    // デバッグ状態.
    private void HuntingEndBranch()
    {
        if(_monsterState.GetHitPoint() == 0)
        {
            _QuestClear = true;
        }
        else if(_playerState.GetHitPoint() == 0)
        {
            _QuestClear= false;
        }
    }

    // カウント開始.
    private void CountStart()
    {
        // クエストをクリアしていないときにスキップ.
        if (!_QuestClear) return;
        _startSceneTransitionCount--;
    }
}
