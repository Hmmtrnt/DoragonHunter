/*����̃V�[���}�l�[�W���[*/

using UnityEngine;

public class HuntingSceneManager : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // BGM�}�l�[�W���[.
    private BGMManager _bgmManager;
    // �|�[�Y���.
    private MainSceneMenuSelectUi _mainSceneSelectUi;
    // �ꎞ��~.
    private PauseTimeStop _pauseTimeStop;
    // �����X�^�[�̏��.
    private MonsterState _monsterState;
    // �v���C���[�̏��.
    private PlayerState _playerState;
    // SE.
    private SEManager _seManager;
    // ��I��������.
    private HuntingEnd _huntingEnd;

    // �ꎞ��~���Ă��邩�ǂ���.
    private bool _pauseStop = false;
    // ���j���[��ʂ��J���Ă��邩.
    public bool _openMenu = false;
    // �I�v�V������ʂ��J���Ă��邩.
    public bool _openOption = false;
    // �|�[�Y��ʂ��J���Ă��邩�ǂ���.
    public bool _openPause = false;
    // ���^�C�A�m�F��ʂ��J���Ă��邩.
    public bool _openRetireConfirmation = false;
    // �v���C��UI�̕\����\��.
    public bool _openGamePlayUi = true;

    // �Q�[���S�̂̎��Ԃ��~����܂ł̎���
    private int _pauseCount;

    // �����X�^�[�̗̑͂��������ǂ���.
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
        // �Q�[���̗�����~�߂��蓮�������肷��.
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


        // �ꎞ��~���������Ƃ��̏���.
        if(_mainSceneSelectUi._selectNum == (int)MainSceneMenuSelectUi.SelectItem.PAUSE && 
            _controllerManager._AButtonDown && _openMenu &&
            !_pauseStop)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
            _pauseStop = true;
        }
        // �ĊJ.
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

    // ���j���[��ʂ̊J����.
    private void MenuOpneAndClose()
    {
        // �J���Ƃ�
        if (!_openMenu && !_huntingEnd._questEnd)
        {
            if (_controllerManager._MenuButtonDown)
            {
                _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
                _openMenu = true;
            }
        }
        // ����Ƃ�
        else
        {
            // ���j���[��ʂ��炳��ɊJ���Ă����ʂ������
            // ���̉�ʂ�D��I�ɕ���.
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

    // �����X�^�[��|�����Ƃ���BGM��ύX����.
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

    // ���j���[��ʂ��J���Ă��邩�ǂ���.
    public bool GetOpenMenu() { return _openMenu; }
    // �I�v�V������ʂ��J���Ă��邩�ǂ���.
    public bool GetOpenOption() { return _openOption; }
    // �|�[�Y��ʂ��J���Ă��邩�ǂ���.
    public bool GetOpenPause() { return _openPause; }
    // ���^�C�A�m�F��ʂ��J���Ă��邩�ǂ���.
    public bool GetOpenRetireConfirmation() { return _openRetireConfirmation; }
    // �v���C����UI��\�����Ă��邩�ǂ���.
    public bool GetGamePlayUI() { return _openGamePlayUi; }

    // ���݈ꎞ��~�����ǂ���.
    public bool GetPauseStop() { return _pauseStop; }
}
