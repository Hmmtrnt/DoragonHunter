/*ビルド実行する際に画面の大きさ設定*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreenSetting : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow, 60);
    }
}
