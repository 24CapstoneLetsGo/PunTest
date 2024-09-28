using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun, IPunObservable
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
        if (photonView.IsMine) // ���� �÷��̾ speechCount�� ������ų �� ����
        {
            speechCount++;
            Debug.Log($"New Speech Count: {speechCount}");
        }
        return speechCount;
    }

    // Photon PUN ����ȭ �޼���
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // ���� �÷��̾ �����͸� ������ ���� ��, speechCount ����
            stream.SendNext(speechCount);
        }
        else
        {
            // �ٸ� Ŭ���̾�Ʈ�κ��� �����͸� ���� ��, speechCount ����ȭ
            speechCount = (int)stream.ReceiveNext();
        }
    }
}
