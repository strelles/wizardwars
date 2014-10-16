using UnityEngine;
using System.Collections;

public class PointGainer : MonoBehaviour {

    public int PlayerSpot = 1;
    public GameObject[] playerPoints;

    void OnEnable()
    {
        GameState.instance.GamePause(true);

        if(GameState.instance.playerInfo.Count < PlayerSpot)
        {
            this.gameObject.SetActive(false);
            return;
        }

        int PlayerPoints = GameState.instance.GameInfo.PlayerPoints[PlayerSpot - 1];

        for (int i = 0; i < playerPoints.Length; i++)
        {
            if(PlayerPoints > i)
                playerPoints[i].SetActive(true);
            else
                playerPoints[i].SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            GameState.instance.GamePause(false);
            GameGui.instance.UnloadForground();
        }
}
}
