using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.Rendering.Universal;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public TMP_Text textStatus;
    public TMP_InputField nameInput;
    public GameObject btnConect;

    void Start()
    {
        btnConect.SetActive(false);
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
        btnConect.SetActive(true);
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
        Debug.Log("Estamos conectados a la sala: " + PhotonNetwork.CurrentRoom.Name + " Bienvenido " + PhotonNetwork.NickName);
    }
}
