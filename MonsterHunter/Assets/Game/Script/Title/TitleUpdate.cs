using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUpdate : MonoBehaviour
{
    private SceneTransitionManager _sceneTransitionManager;


    void Start()
    {
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // メインシーンへ遷移
        // デバッグ用.
        //if (Input.anyKeyDown)
        //{
        //    _sceneTransitionManager.MainScene();
        //}
    }

    private void FixedUpdate()
    {
        
    }

    // PressAnyBottonを押したとき、GAME STARTとOPTIONを表示.
    private void SecondUIDraw()
    {
        
    }
}

