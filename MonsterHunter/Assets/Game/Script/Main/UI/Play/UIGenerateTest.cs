/*UI�̐���*/

using UnityEngine;

public class UIGenerateTest : MonoBehaviour
{
    public Camera _camera;

    // UI��\��������ΏۃI�u�W�F�N�g.
    public Transform _target;

    // �\������UI.
    public Transform _targetUI;

    // �I�u�W�F�N�g�ʒu�̃I�t�Z�b�g.
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

    // UI�̈ʒu���X�V.
    private void OnUpdatePosition()
    {
        var cameraTransform = _camera.transform;
        // �J�����̌����x�N�g��.
        var cameraDir = cameraTransform.forward;
        // �I�u�W�F�N�g�̈ʒu.
        var targetWorldPos = _target.position + _worldoffset;
        // �J��������^�[�Q�b�g�ւ̃x�N�g��.
        var targetDir = targetWorldPos - cameraTransform.position;
        // ���ς��g���ăJ�����S�e���ǂ����𔻒�.
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��.
        _targetUI.gameObject.SetActive(isFront);
        if(!isFront) { return; }

        // �I�u�W�F�N�g�̃��[���h���W����X�N���[�����W�ϊ�.
        var targetScreenPos = _camera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W�ϊ�����UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform,
            targetScreenPos,
            null,
            out var uiLocalPos
            );

        _targetUI.localPosition = uiLocalPos;


    }
}
