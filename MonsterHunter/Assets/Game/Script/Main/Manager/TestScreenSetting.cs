/*�r���h���s����ۂɉ�ʂ̑傫���ݒ�*/

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
