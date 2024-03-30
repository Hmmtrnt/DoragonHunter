/*カメラの座標関係の処理*/
// 参考サイトURL.
// https://tat1kun.hatenablog.com/entry/chinemachine_fixedPos#Y%E8%BB%B8%E5%9B%BA%E5%AE%9A%E3%81%AE%E3%81%BF%E3%81%AE%E7%B0%A1%E5%8D%98%E3%81%AA%E3%82%B3%E3%83%BC%E3%83%89

using Cinemachine;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")]
public class LockAxisCamera : CinemachineExtension
{
    // 度の座標を固定するか.
    public bool _lockX;
    public bool _lockY;
    public bool _lockZ;
    // 固定するときの座標
    public Vector3 _lockPosition;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var newPos = state.RawPosition;
            if (_lockX) newPos.x = _lockPosition.x;
            if (_lockY) newPos.y = _lockPosition.y;
            if (_lockZ) newPos.z = _lockPosition.z;
            state.RawPosition = newPos;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LockAxisCamera))]
public class LockAxisCameraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var lockAxisCamera = target as LockAxisCamera;
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUIUtility.labelWidth = 10;
            EditorGUILayout.LabelField("固定する軸");
            lockAxisCamera._lockX = EditorGUILayout.Toggle("X", lockAxisCamera._lockX);
            lockAxisCamera._lockY = EditorGUILayout.Toggle("Y", lockAxisCamera._lockY);
            lockAxisCamera._lockZ = EditorGUILayout.Toggle("Z", lockAxisCamera._lockZ);
        }
        EditorGUILayout.LabelField("固定する座標");
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUIUtility.labelWidth = 10;
            lockAxisCamera._lockPosition.x = EditorGUILayout.FloatField("X", lockAxisCamera._lockPosition.x);
            lockAxisCamera._lockPosition.y = EditorGUILayout.FloatField("Y", lockAxisCamera._lockPosition.y);
            lockAxisCamera._lockPosition.z = EditorGUILayout.FloatField("Z", lockAxisCamera._lockPosition.z);
        }
    }
}
#endif