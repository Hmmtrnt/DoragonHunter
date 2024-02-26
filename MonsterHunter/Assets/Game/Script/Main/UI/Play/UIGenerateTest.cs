/*UIの生成*/

using UnityEngine;

public class UIGenerateTest : MonoBehaviour
{
    public Camera _camera;

    // UIを表示させる対象オブジェクト.
    public Transform _target;

    // 表示するUI.
    public Transform _targetUI;

    // オブジェクト位置のオフセット.
    public Vector3 _worldoffset;

    private RectTransform _rectTransform;

    void Start()
    {
        if(_camera == null)
        {
            _camera = Camera.main;
        }
        

        _rectTransform = _targetUI.parent.GetComponent<RectTransform>();
    }

    void Update()
    {
        OnUpdatePosition();
    }

    // UIの位置を更新.
    private void OnUpdatePosition()
    {
        var cameraTransform = _camera.transform;
        // カメラの向きベクトル.
        var cameraDir = cameraTransform.forward;
        // オブジェクトの位置.
        var targetWorldPos = _target.position + _worldoffset;
        // カメラからターゲットへのベクトル.
        var targetDir = targetWorldPos - cameraTransform.position;
        // 内積を使ってカメラ全貌かどうかを判定.
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // カメラ前方ならUI表示、後方なら非表示.
        _targetUI.gameObject.SetActive(isFront);
        if(!isFront) { return; }

        // オブジェクトのワールド座標からスクリーン座標変換.
        var targetScreenPos = _camera.WorldToScreenPoint(targetWorldPos);

        // スクリーン座標変換からUIローカル座標変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform,
            targetScreenPos,
            null,
            out var uiLocalPos
            );

        _targetUI.localPosition = uiLocalPos;


    }
}
