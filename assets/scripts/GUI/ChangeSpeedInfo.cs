using UnityEngine;
using System.Collections;

public class ChangeSpeedInfo : MonoBehaviour {

    public void ChangeSpeed(float speed)
    {
        this.GetComponent<UILabel>().text = speed.ToString();
    }
}
