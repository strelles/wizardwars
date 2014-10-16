using UnityEngine;
using System.Collections;

public class ScaleOverTime : MonoBehaviour {

    public bool x;
    public bool y; 
    public bool z;

    public float ScaleFactor;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float xS;
        float yS;
        float zS;
        if (x)
            xS = transform.localScale.x + (ScaleFactor * Time.deltaTime);
        else
            xS = transform.localScale.x;

        if (y)
            yS = transform.localScale.y + (ScaleFactor * Time.deltaTime);
        else
            yS = transform.localScale.y;

        if(z)
            zS = transform.localScale.z + (ScaleFactor * Time.deltaTime);
        else 
            zS = transform.localScale.z;

        transform.localScale = new Vector3(xS, yS, zS);
        
	}
}
