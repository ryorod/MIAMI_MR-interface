using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCController : MonoBehaviour
{
    public string serverId = "MusicVAE";
    public string serverIp = "127.0.0.1";
    public int    serverPort = 12000;

    public void Init()
    {
        OSCHandler.Instance.Init(this.serverId, this.serverIp, this.serverPort);
    }

    public void Send(string adr, string msg)
    {
        OSCHandler.Instance.SendMessageToClient(this.serverId, adr, msg);
    }
}
