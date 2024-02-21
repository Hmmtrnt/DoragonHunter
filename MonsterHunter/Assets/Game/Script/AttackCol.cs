/*プレイヤーの攻撃判定*/

using System.Buffers;
using System.Collections;
using System.Collections.Generic;
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

    private Player _state;
    private PlayerHitStopManager _hitStop;
    private SEManager _seManager;
    // 攻撃を当てた時の流血エフェクトプレハブ取得.
    GameObject _bloodEffectObject;
    // 弱い攻撃ヒットエフェクトのプレハブ取得.
    GameObject _smallHitEffectObject;
    // 強い攻撃ヒットエフェクトのプレハブ取得.
    GameObject _hardHitEffectObject;
    // 攻撃ヒットエフェクトの生成位置.
    GameObject _hitEffectPosition;

    // 攻撃ヒットエフェクトのポケット.
    GameObject _hitEffectPocket = null;

    // 処理を複数同時に行わないようにするための変数.
    public bool _isOneProcess = true;

    void Start()
    {
        _state = GameObject.Find("Hunter").GetComponent<Player>();
        _hitStop = GameObject.Find("HitStopManager").GetComponent<PlayerHitStopManager>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        // 攻撃ヒットエフェクトのプレハブ取得.
        _bloodEffectObject = (GameObject)Resources.Load("Blood2");
        _smallHitEffectObject = (GameObject)Resources.Load("SmallHitEffect");
        _hardHitEffectObject = (GameObject)Resources.Load("HardHitEffect");
        _hitEffectPosition = GameObject.Find("EffectSpawnPosition");
        _isOneProcess = true;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 同時に処理させないために一度しか処理を通さない.
        if (!_isOneProcess) return;

        if (other.gameObject.tag == "MonsterHead")
        {
            CauseDamageUpdate(1.2f, (int)Fleshy.HEAD);
        }
        else if(other.gameObject.tag == "MonsterBody")
        {
            CauseDamageUpdate(1.0f, (int)Fleshy.BODY);
        }
        else if (other.gameObject.tag == "MonsterWingRight")
        {
            CauseDamageUpdate(0.9f, (int)Fleshy.WINGRIGHT);
        }
        else if( other.gameObject.tag == "MonsterWingLeft")
        {
            CauseDamageUpdate(0.9f, (int)Fleshy.WINGLEFT);
        }
        else if(other.gameObject.tag == "MonsterTail")
        {
            CauseDamageUpdate(1.1f, (int)Fleshy.TAIL);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    // ダメージを与えた瞬間、肉質を変化やヒットストップ追加.
    // TODO:変数名が決まってない.
    /// <summary>
    /// ダメージを与えた瞬間に更新する
    /// </summary>
    /// <param name="FleshyMagnification">モンスターの肉質倍率</param>
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

        if(_state.GetRoundSlash())
        {
            _hitEffectPocket = _hardHitEffectObject;
        }
        else
        {
            _hitEffectPocket = _smallHitEffectObject;
        }

        AttackEffectSpawn(_hitEffectPocket, FleshyNumber);

        _isOneProcess = false;
    }

    /// <summary>
    /// 攻撃SEを流す.
    /// </summary>
    /// <param name="SENumber">SEの番号</param>
    private void SEPlay(int SENumber)
    {
        // 気大回転刃斬りの時音を変更.
        //if (_state.GetRoundSlash())
        //{
        //    _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.HunterSE.ROUNDSLASH);
        //}
        //else
        //{
        //    _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.HunterSE.SLASH);

        //}

        _seManager.HunterPlaySE((int)SEManager.AudioNumber.AUDIO2D, SENumber);
    }

    /// <summary>
    /// 攻撃ヒット時エフェクトを生成.
    /// </summary>
    /// <param name="objectName">パーティクルを生成するヒットエフェクト</param>
    /// <param name="FleshyNumber">肉質の番号</param>
    private void AttackEffectSpawn(GameObject objectName, int FleshyNumber)
    {
        // 生成.
        Instantiate(objectName, _hitEffectPosition.transform.position, Quaternion.identity);
        if (GetSoftFleshy(FleshyNumber))
        {
            Instantiate(_bloodEffectObject, _hitEffectPosition.transform.position, Quaternion.identity);
        }
        
    }

    // 肉質番号をだいにゅ
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
            (FleshyNumber == (int)Fleshy.BODY) ||
            (FleshyNumber == (int)Fleshy.TAIL);

        return softFleshy;
    }
}
