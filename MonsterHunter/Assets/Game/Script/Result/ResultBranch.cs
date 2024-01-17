/*リザルト結果によって処理を分岐*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBranch : MonoBehaviour
{
    // クリアリザルト.
    private GameObject _clearBranch;
    // ゲームオーバーリザルト.
    private GameObject _resultBranch;

    void Start()
    {
        _clearBranch = GameObject.Find("ClearResult").gameObject;
        _resultBranch = GameObject.Find("FailedResult").gameObject;
    }

    void Update()
    {
        
    }
}
