using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {


    public float Scale;

    public void SetScale(float MaxHP, float CurrentHP)
    {
        Scale = float.Parse(CurrentHP.ToString()) / float.Parse(MaxHP.ToString());
        transform.localScale = new Vector3(Scale, 1, 1);
    }
}
