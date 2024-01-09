using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopManager : MonoBehaviour
{
    // �C���X�^���X.
    public static HitStopManager instance;

    void Start()
    {
        //instance = this;
        
        if(instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// �q�b�g�X�g�b�v�J�n.
    /// </summary>
    /// <param name="HitStopTime">�q�b�g�X�g�b�v����</param>
    public void StartHitStop(float HitStopTime)
    {
        instance.StartCoroutine(instance.HitStopCoroutine(HitStopTime));
    }

    private IEnumerator HitStopCoroutine(float HitStopTime)
    {
        // ���Ԓ�~�J�n.
        Time.timeScale = 0f;

        // �w�肵�����Ԃ܂Œ�~.
        yield return new WaitForSecondsRealtime(HitStopTime);

        //���Ԓ�~�I��.
        Time.timeScale = 1f;
    }
}
