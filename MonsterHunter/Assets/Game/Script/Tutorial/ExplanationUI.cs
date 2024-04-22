/*説明するUIの処理*/

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationUI : MonoBehaviour
{
    // 差し替える画像の種類
    enum ReplaceImageType
    {
        SHEATHING, // 納刀時.
        UNSHEATHED,// 抜刀時.
    }

    // 描画する画像の種類
    enum DrawImageType
    {
        INPUT_EXPLANATION,// 入力情報.
        COMBO_EXPLANATION,// コンボ情報.
        MAX_NUM
    }

    // 画像
    [Header("画像素材")]
    [SerializeField, EnumIndex(typeof(ReplaceImageType))]
    public Sprite[] _sprites;

    // 説明画像.
    [Header("説明画像")]
    [SerializeField, EnumIndex(typeof(DrawImageType))]
    public Image[] _canvasUI;
    [Header("説明画像オブジェクト")]
    [SerializeField, EnumIndex(typeof(DrawImageType))]
    public GameObject[] _canvasUIObject;

    // プレイヤー情報.
    private PlayerState _playerState;
    // メインシーンの情報.
    private HuntingSceneManager _huntingSceneManager;
    // 描画するかどうか.
    private bool _drawCanvas = true;

    void Start()
    {
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _huntingSceneManager = GameObject.Find("GameManager").GetComponent<HuntingSceneManager>();
    }

    void Update()
    {
        DrawImage();
        // 説明UIの描画を行っているときに処理.
        if (_drawCanvas)
        {
            ChangeImage();
        }
    }

    // 説明UIの画像を変更.
    private void ChangeImage()
    {
        // 抜刀、納刀状態によって描画する画像を変更する.
        if (!_playerState.GetIsUnsheathedSword())
        {
            // 納刀.
            _canvasUI[(int)DrawImageType.INPUT_EXPLANATION].sprite = _sprites[(int)ReplaceImageType.SHEATHING];
        }
        else
        {
            // 抜刀.
            _canvasUI[(int)DrawImageType.INPUT_EXPLANATION].sprite = _sprites[(int)ReplaceImageType.UNSHEATHED];
        }
    }

    // 説明UIを描画するかどうか.
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
