/*�I����ʑS�̂̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUpdate : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // �I������UI.
    private SelectSceneSelectUi _SelectUi;

    void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _SelectUi = GameObject.Find("SelectDraw").GetComponent<SelectSceneSelectUi>();
    }

    void Update()
    {
        
    }
}
