using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class PlayerMovement : MonoBehaviour {

	public float Speed;
	public Vector3 LookatPoint;
	public int PlayerNumber = 1;
    public int DisplayPlayerNumber;
    Camera RealCam;

	private ParameterScript.Parameters LevelParameters;


	// Use this for initialization
	void Start () {
		LookatPoint = Vector3.zero;
        RealCam = Camera.main;
		LevelParameters = GameObject.FindObjectOfType<ParameterScript>().Parser;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug for bots
        if(PlayerNumber == 0)
            return;
        if(!GameState.instance.IsPaused)
        {
            if(GameState.instance.SinglePlayer)
            {
                if(RealCam.GetComponent<SinglePlayerCameraScript>().Cinematic)
                    transform.Translate (XCI.GetAxis(XboxAxis.LeftStickX, PlayerNumber) * Speed * Time.deltaTime, 0, XCI.GetAxis(XboxAxis.LeftStickY, PlayerNumber) * Speed * Time.deltaTime, Space.World);
                else
                {
                if(Vector3.Distance(transform.position + new Vector3(XCI.GetAxis(XboxAxis.LeftStickX, PlayerNumber) * Speed * Time.deltaTime, 0, XCI.GetAxis(XboxAxis.LeftStickY, PlayerNumber) * Speed * Time.deltaTime), RealCam.transform.position) < 30f)
                    transform.Translate (XCI.GetAxis(XboxAxis.LeftStickX, PlayerNumber) * Speed * Time.deltaTime, 0, XCI.GetAxis(XboxAxis.LeftStickY, PlayerNumber) * Speed * Time.deltaTime, Space.World);
                else if(Vector3.Distance(transform.position + new Vector3(XCI.GetAxis(XboxAxis.LeftStickX, PlayerNumber) * Speed * Time.deltaTime, 0, XCI.GetAxis(XboxAxis.LeftStickY, PlayerNumber) * Speed * Time.deltaTime), RealCam.transform.position) < Vector3.Distance(transform.position, RealCam.transform.position))
                    transform.Translate (XCI.GetAxis(XboxAxis.LeftStickX, PlayerNumber) * Speed * Time.deltaTime, 0, XCI.GetAxis(XboxAxis.LeftStickY, PlayerNumber) * Speed * Time.deltaTime, Space.World);
                }

                LookatPoint = new Vector3 (XCI.GetAxis(XboxAxis.RightStickX, PlayerNumber) + transform.position.x, transform.position.y, XCI.GetAxis(XboxAxis.RightStickY, PlayerNumber) + transform.position.z);
        		transform.LookAt (LookatPoint);
            }
            else
            {
                transform.Translate (XCI.GetAxis(XboxAxis.LeftStickX, PlayerNumber) * Speed * Time.deltaTime, 0, XCI.GetAxis(XboxAxis.LeftStickY, PlayerNumber) * Speed * Time.deltaTime, Space.World);
                LookatPoint = new Vector3 (XCI.GetAxis(XboxAxis.RightStickX, PlayerNumber) + transform.position.x, transform.position.y, XCI.GetAxis(XboxAxis.RightStickY, PlayerNumber) + transform.position.z);
                transform.LookAt (LookatPoint);
            }
        }

        if (XCI.GetButtonDown(XboxButton.A))
        {
            Camera RealCam = Camera.main;
            Debug.Log(Vector3.Distance(RealCam.transform.position, transform.position));
        }

		//Checks if you are outside of the screen and teleports you to the other side if you is
//		CheckIfOverBorders ();
	}

	public void ApplyKnockBack(Vector3 pos, float force)
	{
        pos.y = 0;
		rigidbody.AddForce (pos * force);
	}

	void CheckIfOverBorders()
	{
        if (transform.position.x > LevelParameters.XPos)
            transform.position = new Vector3(LevelParameters.XNeg + .1f, transform.position.y, transform.position.z);
        else if (transform.position.x < LevelParameters.XNeg)
            transform.position = new Vector3(LevelParameters.XPos - .1f, transform.position.y, transform.position.z);
        else if (transform.position.z > LevelParameters.ZPos)
            transform.position = new Vector3(transform.position.x, transform.position.y, LevelParameters.ZNeg + .1f);
        else if (transform.position.z < LevelParameters.ZNeg)
            transform.position = new Vector3(transform.position.x, transform.position.y, LevelParameters.ZPos - .1f);
   
	}
 
}
