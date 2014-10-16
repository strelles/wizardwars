using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;

public class GameState : MonoBehaviour {

    public static GameState instance;

    public List<int> playerInfo;
    public List<PlayerInfo> AlivePlayers;
    public GameObject[] CharacktorChoises;

    //IngameInfo
    public CurrentGameInfo GameInfo;
    public bool Ingame;
    public bool IsPaused;
    public float Playtime = 0f;
    private float CurrentSessionPlaytime;
    public bool FirstTimePhase;
    public bool Overtime;
    public int Difficulty = 0;

    public bool SinglePlayer = false;
    private int SS = 0;

    void Awake()
    {
    }

	void Start () 
	{
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        CharacktorChoises = new GameObject[4];
	}

	// Update is called once per frame
	void Update () {
        Ingametimer();
        GameLoop();
        if (XCI.GetButtonDown(XboxButton.Y))
            TakeScreenshot();

        Inputs();
	}

    //DebugScreenshot
    void TakeScreenshot()
    {
        Application.CaptureScreenshot("Screenshot/Screenshot_" + SS + ".png", 2);
        SS++;
    }

    void Ingametimer()
    {
        if (Ingame)
        {
            Playtime -= Time.deltaTime;
            if(CurrentSessionPlaytime / 2 > Playtime)
                FirstTimePhase = true;
            if(Playtime < 0)
                Overtime = true;
        }
    }

    void GameLoop()
    {
        if(Ingame)
        {
            //Anounce winner and restart match
            if(AlivePlayers.Count <= 1)
            {

            }
        }
    }

    public void GamePause(bool paused)
    {
        if(paused)
        {
            IsPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            IsPaused = false;
            Time.timeScale = 1f;
        }
    }

    void Inputs()
    {
        //RestartLevelCheet
        if (Input.GetKeyDown(KeyCode.JoystickButton6))
            Application.LoadLevel(Application.loadedLevel);
        if (Input.GetKeyDown(KeyCode.JoystickButton7) && GameGui.instance.Background == GameGui.GUIBackground.Ingame)
        {
            if(GameGui.instance.Forground == GameGui.GUIForground.none)
            {
                GamePause(true);
                GameGui.instance.Forground = GameGui.GUIForground.PauseMenu;
            }
            else if(GameGui.instance.Forground == GameGui.GUIForground.PauseMenu)
            {
                GamePause(false);
                GameGui.instance.Forground = GameGui.GUIForground.none;
            }   
                
        }
    }


    public void InitLevel(float PlayTime, bool NewGame)
    {
        Ingame = true;
        Playtime = PlayTime;
        CurrentSessionPlaytime = PlayTime;
        AlivePlayers = new List<PlayerInfo>();
        if(NewGame)
            GameInfo = new CurrentGameInfo(5, 0, playerInfo.Count, PlayTime);

        //Sets Time bools to false
        FirstTimePhase = false;
        Overtime = false;
    }

    //Playtime Player Info.
    public void AddAlivePLayer(PlayerInfo player)
    {
        AlivePlayers.Add(player);
    }

    public void RemoveAlivePlayer(PlayerInfo player)
    {
        AlivePlayers.Remove(player);
        Debug.Log(AlivePlayers.Count);
        if(AlivePlayers.Count <= 1)
        {
            foreach(PlayerInfo LastPlayer in AlivePlayers)
            {
                GameInfo.AddPointsToPlayer(LastPlayer.PlayerNumber);
                GameGui.instance.Forground = GameGui.GUIForground.WinScreen;
            }
            RespawnPlayers();   
        }
    }

    public void ResetInfos()
    {
        playerInfo = new List<int>();
    }

    public void RespawnPlayers()
    {
        foreach(PlayerInfo player in AlivePlayers)
            Destroy(player.gameObject);
        InitLevel(GameInfo.Playtime, false);

        foreach(PlayerSpawnerIngame spawn in GameObject.FindObjectsOfType<PlayerSpawnerIngame>())
            spawn.Respawn();


    }

}

public class CurrentGameInfo
{
    public int AmountOfRounds;
    public int BestTo;
    public int CurrentRounds;
    public float Playtime;

    public int[] PlayerPoints;


    public CurrentGameInfo(int Rounds, int bestTo, int AmountOfPlayers, float playTime)
    {
        AmountOfRounds = Rounds;
        BestTo = bestTo;
        PlayerPoints = new int[AmountOfPlayers];
        Playtime = playTime;
        for (int i = 0; i < PlayerPoints.Length; i++)
        {
            PlayerPoints[i] = 0;
        }
    }

    public void AddPointsToPlayer(int PlayerNumber)
    {
        AddPointsToPlayer(PlayerNumber, 1);
    }
    public void AddPointsToPlayer(int PlayerNumber, int AmountOfPoints)
    {
//        if(PlayerNumber > PlayerPoints.Length)
//            return;

        PlayerPoints[PlayerNumber - 1] += AmountOfPoints;
        Debug.Log("points: " +PlayerPoints[PlayerNumber - 1] + "For Player: " + PlayerNumber);

        if(BestTo != 0)
        {
            if(PlayerPoints[PlayerNumber] >= BestTo)
            {
                //Win Game TODO !
            }
        }
    }

}
