/*�N�G�X�g���X�g���̏���*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestListUpdate : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // �V�[���J�ڊǗ�.
    private SceneTransitionManager _sceneTransitionManager;
    // �I������UI.
    private SelectSceneSelectUi _SelectUi;
    // SE.
    private SEManager _seManager;
    // �t�F�[�h.
    private Fade _fade;
    // ����{�^�������������ǂ���.
    private bool _decidePush = false;
    // �N�G�X�g�̓�Փx.
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

    // ��Փx�̐ݒ�.
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

    // ���肵�����̏���.
    private void DecidePush()
    {
        if (_controllerManager._AButtonDown)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.QUEST_START);
            _decidePush = true;
            _fade._isFading = false;
        }
    }

    // �V�[���J�ڂ��s��.
    private void SceneTransition()
    {
        if (!_decidePush) return;

        // �V�[���J��
        SceneManager.sceneLoaded += SceneTransitionUpdate;
        if (_fade._fadeEnd)
        {
            _sceneTransitionManager.MainScene();
        }
    }

    // �V�[���J�ڎ��ɍs������.
    private void SceneTransitionUpdate(Scene next, LoadSceneMode mode)
    {
        // �V�[���J�ڐ�ɂ���X�N���v�g�ǉ�.
        MainSceneManager mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();

        // ��Փx��I������������.
        mainSceneManager._hitPointMany = _hard;

        SceneManager.sceneLoaded -= SceneTransitionUpdate;
    }

    
}
