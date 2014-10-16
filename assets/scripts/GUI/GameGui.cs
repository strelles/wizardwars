using UnityEngine;
using System.Collections;

public class GameGui : MonoBehaviour {

    public static GameGui instance;

    public GameObject GUIHolder;

	public enum GUIBackground{Menus, Ingame, none}
	private GUIBackground LastBackground = GUIBackground.none;
	public GUIBackground Background = GUIBackground.none;
    public float BookTime = 0.5f;
    public GameObject Book;

    //SpellHolders
    public GameObject[] SpellHolder;

	public GameObject[] Backgrounds;
	/*BACKGROUNDS
	 * 0 - Menus
     * 1 - Ingame
	 */

	public enum GUIForground{MainMenu, Versus, PauseMenu , WinScreen, none}
	private GUIForground LastForground = GUIForground.none;
	public GUIForground Forground = GUIForground.none;

	public GameObject[] Forgrounds;
	/*FORGROUNDS:
	 *0 - MainMenu  
     *1 - Verses
     *2 - PauseMenu
     *3 - WinScreen
	 */

	// Use this for initialization
	void Start () {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(GUIHolder);

		Background = GUIBackground.Menus;
        Forground = GUIForground.MainMenu;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateForground ();
        UpdateBackground();
	
	}

	void UpdateBackground()
	{
		if (LastBackground != Background)
        {
            switch (Background)
            {
                case GUIBackground.Menus:
                    LoadBackground(0);
                    break;
                case GUIBackground.Ingame: 
                    LoadBackground(1);
                    break;
                case GUIBackground.none:
                    UnloadBackgrounds();
                    LastBackground = GUIBackground.none;
                    break;
                default:
                    Debug.Log ("Something is wrong with your Background load -- Wrong number loaded");
                    break;
            }
        }
	}

	void UpdateForground()
	{
		if (LastForground != Forground) 
		{
			switch (Forground) 
            {
    			case GUIForground.MainMenu:
    				LoadForground (0);
    				break;
                case GUIForground.Versus:
                    LoadForground(1);
                    break;
                case GUIForground.PauseMenu:
                    LoadForground(2);
                    break;
                case GUIForground.none:
    				UnloadForgrounds ();
                    LastForground = GUIForground.none;
    				break;
                case GUIForground.WinScreen:
                    LoadForground(3);
                    break;
    			default:
    				Debug.Log ("Something is wrong with your forground load -- Wrong number loaded");
                    break;
			}
		}
	}

    void LoadBackground(int Number)
    {
        LastBackground = Background;
        UnloadBackgrounds();
//        foreach(CharacterSelecter car in GameObject.FindObjectsOfType<CharacterSelecter>())
//        {
//            car.LockInChar(1);
//            car.LockInChar(2);
//            car.LockInChar(3);
//            car.LockInChar(4);
//        }
        Backgrounds [Number].SetActive(true);
        if (Number != 1 && Application.loadedLevelName != "Empty")
        {
            GameState.instance.ResetInfos();
            Application.LoadLevel("Empty");
        }
    }

    void UnloadBackgrounds()
    {
        foreach (GameObject go in Backgrounds)
            go.SetActive (false);
        GameState.instance.IsPaused = false;
        Time.timeScale = 1f;
    }

	void LoadForground(int Number)
	{
        if (Background == GUIBackground.Menus)
        {
            ChangePage(false);
            LastForground = Forground;
            UnloadForgrounds();
            Forgrounds [Number].SetActive(true);
        } else
        {
            LastForground = Forground;
            UnloadForgrounds();
            Forgrounds [Number].SetActive(true);
        }
	}

	void UnloadForgrounds()
	{
		foreach (GameObject go in Forgrounds)
			go.SetActive (false);
	}

    #region BuiltinLoads

    #region Forgrounds
    public void UnloadForground(){Forground = GUIForground.none;}
    public void ForgroundLoadVerses(){Forground = GUIForground.Versus;}
    public void ForgroundLoadMainMenu(){Forground = GUIForground.MainMenu;}
    public void ForgroundLoadPauseMenu(){Forground = GUIForground.PauseMenu;}
    #endregion

    #region Backgrounds
    public void UnloadBackground(){Background = GUIBackground.none;}
    public void BackgroundLoadMenus(){Background = GUIBackground.Menus;}
    public void BackgroundLoadInGame(){Background = GUIBackground.Ingame;}
    #endregion

    #region LoadSpecifikLevels
    public void LoadTestLevel(){
        if (GameState.instance.playerInfo.Count != 0)
        {
            GameState.instance.InitLevel(60f, true);
//            Application.LoadLevel("BlackRockCave");
            Application.LoadLevel("TestSceen");
            UnloadForground();
            Background = GUIBackground.Ingame;
        }
    }

    public void RestartLevel(){ Application.LoadLevel(Application.loadedLevel); Forground = GUIForground.none;}
    #endregion

    #endregion

    public void ChangePage(bool Backwards)
    {
        Book.GetComponent<UISpriteAnimation>().Play(Backwards);
    }
}
