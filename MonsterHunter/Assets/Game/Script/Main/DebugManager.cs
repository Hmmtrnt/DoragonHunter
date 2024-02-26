/*デバッグ用マネージャー*/

using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private GameObject _Monster;
    private MonsterState _monsterState;
    [SerializeField] private Vector3 _respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        _monsterState = _Monster.GetComponent<MonsterState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _monsterState.SetHitPoint(300.0f);
            _Monster.transform.position = _respawnPosition;
            _Monster.SetActive(true);
        }
    }
}
