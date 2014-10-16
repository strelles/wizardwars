using UnityEngine;
using System.Collections;

public class CharacterSelecter : MonoBehaviour {

    public GameObject[] Selectors;
    private string[] LockoutName = new string[4];

	// Use this for initialization
	void Start () {
	    foreach(GameObject S in Selectors)
            S.SetActive(false);
	}

    public void SetSelectorActive(int player)
    {
        int ActuralPlayer = player;
        Selectors[ActuralPlayer].SetActive(true);
//        GameState.instance.CharacktorChoises[ActuralPlayer] = this.gameObject;
    }

    public void SetSelectorUnActive(int player)
    {
        int ActuralPlayer = player;
        Selectors[ActuralPlayer].SetActive(false);
    }

    public void LockInChar(int player)
    {
        int ActuralPlayer = player;
        LockoutName[player] = Selectors[ActuralPlayer].GetComponent<UISprite>().spriteName;
        Selectors [ActuralPlayer].GetComponent<UISprite>().spriteName = LockoutName[player] + "_Chosen";
    }

    public void LockOutChar(int player)
    {
        if(LockoutName != null)
        {
            int ActuralPlayer = player; 
            Selectors[ActuralPlayer].GetComponent<UISprite>().spriteName = LockoutName[player];
            LockoutName[player] = null;
        }
    }
	
}
