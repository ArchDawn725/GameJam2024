using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    int astroids = 0;
    bool bossActive = false;

    public UnityEvent OnBossDefeated;

    void Start()
    {
        Invoke("ReadyBoss", 1f);
    }

    void LateUpdate()
    {
        if (astroids == 0 && bossActive)
        {
            OnBossDefeated.Invoke();
        }
    }

    void ReadyBoss()
    {
        bossActive = true;
    }
    public void AddAstroid()
    {
        astroids++;
    }

    public void RemoveAstroid()
    {
        astroids--;
    }


}
