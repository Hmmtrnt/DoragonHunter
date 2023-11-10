using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneTransition : MonoBehaviour
{
    public void DebugPlayScene()
    {
        SceneManager.LoadScene("DebugPlayScene");
    }

    public void TitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
