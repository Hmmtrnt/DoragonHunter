/*�^�C�g����ʂ�UI�S�̂̐���*/

using UnityEngine;

public class TitleMenuManager : MonoBehaviour
{
    private GameObject _optionMenu;
    // �ݒ��ʂ��J���Ă��邩�ǂ���.
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
