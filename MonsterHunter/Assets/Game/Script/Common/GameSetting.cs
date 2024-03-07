/*ゲーム全体の設定*/

using UnityEngine;

public class GameSetting : MonoBehaviour
{
    // 画面サイズ.
    public int _width = 1920;
    public int _height = 1080;
    // リフレッシュレート.
    public int _refreshRate = 60;

    private void Awake()
    {
        FixedFPS();
        FixedScreenSize();
        CursorNoDraw();
    }

    /// <summary>
    /// fps固定.
    /// </summary>

    private void FixedFPS()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// スクリーンサイズ固定.
    /// </summary>
    private void FixedScreenSize()
    {
        Screen.SetResolution(_width, _height, FullScreenMode.FullScreenWindow, _refreshRate);
    }

    /// <summary>
    /// マウスカーソルを表示させない
    /// </summary>
    private void CursorNoDraw()
    {
        Cursor.visible = false;
    }

}
