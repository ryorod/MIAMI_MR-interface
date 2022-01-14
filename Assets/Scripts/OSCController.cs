using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCController : MonoBehaviour
{
    public string receiverIp = "127.0.0.1";
    public string serverId = "MusicVAE";
    public int    serverPort = 12000;
    public string M4LId = "M4L";
    public int M4LPort = 13000;

    public void Init()
    {
        OSCHandler.Instance.Init(this.serverId, this.receiverIp, this.serverPort);
    }

    public void Send(string adr, string msg)
    {
        OSCHandler.Instance.SendMessageToClient(this.serverId, adr, msg);
    }

    public void InitM4L()
    {
        OSCHandler.Instance.Init(this.M4LId, this.receiverIp, this.M4LPort);
    }

    public void SendM4L(string adr, string msg)
    {
        OSCHandler.Instance.SendMessageToClient(this.M4LId, adr, msg);
    }
}
