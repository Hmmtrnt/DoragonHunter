/*�K�C�h��UI*/

using UnityEngine;

public class GuideUI : MonoBehaviour
{
    private RectTransform _rectTransform;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // ���g�̐��ʂ��J�����Ɍ�����.
        _rectTransform.LookAt(Camera.main.transform);
    }
}
