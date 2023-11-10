/*シーンマネージャー*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    // タイトルシーン遷移
    public void TitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    // メインシーン遷移
    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // リザルトシーン遷移
    public void ResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
