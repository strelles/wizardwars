using UnityEngine;
using System.Collections;

public class ParameterScript : MonoBehaviour {

	
	public struct Parameters
	{
		public float XPos;
		public float XNeg;
		public float ZPos;
		public float ZNeg;
	}
	public Parameters Parser;

	// Use this for initialization
	void Awake () {
		foreach (Transform child in GetComponentsInChildren<Transform>()) {
			switch(child.name)
			{
			case "XPos":
				Parser.XPos = child.position.x;
				break;
			case "XNeg":
				Parser.XNeg = child.position.x;
				break;
			case "ZPos":
				Parser.ZPos = child.position.z;
				break;
			case "ZNeg":
				Parser.ZNeg = child.position.z;
				break;
			default:
				Debug.Log ("Something went wrong with the Parameters of the level with the child names " + child.name + "  -- May be level convention or missing parameters");
				break;
			}

		}
	}

    public Parameters infenet()
    {
        Parameters Pars;
        Pars.XPos = Mathf.Infinity;
        Pars.XNeg = Mathf.Infinity;
        Pars.ZPos = Mathf.Infinity;
        Pars.ZNeg = Mathf.Infinity;

        return Pars;
    }
}


