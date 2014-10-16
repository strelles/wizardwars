using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

    public float TimeToDestroy = 0;

    void Start()
    {
        if(TimeToDestroy == 0)
            TimeToDestroy = Time.time + 1f;
    }

	// Update is called once per frame
	void Update () {
        if(Time.time > TimeToDestroy)
            Destroy(this.gameObject);
	}
}
