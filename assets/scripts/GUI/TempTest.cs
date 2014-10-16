using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class TempTest : MonoBehaviour {

	void Awake()
    {
//        this.gameObject.GetComponent<UISpriteAnimation>()
    }
	
	// Update is called once per frame
	void Update () {
	    if (XCI.GetDPadDown(XboxDPad.Right))
            this.gameObject.GetComponent<UISpriteAnimation>().Play();
        if (XCI.GetDPadDown(XboxDPad.Left))
            this.gameObject.GetComponent<UISpriteAnimation>().Play(true);

	}
}
