using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spell : MonoBehaviour {

    public enum SpellType{ForwordMovingSpell, AreaBlast, Wall, none};
	public SpellType spellType = SpellType.ForwordMovingSpell;
    public bool BossSpell;

    //The Image showen in the interface 128x128px
    public Texture2D InterfaceImg;

	public float speed = 10f;
    public float Range = 30f;
    public float Cooldown = 1f;

    //ForRange
    private float Distance = 0f;
    private Vector3 OldPos;

	public float Damage;
	public float Knockback;
    public float DeathTime = 0;

    //GameObecjt to spawn on hit;
    public GameObject DestroktionObject;

	public GameObject Caster{ get; set; }
    public Transform CastPoint{ get; set; }

    public DebuffScript Debuff;

    private float DisableTime = 0; //For AreaBlasts

	public ParameterScript.Parameters LevelParameters;
    private List<GameObject> PlayersInsideSpell = new List<GameObject>();
 
	void Start () {
        //ForRange
        OldPos = transform.position;

        CastPoint = Caster.transform;
        if (GameObject.FindObjectOfType<ParameterScript>() != null)
            LevelParameters = GameObject.FindObjectOfType<ParameterScript>().Parser;
        else
        {
            LevelParameters.XPos = Mathf.Infinity;
            LevelParameters.XNeg = Mathf.Infinity;
            LevelParameters.ZPos = Mathf.Infinity;
            LevelParameters.ZNeg = Mathf.Infinity;
        }

        //Sets Deathtime
        if(DeathTime != 0)
            DeathTime = Time.time + DeathTime;

        switch(spellType)
        {
            case SpellType.AreaBlast:
                DisableTime = Time.time + .1f;
                break;
            case SpellType.Wall:
                transform.Translate(Vector3.forward * 2f);
                break;
            default:
                break;
                
        }
	}


	// Update is called once per frame
	void Update () {
//		CheckIfOverBorders ();
        CheckDistance();
        CheckIfToLong();

		switch (spellType) {
		case SpellType.ForwordMovingSpell:
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
			break;
        case SpellType.AreaBlast:
            break;
        case SpellType.Wall:
            if(PlayersInsideSpell.Count != 0) 
            {
                for (int i = 0; i < PlayersInsideSpell.Count; i++) 
                {
                    if(PlayersInsideSpell[i] != null)
                        PlayersInsideSpell[i].GetComponent<PlayerInfo>().TakeDmg(Damage * Time.deltaTime);
                    else
                        PlayersInsideSpell.Remove(PlayersInsideSpell[i]);
                }
            }
            break;
		default:
			Debug.Log("Spell is missing a type");
			break;
		}
	}
	void CheckIfOverBorders()
	{
		if (transform.position.x > LevelParameters.XPos)
        {
            transform.position = new Vector3(LevelParameters.XNeg + .1f, transform.position.y, transform.position.z);
            OldPos = transform.position;
        } else if (transform.position.x < LevelParameters.XNeg)
        {
            transform.position = new Vector3(LevelParameters.XPos - .1f, transform.position.y, transform.position.z);
            OldPos = transform.position;
        } else if (transform.position.z > LevelParameters.ZPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, LevelParameters.ZNeg + .1f);
            OldPos = transform.position;
        } else if (transform.position.z < LevelParameters.ZNeg)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, LevelParameters.ZPos - .1f);
            OldPos = transform.position;
        }
	}

    void CheckDistance()
    {
        Distance += Vector3.Distance(transform.position, OldPos);
        OldPos = transform.position;
        if (Distance >= Range && Range != 0) 
            Destroy(this.gameObject);
    }

    void CheckIfToLong()
    {
        if(DeathTime != 0 && Time.time > DeathTime)
            Destroy(this.gameObject);
        if(DisableTime != 0 && Time.time > DisableTime)
            this.GetComponent<SphereCollider>().enabled = false;
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && other.gameObject != Caster)
        {
            //Specifik for Walls
            if (spellType == SpellType.Wall)
            {
                PlayersInsideSpell.Add(other.gameObject);
                return;
            }

            other.GetComponent<PlayerInfo>().TakeDmg(Damage);
            //Apply debuff if any
            if (Debuff != null)
            {
                DebuffScript ds = other.gameObject.AddComponent<DebuffScript>();
                ds.typeOfDebuff = Debuff.typeOfDebuff;
                ds.Durration = Debuff.Durration;
                ds.Power = Debuff.Power;
                if (Debuff.AddedEffect != null)
                    ds.AddedEffect = Debuff.AddedEffect;
            }

            //Knockback
            Vector3 KnockbackDir = (other.transform.position - CastPoint.position).normalized;
            other.GetComponent<PlayerMovement>().ApplyKnockBack(KnockbackDir, Knockback);

            if (DestroktionObject != null)
                Instantiate(DestroktionObject, transform.position, transform.rotation);

            if (spellType == SpellType.ForwordMovingSpell)
                Destroy(this.gameObject);
        } else if (other.tag == "Boss" && !BossSpell)
        {
            //Apply Damage
            other.GetComponent<BossInfo>().TakeDmg(Damage);

            //Apply Debuf if eny
            if(Debuff != null)
            {
                DebuffScript ds = other.gameObject.AddComponent<DebuffScript>();
                ds.typeOfDebuff = Debuff.typeOfDebuff;
                ds.Durration = Debuff.Durration;
                ds.Power = Debuff.Power;
                if (Debuff.AddedEffect != null)
                    ds.AddedEffect = Debuff.AddedEffect;
            }

            if (spellType == SpellType.ForwordMovingSpell)
                Destroy(this.gameObject);

        }
		if (other.tag == "Enviroment")
			Destroy (this.gameObject);
	}

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && other.gameObject != Caster)
        {
            if(spellType == SpellType.Wall)
            {
                PlayersInsideSpell.Remove(other.gameObject);
                return;
            }
        }
    }
}
