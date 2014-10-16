using UnityEngine;
using System.Collections;

public class ChangeHPInfo : MonoBehaviour {

	public void ChangeHp(float hp)
    {
        this.GetComponent<UILabel>().text = hp.ToString();
    }
}
