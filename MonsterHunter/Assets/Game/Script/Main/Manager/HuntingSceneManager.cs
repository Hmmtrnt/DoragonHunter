/*狩猟中のシーンマネージャー*/

using UnityEngine;

public class HuntingSceneManager : MonoBehaviour
{
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // BGMマネージャー.
    private BGMManager _bgmManager;
    // ポーズ画面.
    private MainSceneMenuSelectUi _mainSceneSelectUi;
    // 一時停止.
    private PauseTimeStop _pauseTimeStop;
    // モンスターの情報.
    private MonsterState _monsterState;
    // プレイヤーの情報.
    private PlayerState _playerState;
    // SE.
    private SEManager _seManager;
    // 狩猟終了した時.
    private HuntingEnd _huntingEnd;

    // 一時停止しているかどうか.
    private bool _pauseStop = false;
    // メニュー画面を開いているか.
    public bool _openMenu = false;
    // オプション画面を開いているか.
    public bool _openOption = false;
    // ポーズ画面を開いているかどうか.
    public bool _openPause = false;
    // リタイア確認画面を開いているか.
    public bool _openRetireConfirmation = false;
    // プレイ中UIの表示非表示.
    public bool _openGamePlayUi = true;

    // ゲーム全体の時間を停止するまでの時間
    private int _pauseCount;

    // モンスターの体力が多いかどうか.
    public bool _hitPointMany = false;

    void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        _mainSceneSelectUi = GameObject.Find("SelectItem").GetComponent<MainSceneMenuSelectUi>();
        _pauseTimeStop = GetComponent<PauseTimeStop>();
        _monsterState = GameObject.Find("Dragon").GetComponent<MonsterState>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        
        _pauseCount = 15;
    }

    void Update()
    {
        // ゲームの流れを止めたり動かしたりする.
        if(_pauseStop)
        {
            _openMenu = false;
            _openPause = true;

            if(_pauseCount == 0)
            {
                _pauseTimeStop.StopTime();
            }

            _pauseCount--;
        }
        else
        {
            _pauseTimeStop.StartTime();
            _openPause = false;
            _pauseCount = 15;
        }


        // 一時停止を押したときの処理.
        if(_mainSceneSelectUi._selectNum == (int)MainSceneMenuSelectUi.SelectItem.PAUSE && 
            _controllerManager._AButtonDown && _openMenu &&
            !_pauseStop)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
            _pauseStop = true;
        }
        // 再開.
        else if(_pauseStop && _controllerManager._PressAnyButton)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
            _pauseStop = false;
        }
        MenuOpneAndClose();
    }

    private void FixedUpdate()
    {
        BGMChange();
    }

    // メニュー画面の開閉制御.
    private void MenuOpneAndClose()
    {
        // 開くとき
        if (!_openMenu && !_huntingEnd._questEnd)
        {
            if (_controllerManager._MenuButtonDown)
            {
                _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
                _openMenu = true;
            }
        }
        // 閉じるとき
        else
        {
            // メニュー画面からさらに開いている画面があれば
            // その画面を優先的に閉じる.
            if (_controllerManager._BButtonDown)
            {
                _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
                if (_openOption)
                {
                    _openOption = false;
                    return;
                }
                else if(_openRetireConfirmation)
                {
                    _openRetireConfirmation = false;
                    return;
                }

                _openMenu = false;
            }
        }
    }

    // モンスターを倒したときにBGMを変更する.
    private void BGMChange()
    {
        if(_monsterState.GetHitPoint() <= 0)
        {
            _bgmManager.BGMChange((int)BGMManager.BGM.VICTORY);
        }
        else if(_playerState.GetHitPoint() <= 0)
        {
            _bgmManager.BGMChange((int)BGMManager.BGM.FAILED);
        }
        
    }

    // メニュー画面を開いているかどうか.
    public bool GetOpenMenu() { return _openMenu; }
    // オプション画面を開いているかどうか.
    public bool GetOpenOption() { return _openOption; }
    // ポーズ画面を開いているかどうか.
    public bool GetOpenPause() { return _openPause; }
    // リタイア確認画面を開いているかどうか.
    public bool GetOpenRetireConfirmation() { return _openRetireConfirmation; }
    // プレイ中のUIを表示しているかどうか.
    public bool GetGamePlayUI() { return _openGamePlayUi; }

    // 現在一時停止中かどうか.
    public bool GetPauseStop() { return _pauseStop; }
}
