using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.Rendering.Universal;
using UnityEditor;
using System.Collections;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public TMP_Text textStatus;
    public TMP_InputField nameInput;
    public TMP_Text textNameRoom;
    public GameObject btnConect;
    public GameObject btnCreateRoom;
    public WindowHandler windowHandler;
    public Transform contentPlayer;

    private int countPlayer = 0;

    [Header("Prefabs")]
    public GameObject nicknamePlayer;

    void Start()
    {
        btnConect.SetActive(true);
        btnCreateRoom.SetActive(false);
        textStatus.text = "";
    }

    public void ConectPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            CreatePlayer();
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public void CreatePlayer()
    {
        string namePlayer = nameInput.text;
        Debug.Log("Nombre " + namePlayer);
        PhotonNetwork.NickName = namePlayer;
        Debug.Log("Nombre en el server " + PhotonNetwork.NickName);
    }

    public override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Hay internet!");
        textStatus.text = "Conectado a internet";
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Conectados al servidor!");
        textStatus.text = "Bienvenido "+ PhotonNetwork.NickName +" pues";
        btnConect.SetActive(false);
        btnCreateRoom.SetActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        
        textStatus.text = "Se desconecto por: "+ cause;
    }

    public void CreateRoom()
    {
        string user = PhotonNetwork.NickName;
        string nameRoom = "Room1";

        RoomOptions optionRoom = new RoomOptions();
        optionRoom.IsVisible = true;
        optionRoom.MaxPlayers = 2;
        optionRoom.PublishUserId = true;

        PhotonNetwork.JoinOrCreateRoom(nameRoom, optionRoom, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        textNameRoom.text = PhotonNetwork.CurrentRoom.Name;
        windowHandler.enabledWindow(2);
        StartCoroutine(UpdateRoomTexts());
        Debug.Log("Estamos conectados a la sala: " + PhotonNetwork.CurrentRoom.Name + " Bienvenido " + PhotonNetwork.NickName);
    }

    IEnumerator UpdateRoomTexts()
    {
        yield return new WaitForSeconds(0.2f);

        if (PhotonNetwork.CurrentRoom.PlayerCount != countPlayer)
        {
            countPlayer = PhotonNetwork.CurrentRoom.PlayerCount;
            foreach (var item in PhotonNetwork.CurrentRoom.Players)
            {
                GameObject nickname = Instantiate(nicknamePlayer, contentPlayer);
                nickname.GetComponent<TMP_Text>().text = item.Value.NickName;
            }
        }
        
        
    }
}
