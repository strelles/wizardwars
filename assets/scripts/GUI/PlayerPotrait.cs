using UnityEngine;
using System.Collections;

public class PlayerPotrait : MonoBehaviour {

	public void ChangeSprite(Texture2D NewTex, string Name)
    {
        this.GetComponent<UITexture>().mainTexture = NewTex;
        this.GetComponentInChildren<UILabel>().text = Name;
    }
}
