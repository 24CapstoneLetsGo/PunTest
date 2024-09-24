using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SingleTon Pattern
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Set this as the instance and ensure it persists across scenes
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    // �߾� ��ȣ ī����
    private int speechCount = 0;

    // �߾� ��ȣ ��ȯ �� ī���� ����
    public int GetNextSpeechCount()
    {
        speechCount++;
        return speechCount;
    }


}
