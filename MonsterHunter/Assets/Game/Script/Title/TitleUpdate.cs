using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUpdate : MonoBehaviour
{
    private SceneTransitionManager _sceneTransitionManager;

    // Start is called before the first frame update
    void Start()
    {
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // メインシーンへ遷移
        if (Input.anyKeyDown)
        {
            _sceneTransitionManager.MainScene();
        }
    }
}
