/*尾煙の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSmoke : MonoBehaviour
{
    // 生存時間
    private int _countLife;
    // モンスター情報.
    private Monster _monsterState;

    void Start()
    {
        _monsterState = GameObject.Find("Dragon").GetComponent<Monster>();

    }

    private void FixedUpdate()
    {
        _countLife++;

        // パーティクルを消去する.
        if (_countLife == 50)
        {
            Destroy(gameObject);
        }

        ParticlePositionDesignation();
    }

    // パーティクルの座標指定.
    private void ParticlePositionDesignation()
    {
        // それぞれの状態の情報によってパーティクルをモデルのメッシュに固定.
        if (_monsterState._currentRotateAttack)
        {
            transform.position = _monsterState._footSmokePosition[2].transform.position;
        }
    }
}
