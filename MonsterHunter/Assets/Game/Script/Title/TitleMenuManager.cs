/*�^�C�g����ʂ�UI�S�̂̐���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuManager : MonoBehaviour
{
    private GameObject _optionMenu;
    // �ݒ��ʂ��J���Ă��邩�ǂ���.
    public bool _openOption = false;

    // Start is called before the first frame update
    void Start()
    {
        _optionMenu = GameObject.Find("OptionMenu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _optionMenu.SetActive(_openOption);
    }
}
