using UnityEngine;
using System.Collections;

public class Transport : MonoBehaviour {

    public enum TransportMode{Teleport, none};
    public TransportMode transportMode;

    public float Range;

	// Use this for initialization
	void Start () {
	    switch(transportMode)
        {
            case TransportMode.Teleport:
                Teleport();
                break;
            default:
                break;
        }
	}
	
	public void Teleport()
    {
        GameObject Player = this.GetComponent<Spell>().Caster;
        Player.transform.Translate(Vector3.forward * Range);
        if(this.GetComponent<Spell>().DestroktionObject != null)
            Instantiate(this.GetComponent<Spell>().DestroktionObject, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
