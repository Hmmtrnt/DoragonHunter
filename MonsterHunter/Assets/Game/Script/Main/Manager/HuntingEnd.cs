/*狩猟終了時の処理*/

using UnityEngine;

public class HuntingEnd : MonoBehaviour
{
    // ランクの番号.
    enum RankNumber
    {
        S = 0,
        A,
        B,
        C,
        MAXNUM
    }

    // ランクダウンさせる時間帯.
    // ノーマルモード.
    private int[] _rankDownNormalTime = new int[(int)RankNumber.MAXNUM];
    // ハードモード.
    private int[] _rankDownHardTime = new int[(int)RankNumber.MAXNUM];

    // 狩猟中シーンの情報.
    private HuntingSceneManager _huntingSceneManager;

    // モンスターの情報.
    private MonsterState _monsterState;
    // ハンターの情報.
    private PlayerState _playerState;
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

    // クエスト失敗になる時間.
    private const int _minuteFailed = 50;

    // 現在のランク.
    private int _currentRank = 0;

    void Start()
    {
        _rankDownNormalTime[0] = 3;
        _rankDownNormalTime[1] = 6;
        _rankDownNormalTime[2] = 10;

        _rankDownHardTime[0] = 5;
        _rankDownHardTime[1] = 9;
        _rankDownHardTime[2] = 13;

        _huntingSceneManager = GameObject.Find("GameManager").GetComponent<HuntingSceneManager>();
        _monsterState = GameObject.Find("Dragon").GetComponent<MonsterState>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _controllerManager = GetComponent<ControllerManager>();
        _questTime = GameObject.Find("LongTimeShaft").GetComponent<QuestTime>();
        _resultUpdate = GameObject.Find("ResultBackGround").GetComponent<ResultUpdate>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
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

    // モンスター又、プレイヤーの体力が0になった時.クエストのクリアどうか変更.
    private void HuntingEndBranch()
    {
        // 敵の体力が尽きたらクリア.
        if(_monsterState.GetHitPoint() == 0)
        {
            _QuestClear = true;
        }
        // ハンターの体力が尽きる、また時間が過ぎたら失敗.
        else if(_playerState.GetHitPoint() == 0 || _Minute >= _minuteFailed)
        {
            _QuestClear= false;
        }


        if (_playerState.GetHitPoint()==0 || _monsterState.GetHitPoint() == 0 || _Minute >= _minuteFailed)
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
        if (_huntingSceneManager._hitPointMany)
        {
            if (_Minute >= _rankDownHardTime[0])
            {
                _currentRank = (int)RankNumber.A;
            }
            else if (_Minute >= _rankDownHardTime[1])
            {
                _currentRank = (int)RankNumber.B;
            }
            else if (_Minute >= _rankDownHardTime[2])
            {
                _currentRank = (int)RankNumber.C;
            }
        }
        // ノーマルモードのタイム.
        else if (!_huntingSceneManager._hitPointMany)
        {
            if (_Minute >= _rankDownNormalTime[0])
            {
                _currentRank = (int)RankNumber.A;
            }
            else if (_Minute >= _rankDownNormalTime[1])
            {
                _currentRank = (int)RankNumber.B;
            }
            else if (_Minute >= _rankDownNormalTime[2])
            {
                _currentRank = (int)RankNumber.C;
            }
        }
    }

    // クエストの結果を決定.
    private void QuestResult()
    {
        if(_playerState.GetHitPoint()<=0)
        {
            _QuestFailed = true;
            _huntingSceneManager._openGamePlayUi = false;
        }
        else if(_monsterState.GetHitPoint()<=0) 
        {
            _QuestClear = true;
            _huntingSceneManager._openGamePlayUi = false;
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
