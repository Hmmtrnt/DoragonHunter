/*選択画面全体の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUpdate : MonoBehaviour
{
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // 選択したUI.
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
