using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region SingleTon Pattern
    public static UIManager instance;  // Singleton instance

    void Awake() // SingleTon
    {
        // �̹� �ν��Ͻ��� �����ϸ鼭 �̰� �ƴϸ� �ı� ��ȯ
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // Set the instance to this object and make sure it persists between scene loads
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    // �α׾ƿ� ��ư�� ������ �� ȣ��� �޼��� - ������ �ӽ÷� ���� ����� �����ص� 
    public void OnLogoutButtonPressed()
    {
        // ���� ���� ����
        Debug.Log("Logging out and quitting the game.");
        Application.Quit();

        // �����Ϳ��� ���� ���� �� ���� ���Ḧ �ùķ��̼�
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
