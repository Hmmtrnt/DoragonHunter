/*�I����ʑS�̂̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectUpdate : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // �V�[���J�ڊǗ�.
    private SceneTransitionManager _sceneTransitionManager;

    // �I������UI.
    private SelectSceneSelectUi _SelectUi;
    // ����{�^�������������ǂ���.
    private bool _decidePush = false;
    // �N�G�X�g�̓�Փx.
    private bool _hard = false;

    void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _SelectUi = GameObject.Find("SelectDraw").GetComponent<SelectSceneSelectUi>();
    }

    void Update()
    {
        DecidePush();
    }

    private void FixedUpdate()
    {
        if(_decidePush)
        {
            SceneTransition();
        }
        Difficulty();
    }

    // ��Փx�̐ݒ�.
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


    // ���肵�����̏���.
    private void DecidePush()
    {
        if(_controllerManager._AButtonDown)
        {
            _decidePush = true;
        }
    }

    // �V�[���J�ڂ��s��.
    private void SceneTransition()
    {
        if (!_decidePush) return;

        // �V�[���J��
        SceneManager.sceneLoaded += SceneTransitionUpdate;

        _sceneTransitionManager.MainScene();
    }

    // �V�[���J�ڎ��ɍs������.
    private void SceneTransitionUpdate(Scene next, LoadSceneMode mode)
    {
        // �V�[���J�ڐ�ɂ���X�N���v�g�ǉ�.
        MonsterState monsterState�@= GameObject.Find("Dragon").GetComponent<MonsterState>();

        //Debug.Assert(monsterState != null);

        // ��Փx��I������������.
        monsterState._HitPointMany = _hard;

        SceneManager.sceneLoaded -= SceneTransitionUpdate;
    }
}
