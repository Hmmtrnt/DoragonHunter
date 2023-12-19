using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCol : MonoBehaviour
{
    private PlayerState _state;

    // Start is called before the first frame update
    void Start()
    {
        _state = GameObject.Find("Hunter").GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(/*_state._isCauseDamage &&*/ other.gameObject.tag == "Monster")
        {
            Debug.Log("当たった");

            _state._isCauseDamage = false;

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        //if(other.gameObject.tag == "Monster")
        //{
        //    Debug.Log("通る");
        //    _col.enabled = true;
        //}
    }
}
