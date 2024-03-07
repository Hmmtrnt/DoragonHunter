/*プレイヤー行動全体の管理*/

using UnityEngine;

public partial class PlayerState : MonoBehaviour
{
    void Start()
    {
        Init();
    }

    void Update()
    {
        UpdateProcess();
    }

    private void FixedUpdate()
    {
        FixedUpdateProcess();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ColEnter(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other);
    }
}