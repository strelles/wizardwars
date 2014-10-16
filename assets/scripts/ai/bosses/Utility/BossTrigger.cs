using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {

    public Camera BossCam;  //The Cam for the BossFight
    public GameObject[] PlayerSpawns;  //Place Players are teleportet to
    public GameObject TheBoss; //The Boss the player has to fight, intro animation have to be on the boss
    private bool FightStartet = false; //Starting the Fight
    public Camera ExploreCamera; //The Main Camera of the game


    void Update()
    {
        //Engages in the fight
        if (FightStartet)
        {
            //TODO: Teleport Players
            float speed = 5f;
            float step = speed * Time.deltaTime;

            //Move Cam
            ExploreCamera.transform.position = Vector3.MoveTowards(ExploreCamera.transform.position, BossCam.transform.position, step);
            if(Vector3.Distance(ExploreCamera.transform.position, BossCam.transform.position) < 1.5f)
            {
                ChangeCam();
                FightStartet = false;
                TheBoss.SendMessage("InitAnimation");
            }
        }
    }

    void OnTriggerEnter(Collider other) { 
        if(other.tag == "Player")
        {
            ExploreCamera = Camera.main;
            FightStartet = true;
            ExploreCamera.GetComponent<SinglePlayerCameraScript>().Cinematic = true;
        }
    }

    void ChangeCam()
    {
        if (BossCam.enabled)
        {
            ExploreCamera.enabled = true;
//            ExploreCamera.GetComponent<SinglePlayerCameraScript>().Cinematic = false;
            BossCam.enabled = false;
        } else
        {
            BossCam.enabled = true;
            ExploreCamera.enabled = false;
        }
    }
}
