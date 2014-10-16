using UnityEngine;
using System.Collections;

public class BossInfo : MonoBehaviour {

    public float[] MaxHP;
    public float HP;
    public HealthBarScript HpBar;
    public string Name;
    
    public float LavaDmg = 1;
    private bool InLava = false;
    
    void Start()
    {
        HP = MaxHP[GameState.instance.Difficulty];
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
            HpBar.SetScale(MaxHP[GameState.instance.Difficulty], HP);
            if (HP <= 0)
                Die ();
        }
    }
    
    void Die()
    {
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
