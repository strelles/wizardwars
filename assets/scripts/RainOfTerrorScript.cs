using UnityEngine;
using System.Collections;

public class RainOfTerrorScript : MonoBehaviour {

    private ParameterScript.Parameters LevelParameters;
    public bool SystemObject = true;

    public GameObject FallingObj;
    public GameObject IndicationObj;
    public GameObject HittingGroundEffect;
    public GameObject HIttingPlayerEffect;

    public int Damage = 0;
    public float Knockback = 0f;
    
    public float Interval = 10f;
    public float IntervalMargin = 2f;
    public float TimeToHit = 4f;

    private float NextAttack;


	// Use this for initialization
	void Start () {
        if (SystemObject)
        {
            LevelParameters = GameObject.FindObjectOfType<ParameterScript>().Parser;
            NextAttack = Time.time + Random.Range(Interval - IntervalMargin, Interval + IntervalMargin);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (SystemObject)
        {
            //Cehcking if more then half of the time is gone before the game will start spawning Rain of terror 
            if (Time.time > NextAttack && GameState.instance.FirstTimePhase)
            {
                float x = Random.Range(LevelParameters.XNeg, LevelParameters.XPos);
                float z = Random.Range(LevelParameters.ZNeg, LevelParameters.ZPos);

                //Setting up Indication Object
                GameObject TempGO = Instantiate(IndicationObj, new Vector3(x, 2.1f, z), Quaternion.identity) as GameObject;
                TempGO.GetComponent<FadeInOverTime>().TimeToFade = TimeToHit;

                //Setting up Falling Object
                GameObject go = Instantiate(FallingObj, new Vector3(x, TimeToHit * 100f, z), Quaternion.identity) as GameObject;
                RainOfTerrorScript sc = go.AddComponent<RainOfTerrorScript>();
                sc.HittingGroundEffect = HittingGroundEffect;
                sc.Knockback = Knockback;
                sc.SystemObject = false;
                sc.IndicationObj = TempGO;

                //Calculating time for next attack
                NextAttack = Time.time + Random.Range(Interval - IntervalMargin, Interval + IntervalMargin);
            }
        } else
        {
            transform.Translate(Vector3.down * Time.deltaTime * 100f);
            if(transform.position.y <= 2.1f)
            {
                Instantiate(HittingGroundEffect,new Vector3(transform.position.x, 2.1f, transform.position.z), Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (!SystemObject && other.tag == "Player")
        {
            Debug.Log("Kncoking");
            Vector3 KnockbackDir = (new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z) - transform.position).normalized;
//            float Distance = Vector3.Distance(transform.position, other.transform.position);
            other.GetComponent<PlayerMovement>().ApplyKnockBack(KnockbackDir, Knockback);
            other.GetComponent<PlayerInfo>().TakeDmg(Damage);
        }
    }
}
