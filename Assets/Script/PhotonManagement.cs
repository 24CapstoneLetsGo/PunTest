using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManagement : MonoBehaviourPunCallbacks
{

    //���� �Է�
    private readonly string version = "0.1f";
    // ����� ���̵� �Է�
    private string userId = "Mary";

    private void Awake()
    {
        //���� ���� �����鿡�� �ڵ����� ���� �ε�
        PhotonNetwork.AutomaticallySyncScene = true;
        //���� ������ �������� ���� ��� 
        PhotonNetwork.GameVersion = version;
        //���� ���̵� �Ҵ�
        PhotonNetwork.NickName = userId;

        //���� ������ ��� Ƚ�� ����. �ʴ� 30ȸ
        Debug.Log(PhotonNetwork.SendRate);

        //���� ����
        PhotonNetwork.ConnectUsingSettings();
    }

    //���� ������ ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby(); // �κ� ���� 
    }

    //�κ� ���� �� ȣ��Ǵ� �ݹ��Լ�
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom(); // ���� ��ġ����ŷ ��� ���� 
    }

    // ���� �� ���� ���н� ȣ��Ǵ� �Լ�
    public override void OnJoinRandomFailed(short returncode, string message)
    {
        Debug.Log($"JoinRandom Failed {returncode}:{message}");


        // ���� �Ӽ� ���� 
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20; // �ִ� ������ �� 20��
        ro.IsOpen = true; // ���� ���� ����
        ro.IsVisible = true; // �κ񿡼� �� ��Ͽ� ���� ��ų ������ ���� 

        PhotonNetwork.CreateRoom("My room", ro);

    }

    // �� ���� �� �ݹ� �Լ� 
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room name = {PhotonNetwork.CurrentRoom.Name}");
    }

    //�� ���� �� �ݹ� �Լ�
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player count = {PhotonNetwork.CurrentRoom.PlayerCount}");


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
