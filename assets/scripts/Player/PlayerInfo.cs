using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

    public int PlayerNumber;

	public float MaxHP = 100;
    public float HP;
    public HealthBarScript HpBar;
    public TurnIndicatorColorSwitch TurnIndicator;

    public Texture2D CharakterImg;
    public GameObject SelectionObject;
    public string Name;

    public float LavaDmg = 1;
    private bool InLava = false;

	void Start()
	{
		HP = MaxHP;
        if (TurnIndicator != null)
            TurnIndicator.ChangeColor(PlayerNumber);
	}

    void Update()
    {
        if (InLava)
            TakeDmg(LavaDmg * Time.deltaTime);
    }

	public void TakeDmg(float dmg)
	{
        if(!GameState.instance.IsPaused)
        {
            HP -= dmg;
            HpBar.SetScale(MaxHP, HP);
            if (HP <= 0)
    			Die ();
        }
	}

	void Die()
	{
        GameState.instance.RemoveAlivePlayer(this);
        Debug.Log("He is dead");
		Destroy (this.gameObject);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lava")
            InLava = true;
        if (other.tag == "LavaSphere")
            InLava = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Lava")
            InLava = false;
        if (other.tag == "LavaSphere")
            InLava = true;
    }
}
