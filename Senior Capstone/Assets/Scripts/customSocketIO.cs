using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;


public class customSocketIO : MonoBehaviour {
    
    
	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("SocketIO");
        SocketIOComponent socket = go.GetComponent<SocketIOComponent>();
        Debug.Log(socket);

        socket.On("connected", (SocketIOEvent e) =>
        {
            Debug.Log(string.Format("[name: {0}, data: {1}]", e.name, e.data));
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
