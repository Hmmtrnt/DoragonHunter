/*タイトル画面のUI全体の制御*/

using UnityEngine;

public class TitleMenuManager : MonoBehaviour
{
    private GameObject _optionMenu;
    // 設定画面を開いているかどうか.
    public bool _openOption = false;

    void Start()
    {
        _optionMenu = GameObject.Find("OptionMenu").gameObject;
    }

    private void FixedUpdate()
    {
        _optionMenu.SetActive(_openOption);
    }
}
