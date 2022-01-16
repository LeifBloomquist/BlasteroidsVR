using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class NetworkReceiver : MonoBehaviour
{
    private GameObject sphere;
    private Renderer spRenderer;
    private UdpClient udpServer;
    private IPEndPoint remoteEP;

    // Start is called before the first frame update
    void Start()
    {
        udpServer = new UdpClient(50000);
        remoteEP = new IPEndPoint(IPAddress.Any, 50000);

        sphere = GameObject.Find("InfoSphere");
        spRenderer = sphere.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color c = Color.black;

        if (udpServer.Available > 0)
        {
            c = new Color(1, 0, 0);
        }

        //Call SetColor using the shader property name "_Color" and setting the color to red
        spRenderer.material.SetColor("_Color", c);
    }
}
