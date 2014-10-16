using UnityEngine;
using System.Collections;

public class LookatScript : MonoBehaviour {

    public Transform LookatPoint;
    public Camera LookatCam;

    void Start()
    {
        if (LookatCam == null)
            LookatCam = Camera.main;
    }

	// Update is called once per frame
	void Update () {
        transform.LookAt(LookatCam.transform);
	}
}
