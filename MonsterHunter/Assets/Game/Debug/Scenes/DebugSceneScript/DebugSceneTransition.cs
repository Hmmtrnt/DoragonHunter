using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneTransition : MonoBehaviour
{
    private ControllerManager controllerManager;

    private void Start()
    {
        controllerManager = GetComponent<ControllerManager>();
    }

    private void Update()
    {
        if(controllerManager._YButtonDown)
        {
            //DebugPlayScene();
        }
        if(controllerManager._XButtonDown)
        {
            TitleScene();
        }
        if(controllerManager._AButtonDown)
        {
            MainScene();
        }
        if(controllerManager._BButtonDown)
        {
            ResultScene();
        }
    }


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
