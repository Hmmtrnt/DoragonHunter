/*��������UI�̏���*/

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationUI : MonoBehaviour
{
    // �����ւ���摜�̎��
    enum ReplaceImageType
    {
        SHEATHING, // �[����.
        UNSHEATHED,// ������.
    }

    // �`�悷��摜�̎��
    enum DrawImageType
    {
        INPUT_EXPLANATION,// ���͏��.
        COMBO_EXPLANATION,// �R���{���.
        MAX_NUM
    }

    // �摜
    [Header("�摜�f��")]
    [SerializeField, EnumIndex(typeof(ReplaceImageType))]
    public Sprite[] _sprites;

    // �����摜.
    [Header("�����摜")]
    [SerializeField, EnumIndex(typeof(DrawImageType))]
    public Image[] _canvasUI;
    [Header("�����摜�I�u�W�F�N�g")]
    [SerializeField, EnumIndex(typeof(DrawImageType))]
    public GameObject[] _canvasUIObject;

    // �v���C���[���.
    private PlayerState _playerState;
    // ���C���V�[���̏��.
    private HuntingSceneManager _huntingSceneManager;
    // �`�悷�邩�ǂ���.
    private bool _drawCanvas = true;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _huntingSceneManager = GameObject.Find("GameManager").GetComponent<HuntingSceneManager>();
    }

    void Update()
    {
        DrawImage();
        // ����UI�̕`����s���Ă���Ƃ��ɏ���.
        if (_drawCanvas)
        {
            ChangeImage();
        }
    }

    // ����UI�̉摜��ύX.
    private void ChangeImage()
    {
        // �����A�[����Ԃɂ���ĕ`�悷��摜��ύX����.
        if (!_playerState.GetIsUnsheathedSword())
        {
            // �[��.
            _canvasUI[(int)DrawImageType.INPUT_EXPLANATION].sprite = _sprites[(int)ReplaceImageType.SHEATHING];
        }
        else
        {
            // ����.
            _canvasUI[(int)DrawImageType.INPUT_EXPLANATION].sprite = _sprites[(int)ReplaceImageType.UNSHEATHED];
        }
    }

    // ����UI��`�悷�邩�ǂ���.
    private void DrawImage()
    {
        for (int i = 0; i < (int)DrawImageType.MAX_NUM; i++)
        {
            _canvasUIObject[i].SetActive(_drawCanvas);
        }

        if (_huntingSceneManager._openMenu)
        {
            _drawCanvas = false;
        }
        else
        {
            _drawCanvas = true;
        }
    }

}
