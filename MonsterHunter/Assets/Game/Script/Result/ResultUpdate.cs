/*リザルト結果の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUpdate : MonoBehaviour
{
    enum UIKinds
    {
        // 各表示の背景.
        TITLE, 
        RANK,
        RANK_TABLE,
        CLEAR_TIME,
        GUIDE

    }

    // クエスト終了時の情報.
    private HuntingEnd _huntingEnd;
    // リザルト画面のUI.
    public GameObject[] _ui;


    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        

        
    }

    void Update()
    {
        
    }

    


}
