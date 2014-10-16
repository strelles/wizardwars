using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;

public class ControllerSelection : MonoBehaviour {

    public GameObject[] PlayerHolders;
    public GameObject[] CharPotraits;
    public GameObject[] PlayableCharactors;
    public int[] PlayerPicks;
    List<int> PlayerControllers = new List<int>();

    private bool[] isAxisInUse = new bool[4]{false, false, false, false};
    private float[] TimeTilAxisUse = new float[4]; 

    int AmountOfPlayers = 0;

	// Use this for initialization
	void OnEnable () {
        //Setups
        PlayerControllers = new List<int>();
        PlayerPicks = new int[4];
        AmountOfPlayers = 0;

        SetyCharPotraits();

        

//	    foreach (GameObject go in PlayerHolders)
//            go.SetActive(false);
	}

    void SetyCharPotraits()
    {
        for (int i = 0; i < CharPotraits.Length; i++)
        {
            if(i >= PlayableCharactors.Length)
                CharPotraits[i].SetActive(false);
            else
            {
                CharPotraits[i].GetComponentInChildren<PlayerPotrait>().ChangeSprite(PlayableCharactors[i].GetComponent<PlayerInfo>().CharakterImg, PlayableCharactors[i].GetComponent<PlayerInfo>().name);
                CharPotraits[i].GetComponentInChildren<ChangeHPInfo>().ChangeHp(PlayableCharactors[i].GetComponent<PlayerInfo>().MaxHP);
                CharPotraits[i].GetComponentInChildren<ChangeSpeedInfo>().ChangeSpeed(PlayableCharactors[i].GetComponent<PlayerMovement>().Speed);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        Inputs();
        CheckAxisRestes();
  	}

    public void Inputs()
    {
        if (XCI.GetButtonDown(XboxButton.Start, 1))
            AddPlayer(1);
        if (XCI.GetButtonDown(XboxButton.Start, 2))
            AddPlayer(2);
        if (XCI.GetButtonDown(XboxButton.Start, 3))
            AddPlayer(3);
        if (XCI.GetButtonDown(XboxButton.Start, 4))
            AddPlayer(4);
        
        //Debug to start level
        if(XCI.GetButtonDown(XboxButton.Back, 1))
            GameGui.instance.LoadTestLevel();
        if(XCI.GetButtonDown(XboxButton.Back, 2))
            GameGui.instance.LoadTestLevel();
        if(XCI.GetButtonDown(XboxButton.Back, 3))
            GameGui.instance.LoadTestLevel();
        if(XCI.GetButtonDown(XboxButton.Back, 4))
            GameGui.instance.LoadTestLevel();

        if (XCI.GetAxis(XboxAxis.LeftStickY, 1) > 0.5 && !isAxisInUse [0])
            ChangeSelectionDown(1);
        if (XCI.GetAxis(XboxAxis.LeftStickY, 2) > 0.5 && !isAxisInUse [1])
            ChangeSelectionDown(2);
        if (XCI.GetAxis(XboxAxis.LeftStickY, 3) > 0.5 && !isAxisInUse [2])
            ChangeSelectionDown(3);
        if (XCI.GetAxis(XboxAxis.LeftStickY, 4) > 0.5 && !isAxisInUse [2])
            ChangeSelectionDown(4);

        if (XCI.GetAxis(XboxAxis.LeftStickY, 1) < -0.5 && !isAxisInUse[0])
            ChangeSelectionUp(1);
        if (XCI.GetAxis(XboxAxis.LeftStickY, 2) < -0.5 && !isAxisInUse[1])
            ChangeSelectionUp(2);
        if (XCI.GetAxis(XboxAxis.LeftStickY, 3) < -0.5 && !isAxisInUse[2])
            ChangeSelectionUp(3);
        if (XCI.GetAxis(XboxAxis.LeftStickY, 4) < -0.5 && !isAxisInUse[3])
            ChangeSelectionUp(4);

        //SelectChar
        if(XCI.GetButtonDown(XboxButton.A, 1))
            SelectChar(1);
        if(XCI.GetButtonDown(XboxButton.A, 2))
            SelectChar(2);
        if(XCI.GetButtonDown(XboxButton.A, 3))
            SelectChar(3);
        if(XCI.GetButtonDown(XboxButton.A, 4))
            SelectChar(4);
        
        //DeselectChar
        if(XCI.GetButtonDown(XboxButton.B, 1))
            DeselectChar(1);
        if(XCI.GetButtonDown(XboxButton.B, 2))
            DeselectChar(2);
        if(XCI.GetButtonDown(XboxButton.B, 3))
            DeselectChar(3);
        if(XCI.GetButtonDown(XboxButton.B, 4))
            DeselectChar(4);
    }

    void SelectChar(int ControllerNumber)
    {
        int PlayerNumber = FindPlayer(ControllerNumber);
        if(PlayerNumber > PlayerPicks.Length)
            return;

        CharPotraits[PlayerPicks[PlayerNumber]].GetComponent<CharacterSelecter>().LockInChar(PlayerNumber);
//        PlayerHolders[PlayerNumber].GetComponent<PlatformScript>().SpawnSelected(PlayableCharactors[PlayerPicks[PlayerNumber]].GetComponent<PlayerInfo>().SelectionObject);
        GameState.instance.CharacktorChoises[PlayerNumber] = PlayableCharactors[PlayerPicks[PlayerNumber]];
    }

    void DeselectChar(int ControllerNumber)
    {
        int PlayerNumber = FindPlayer(ControllerNumber);
        if (PlayerNumber > PlayerPicks.Length)
            return;

        CharPotraits[PlayerPicks[PlayerNumber]].GetComponent<CharacterSelecter>().LockOutChar(PlayerNumber);
//        PlayerHolders [PlayerNumber].GetComponent<PlatformScript>().SpawnSelected(null);
        GameState.instance.CharacktorChoises [PlayerNumber] = null;
    }

    void AddPlayer(int ControllerNumber)
    {
        foreach (int player in PlayerControllers)
        {
            if(player == ControllerNumber)
                return;
        }

        Debug.Log("Controller " + ControllerNumber.ToString() + " Is in the house");

        PlayerControllers.Add(ControllerNumber);
//        PlayerHolders [AmountOfPlayers].SetActive(true);
        PlayerPicks[AmountOfPlayers] = 0;
    
        CharPotraits[0].GetComponent<CharacterSelecter>().SetSelectorActive(AmountOfPlayers);
        AmountOfPlayers++;

        GameState.instance.playerInfo = PlayerControllers;
    }

    int FindPlayer(int ControllerNumber)
    {
        isAxisInUse[ControllerNumber - 1] = true;
        TimeTilAxisUse [ControllerNumber - 1] = Time.time + .25f;
        int i = 0;
        bool found = false;
        foreach(int controller in PlayerControllers)
        {
            if(controller == ControllerNumber)
            {
                found = true;
                break;
            }
            i++;
        }

        if(!found)
            i = -1;

        return i;
    }

    void ChangeSelectionUp(int ControllerNumber)
    {
        int PlayerNumber = FindPlayer(ControllerNumber);

        if(PlayerNumber == -1 || GameState.instance.CharacktorChoises[PlayerNumber] != null)
            return;


        CharPotraits[PlayerPicks[PlayerNumber]].GetComponent<CharacterSelecter>().SetSelectorUnActive(PlayerNumber);
        PlayerPicks[PlayerNumber]++;
        if(PlayerPicks[PlayerNumber] > CharPotraits.Length - 1)
            PlayerPicks[PlayerNumber] = 0;
        CharPotraits[PlayerPicks[PlayerNumber]].GetComponent<CharacterSelecter>().SetSelectorActive(PlayerNumber);
    }

    void ChangeSelectionDown(int ControllerNumber)
    {
        int PlayerNumber = FindPlayer(ControllerNumber);
        
        if(PlayerNumber == -1 || GameState.instance.CharacktorChoises[PlayerNumber] != null)
            return;
        
        
        CharPotraits[PlayerPicks[PlayerNumber]].GetComponent<CharacterSelecter>().SetSelectorUnActive(PlayerNumber);
        PlayerPicks[PlayerNumber]--;
        if(PlayerPicks[PlayerNumber] < 0)
            PlayerPicks[PlayerNumber] = CharPotraits.Length - 1;
        CharPotraits[PlayerPicks[PlayerNumber]].GetComponent<CharacterSelecter>().SetSelectorActive(PlayerNumber);
    }

    void CheckAxisRestes()
    {
        if (TimeTilAxisUse[0] < Time.time && isAxisInUse[0])
            isAxisInUse[0] = false;
        if (TimeTilAxisUse[1] < Time.time && isAxisInUse[1])
            isAxisInUse[1] = false;
        if (TimeTilAxisUse[2] < Time.time && isAxisInUse[2])
            isAxisInUse[2] = false;
        if (TimeTilAxisUse[3] < Time.time && isAxisInUse[3])
            isAxisInUse[3] = false;
    }
}
