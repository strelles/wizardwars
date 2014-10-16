using UnityEngine;
using System.Collections;

public class CooldownScript : MonoBehaviour {

    private bool Active;
    private float DecreaseTimer;
    private float FillRate;

    public void OnEnable()
    {
        this.GetComponent<UISprite>().fillAmount = 0f;
    }

    /// <summary>
    /// Starts the CooldownCykel
    /// </summary>
    /// <param name="CDTime">CD time.</param>
    public void StartCooldown(float CDTime)
    {
        Active = true;
        DecreaseTimer = CDTime;
        FillRate = 1f;
    }

    public void Update()
    {
        if(Active)
        {
            FillRate -= Time.deltaTime / DecreaseTimer;
            this.GetComponent<UISprite>().fillAmount = FillRate;
            if(FillRate <= 0f)
            {
                this.GetComponent<UISprite>().fillAmount = 0f;
                Active = false;
            }
        }
    }
}
