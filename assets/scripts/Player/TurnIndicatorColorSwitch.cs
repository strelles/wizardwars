using UnityEngine;
using System.Collections;

public class TurnIndicatorColorSwitch : MonoBehaviour {

    public Color[] PlayerColors = new Color[4] {new Color(1,0,0), new Color (0,1,0), new Color(0,0,1), new Color(1,1,1)};

    public void ChangeColor(int PlayerNumber)
    {
        if (PlayerNumber <= PlayerColors.Length)
            ChangeColor(PlayerColors [PlayerNumber - 1]);
    }
    public void ChangeColor(Color color)
    {
        renderer.material.SetColor("_IndicatorColor", color);
//        this.GetComponent<Material>().SetColor("_IndicatorColor", color);
    }
}
