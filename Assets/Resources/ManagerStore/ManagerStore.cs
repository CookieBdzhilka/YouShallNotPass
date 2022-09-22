using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStore : MonoBehaviour
{
    public static ManagerStore managerStore { get; private set; }
    public GameOverManager gameOverManager { get; private set; }

    private void Awake()
    {
        CheckInstance();
        InitManagers();
    }

    private void CheckInstance()
    {
        if (managerStore == null)
        {
            managerStore = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitManagers()
    {
        gameOverManager = GetComponent<GameOverManager>();
    }
}
