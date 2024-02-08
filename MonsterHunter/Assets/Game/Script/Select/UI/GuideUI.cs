/*ガイドのUI*/

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
        // 自身の正面をカメラに向ける.
        _rectTransform.LookAt(Camera.main.transform);
    }
}
