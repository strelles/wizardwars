using UnityEngine;
using System.Collections;

public class DebuffScript : MonoBehaviour {

    public enum TypeOfDebuff{Slow, none};
    public TypeOfDebuff typeOfDebuff = TypeOfDebuff.none;
    public float Durration;
    public float Power;
    public GameObject AddedEffect;

    private float DestroyPoint;

	// Use this for initialization
	void Start () {
        if(this.gameObject.tag != "Player")
            return;

        DestroyPoint = Time.time + Durration;

        switch (typeOfDebuff)
        {
            case TypeOfDebuff.Slow:
                SlowPlayer();
                break;
            case TypeOfDebuff.none:
                break;
            default:
                break;
        }


        GameObject go = Instantiate(AddedEffect, transform.position + new Vector3(0f, 2f, 0f), AddedEffect.transform.rotation) as GameObject;
        DestroyAfter de = go.AddComponent<DestroyAfter>();
        de.TimeToDestroy = Time.time + Durration;
        go.transform.parent = this.transform;
	}

    void Update()
    {
        if(Time.time > DestroyPoint)
            RemoveEffect();
    }

    void RemoveEffect()
    {
        switch(typeOfDebuff)
        {
            case TypeOfDebuff.Slow:
                RemoveSlow();
                break;
            case TypeOfDebuff.none:
                break;
            default:
                break;
        }

        Destroy(this);
    }


    void SlowPlayer()
    {
        if(this.GetComponent<PlayerMovement>() != null)
            this.GetComponent<PlayerMovement>().Speed *= Power;
    }

    void RemoveSlow()
    {
        if(this.GetComponent<PlayerMovement>() != null)
            this.GetComponent<PlayerMovement>().Speed /= Power;
    }

}
