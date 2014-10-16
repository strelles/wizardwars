using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SinglePlayerCameraScript : MonoBehaviour {

    public List<GameObject> Players = new List<GameObject>();
    public Vector3 Offset = new Vector3(0, 15f, 9f);
    public bool Cinematic;

	// Use this for initialization
	void Start () {
	    foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            Players.Add(g);
  	}
	
	// Update is called once per frame
	void Update () {
        if (!Cinematic)
        {
            Vector3 Avarage = GetPlayerAverage(Players);
            transform.position = Avarage + Offset;
        }
	}

    Vector3 GetPlayerAverage(List<GameObject> players)
    {
        Vector3 Total = Vector3.zero;
        int i = 0;
        foreach(GameObject player in players)
        {
            Total += player.transform.position;
            i++;
        }

        return Total / i;
    }

}
