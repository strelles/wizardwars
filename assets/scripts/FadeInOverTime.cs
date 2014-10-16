using UnityEngine;
using System.Collections;

public class FadeInOverTime : MonoBehaviour {

    public float TimeToFade;

    private Color StartCol;
    private float CurrrentA = 0f;

    void Start()
    {
        StartCol = renderer.material.color;
        renderer.material.color = new Color(StartCol.r, StartCol.g, StartCol.b, 0);
    }

	// Update is called once per frame
	void Update () {
        CurrrentA += Time.deltaTime / TimeToFade;
        renderer.material.color = new Color(StartCol.r, StartCol.g, StartCol.b, CurrrentA);
        if (CurrrentA > 1f)
            Destroy(this.gameObject);
    }
}
