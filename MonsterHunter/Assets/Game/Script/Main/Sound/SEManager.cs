/*MainSceneSE�}�l�[�W���[*/

using UnityEngine;

public class SEManager : MonoBehaviour
{
    /*UI*/
    public enum UISE
    {
        SELECT,         // �I��UI�̈ړ�.
        DECISION,       // ����.
        REMOVE_PUSH,    // �߂�.
        QUEST_START,    // �N�G�X�g�X�^�[�g.
        STAMP_PUSH,     // �X�^���v������.
        QUEST_LIST,     // �N�G�X�g���X�g�̉�.
        SENUM           // SE��.
    }



    /*MainScene*/
    public enum HunterSE
    {
        SLASH,              // �a����.
        ROUNDSLASH,         // �C�n���]�a��.
        DAMAGE,             // �_���[�W.
        DRAWSWORD,          // ����.
        SHEATHINGSWORD,     // �[��.
        MISSINGSLASH,       // ��U��
        MISSINGROUNDSLASH,  // ��U��(��]�a��).
        DRINK,              // ����.
        FOOTSTEPLEFT,       // ����(��).
        FOOTSTEPRIGHT,      // ����(�E).
        BOUNCE,             // �e���ꂽ��
        SUCCESSCOUNTER, // �J�E���^�[�������̉�.

        SENUM               // SE��.
    }

    public enum MonsterSE
    {
        ROAR,           // ���K.
        FOOTSTEP,       // ����.
        FOOTSMALLSTEP,  // ����������.
        ROTATE,         // ��]���̖���.
        FALTER,         // ���ސ�.
        BITE,           // ���݂����̖���
        BLESS,          // �u���X.
        GROAN,          // �.
        DOWN,           // �ʂꂽ���̐�.

        SENUM           // SE��.
    }

    public enum AudioNumber
    {
        AUDIO2D,// �ǂ̋����ɂ��Ă���������.
        AUDIO3D,// �����ɉ����ĉ��ʕω�.

        AUDIONUM// AudioSource�̐�.
    }


    // SE�f��.
    // UI.
    [Header("UI�̌��ʉ�")]
    [SerializeField, EnumIndex(typeof(UISE))]
    public AudioClip[] _UiAudio;
    // �n���^�[.
    [Header("�n���^�[�̌��ʉ�")]
    [SerializeField, EnumIndex(typeof(HunterSE))]
    public AudioClip[] _hunterAudio;
    // �����X�^�[.
    [Header("�����X�^�[�̌��ʉ�")]
    [SerializeField, EnumIndex(typeof(MonsterSE))]
    public AudioClip[] _monsterAudio;

    // ����.
    private AudioSource[] _source = new AudioSource[(int)AudioNumber.AUDIONUM];

    void Start()
    {
        _source[0] = GameObject.Find("2DAudioSource").GetComponent<AudioSource>();
        _source[1] = GameObject.Find("3DAudioSource").GetComponent<AudioSource>();
    }

    void Update()
    {
        // �����邩�ǂ����m�F.
        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    _audioSource.PlayOneShot(_medicineConsume);
        //}
    }

    // UI��SE��炷.
    public void UIPlaySE(int Audio, int SENuber)
    {
        _source[Audio].PlayOneShot(_UiAudio[SENuber]);
    }

    // �v���C���[��SE��炷.
    public void HunterPlaySE(int Audio, int SENunber)
    {
        _source[Audio].PlayOneShot(_hunterAudio[SENunber]);
    }
    // �����X�^�[��SE��炷.
    public void MonsterPlaySE(int Audio, int SENunber)
    {
        _source[Audio].PlayOneShot(_monsterAudio[SENunber]);
    }
}
