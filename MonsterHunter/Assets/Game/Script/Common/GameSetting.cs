/*�Q�[���S�̂̐ݒ�*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    // ��ʃT�C�Y.
    public int _width = 1920;
    public int _height = 1080;
    // ���t���b�V�����[�g.
    public int _refreshRate = 60;

    private void Awake()
    {
        FixedFPS();
        FixedScreenSize();
    }

    /// <summary>
    /// fps�Œ�.
    /// </summary>

    private void FixedFPS()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// �X�N���[���T�C�Y�Œ�.
    /// </summary>
    private void FixedScreenSize()
    {
        Screen.SetResolution(_width, _height, FullScreenMode.FullScreenWindow, _refreshRate);
    }

}
