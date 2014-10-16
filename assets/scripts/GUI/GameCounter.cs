using UnityEngine;
using System.Collections;

public class GameCounter : MonoBehaviour {
	
    private int Hours;
    private int Mins;

	// Update is called once per frame
	void Update () {
        if (GameState.instance.Ingame && !GameState.instance.Overtime)
        {
            Hours = (int)Mathf.Floor(GameState.instance.Playtime / 60);
            Mins =  (int)GameState.instance.Playtime - (Hours * 60);
            this.GetComponent<UILabel>().text = Hours.ToString() + " : " + Mins.ToString();
        }
	}
}
