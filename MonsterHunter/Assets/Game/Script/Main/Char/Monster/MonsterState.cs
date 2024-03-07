/*モンスターの行動全体の管理*/

using UnityEngine;

public partial class MonsterState : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdateProcess();
    }

    private void FixedUpdate()
    {
        FixedUpdateProcess();
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other);
    }
}
