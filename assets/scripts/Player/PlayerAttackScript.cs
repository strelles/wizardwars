using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class PlayerAttackScript : MonoBehaviour {

    public GameObject Spell1;
    public GameObject Spell2;
    public GameObject Spell3;
    public GameObject Spell4;

    private float Spell1Casttime;
    private float Spell2Casttime;
    private float Spell3Casttime; 
    private float Spell4Casttime;


	public int PlayerNumber;
    private int ActuralPlayerNumber;


	// Use this for initialization
	void Start () {
		PlayerNumber = this.gameObject.GetComponent<PlayerMovement>().PlayerNumber;
        ActuralPlayerNumber = this.gameObject.GetComponent<PlayerInfo>().PlayerNumber;
        if(PlayerNumber == 0)
            return;

        if(Spell1 != null) GameGui.instance.SpellHolder[ActuralPlayerNumber * 4 - 4].GetComponent<UISprite>().spriteName = Spell1.GetComponent<Spell>().InterfaceImg.name;
        if(Spell2 != null) GameGui.instance.SpellHolder[ActuralPlayerNumber * 4 - 3].GetComponent<UISprite>().spriteName = Spell2.GetComponent<Spell>().InterfaceImg.name;
        if(Spell3 != null) GameGui.instance.SpellHolder[ActuralPlayerNumber * 4 - 2].GetComponent<UISprite>().spriteName = Spell3.GetComponent<Spell>().InterfaceImg.name;
        if(Spell4 != null) GameGui.instance.SpellHolder[ActuralPlayerNumber * 4 - 1].GetComponent<UISprite>().spriteName = Spell4.GetComponent<Spell>().InterfaceImg.name;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug NPC
        if(PlayerNumber == 0)
            return;

        CastSpell();		
	}

    void CastSpell()
    {
        //Spell 1
        if(Input.anyKeyDown)
        {
//            if (Input.GetKeyDown(AttackKey1(PlayerNumber))) {
            if(XCI.GetButton(XboxButton.RightBumper, PlayerNumber) && !GameState.instance.IsPaused){
                if(Spell1 != null & Spell1Casttime < Time.time)
                {
                    GameObject Spellcast = Instantiate(Spell1, transform.position, transform.rotation) as GameObject;
                    Spellcast.GetComponent<Spell>().Caster = this.gameObject;
                    GameGui.instance.SpellHolder[ActuralPlayerNumber * 4 - 4].GetComponentInChildren<CooldownScript>().StartCooldown(Spell1.GetComponent<Spell>().Cooldown);
                    Spell1Casttime = Time.time + Spell1.GetComponent<Spell>().Cooldown;
                }
            }

            //Spell 2
//            if (Input.GetKeyDown(AttackKey2(PlayerNumber))) {
            if(XCI.GetButton(XboxButton.LeftBumper, PlayerNumber) && !GameState.instance.IsPaused){
                if(Spell2 != null & Spell2Casttime < Time.time)
                {
                    GameObject Spellcast = Instantiate(Spell2, transform.position, transform.rotation) as GameObject;
                    Spellcast.GetComponent<Spell>().Caster = this.gameObject;
                    GameGui.instance.SpellHolder[ActuralPlayerNumber * 4 - 3].GetComponentInChildren<CooldownScript>().StartCooldown(Spell2.GetComponent<Spell>().Cooldown);
                    Spell2Casttime = Time.time + Spell2.GetComponent<Spell>().Cooldown;
                }
            }
        }
        //Spell 3
//      if(Input.GetAxis ("joystick " + PlayerNumber.ToString() + " analog 2") < -0.5f)
        if(XCI.GetAxis(XboxAxis.RightTrigger, PlayerNumber) > 0.5f && !GameState.instance.IsPaused)
        {
            if(Spell3 != null & Spell3Casttime < Time.time)
            {
                GameObject Spellcast = Instantiate(Spell3, transform.position, transform.rotation) as GameObject;
                Spellcast.GetComponent<Spell>().Caster = this.gameObject;
                GameGui.instance.SpellHolder[ActuralPlayerNumber * 4 - 2].GetComponentInChildren<CooldownScript>().StartCooldown(Spell3.GetComponent<Spell>().Cooldown);
                Spell3Casttime = Time.time + Spell3.GetComponent<Spell>().Cooldown;
            }
        }

        //Spell 4
//        if(Input.GetAxis ("joystick " + PlayerNumber.ToString() + " analog 2") > 0.5f)
        if(XCI.GetAxis(XboxAxis.LeftTrigger, PlayerNumber) > 0.5f && !GameState.instance.IsPaused)
        {
            if(Spell4 != null & Spell4Casttime < Time.time)
            {
                GameObject Spellcast = Instantiate(Spell4, transform.position, transform.rotation) as GameObject;
                Spellcast.GetComponent<Spell>().Caster = this.gameObject;
                GameGui.instance.SpellHolder[ActuralPlayerNumber * 4 - 1].GetComponentInChildren<CooldownScript>().StartCooldown(Spell4.GetComponent<Spell>().Cooldown);
                Spell4Casttime = Time.time + Spell4.GetComponent<Spell>().Cooldown;
            }
        }
        
        
        
    }
}
