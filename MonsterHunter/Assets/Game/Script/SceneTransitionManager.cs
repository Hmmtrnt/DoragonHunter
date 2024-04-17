/*シーンマネージャー*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    // タイトルシーン遷移.
    public void TitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    // セレクトシーン遷移.
    public void SelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    // メインシーン遷移.
    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // チュートリアルシーン遷移.
    public void TutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
