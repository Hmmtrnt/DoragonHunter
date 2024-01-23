/*メインシーンマネージャー*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    private BGMManager _bgmManager;

    void Start()
    {
        _bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();

        //_bgmManager.BGMLoopPlay();
    }

    void Update()
    {
        
    }
}
