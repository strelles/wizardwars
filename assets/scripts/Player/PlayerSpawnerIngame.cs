using UnityEngine;
using System.Collections;

public class PlayerSpawnerIngame : MonoBehaviour {

    public int PlayerNumber = 1;
    public GameObject Player;

	// Use this for initialization
	void Start () {
        Respawn();
	}

    public void Respawn()
    {
//        if (GameState.instance.playerInfo.Count >= PlayerNumber)
//        {
//            GameObject player = Instantiate(Player, transform.position, transform.rotation) as GameObject;
//            player.GetComponent<PlayerMovement>().PlayerNumber = GameState.instance.playerInfo[PlayerNumber - 1];
//            player.GetComponent<PlayerInfo>().PlayerNumber = PlayerNumber;
//            GameState.instance.AddAlivePLayer(player.GetComponent<PlayerInfo>());
//        }
        if(GameState.instance.CharacktorChoises[PlayerNumber - 1] != null)
        {
            GameObject player = Instantiate(GameState.instance.CharacktorChoises[PlayerNumber - 1], transform.position, transform.rotation) as GameObject;
            player.GetComponent<PlayerMovement>().PlayerNumber = GameState.instance.playerInfo[PlayerNumber - 1];
            player.GetComponent<PlayerInfo>().PlayerNumber = PlayerNumber;
            GameState.instance.AddAlivePLayer(player.GetComponent<PlayerInfo>());
        }
    }
}
