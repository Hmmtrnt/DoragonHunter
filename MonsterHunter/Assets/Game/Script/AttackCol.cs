/*プレイヤーの攻撃判定*/

using UnityEngine;

public class AttackCol : MonoBehaviour
{
    // 肉質番号.
    enum Fleshy
    {
        HEAD,       // 頭.
        BODY,       // 胴.
        WINGRIGHT,  // 右翼.
        WINGLEFT,   // 左翼.
        TAIL,       // 尾.
        MAX_NUM
    }

    private PlayerState _state;
    private PlayerHitStopManager _hitStop;
    private HuntingEnd _end;
    private SEManager _seManager;
    // 攻撃を当てた時の流血エフェクトプレハブ取得.
    GameObject _bloodEffectObject;
    // 流血エフェクトの大きさ.
    Vector3 _bloodEffectScale = new Vector3(10.0f,10.0f,10.0f);

    // 普通の攻撃ヒットエフェクトのプレハブ取得.
    GameObject _smallHitEffectObject;
    // 大回転斬りの攻撃ヒットエフェクトのプレハブ取得.
    GameObject _hardHitEffectObject;
    // 必殺技の攻撃ヒットエフェクトのプレハブ取得.
    GameObject _greatHitEffectObject;
    // ダメージ表記.
    public GameObject _DamageUiObject;
    // ダメージ表記のプレハブ.
    GameObject _DamageUiPrefab;
    public RectTransform _damageUiRectTransform;

    //エフェクトの生成位置.
    GameObject _EffectPosition;

    // 攻撃ヒットエフェクトのポケット.
    GameObject _hitEffectPocket = null;

    public GameObject _canvas;

    // 処理を複数同時に行わないようにするための変数.
    public bool _isOneProcess = true;

    void Start()
    {
        _state = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _hitStop = GameObject.Find("HitStopManager").GetComponent<PlayerHitStopManager>();
        _end = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        // 攻撃ヒットエフェクトのプレハブ取得.
        _bloodEffectObject = (GameObject)Resources.Load("Blood");
        _smallHitEffectObject = (GameObject)Resources.Load("SmallHitEffect");
        _hardHitEffectObject = (GameObject)Resources.Load("HardHitEffect");
        _greatHitEffectObject = (GameObject)Resources.Load("GreatHitEffect");
        _EffectPosition = GameObject.Find("EffectSpawnPosition");
        _isOneProcess = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 同時に処理させないために一度しか処理を通さない.
        if (!_isOneProcess) return;

        if (other.gameObject.tag == "MonsterHead")
        {
            CauseDamageUpdate(1.3f, (int)Fleshy.HEAD);
        }
        else if(other.gameObject.tag == "MonsterBody")
        {
            CauseDamageUpdate(0.4f, (int)Fleshy.BODY);
        }
        else if (other.gameObject.tag == "MonsterWingRight")
        {
            CauseDamageUpdate(1.1f, (int)Fleshy.WINGRIGHT);
        }
        else if( other.gameObject.tag == "MonsterWingLeft")
        {
            CauseDamageUpdate(1.1f, (int)Fleshy.WINGLEFT);
        }
        else if(other.gameObject.tag == "MonsterTail")
        {
            CauseDamageUpdate(0.5f, (int)Fleshy.TAIL);
        }
    }

    // ダメージを与えた瞬間、肉質を変化やヒットストップ追加.
    // TODO:変数名が決まってない.
    /// <summary>
    /// ダメージを与えた瞬間に更新する.
    /// </summary>
    /// <param name="FleshyMagnification">モンスターの肉質倍率</param>
    /// <param name="FleshyNumber">モンスターの肉質番号</param>
    private void CauseDamageUpdate(float FleshyMagnification, int FleshyNumber)
    {
        _state._MonsterFleshy = FleshyMagnification;
        _state.RenkiGaugeFluctuation();
        _state._weaponActive = false;
        // プレイヤーのヒットストップ.
        _hitStop.StartHitStop(_state._hitStopTime);

        // 攻撃が通った場合.
        if (GetSoftFleshy(FleshyNumber))
        {
            SEPlay((int)SEManager.HunterSE.SLASH);
        }
        // 攻撃が弾かれた場合.
        else if(!GetSoftFleshy(FleshyNumber))
        {
            SEPlay((int)SEManager.HunterSE.BOUNCE);
        }

        // 当てた攻撃に応じてエフェクトを変更.
        if(_state.GetRoundSlash())
        {
            _hitEffectPocket = _hardHitEffectObject;
        }
        else if (_state.GetGreatAttackSuccess())
        {
            _hitEffectPocket = _greatHitEffectObject;
        }
        else
        {
            _hitEffectPocket = _smallHitEffectObject;
        }

        EffectSpawn(_hitEffectPocket, FleshyNumber);

        if (!_end.GetQuestEnd())
        {
            DamageUISpawn();
        }
        

        _isOneProcess = false;
    }

    /// <summary>
    /// 攻撃SEを流す.
    /// </summary>
    /// <param name="SENumber">SEの番号</param>
    private void SEPlay(int SENumber)
    {
        _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, SENumber);
    }

    /// <summary>
    /// 攻撃ヒット時エフェクトを生成.
    /// </summary>
    /// <param name="objectName">パーティクルを生成するヒットエフェクト</param>
    /// <param name="FleshyNumber">肉質の番号</param>
    private void EffectSpawn(GameObject objectName, int FleshyNumber)
    {
        // 生成.
        Instantiate(objectName, _EffectPosition.transform.position, Quaternion.identity);
        if (GetSoftFleshy(FleshyNumber))
        {
            Instantiate(_bloodEffectObject, _EffectPosition.transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// UIのプレハブ生成
    /// </summary>
    private void DamageUISpawn()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_EffectPosition.transform.position);

        _DamageUiPrefab = Instantiate(_DamageUiObject);

        _DamageUiPrefab.transform.SetParent(_canvas.transform, false);
    }

    /// <summary>
    /// 肉質の番号を取得し、
    /// trueであれば攻撃が通り
    /// falseであれば攻撃が弾かれる.
    /// </summary>
    /// <param name="FleshyNumber">肉質の番号</param>
    /// <returns>肉質が柔らかいかどうか</returns>

    private bool GetSoftFleshy(int FleshyNumber)
    {
        bool softFleshy = (FleshyNumber == (int)Fleshy.HEAD) ||
            (FleshyNumber == (int)Fleshy.WINGRIGHT) ||
            (FleshyNumber == (int)Fleshy.WINGLEFT);

        return softFleshy;
    }
}
