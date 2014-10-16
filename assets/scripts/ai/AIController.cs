using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;

public class AIController : MonoBehaviour {

    public float RandomWalkDistance;
    public float FightWalkDistance;
    public float LockonTime;

    private Vector3 WalkingPosition;
    private List<GameObject> Players = new List<GameObject>();
    private float NextLockon;
    private GameObject TargetPlayer;
    

	// Use this for initialization
	void Start () {
        MoveToRandomPosition(false);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(obj != this.gameObject)
                Players.Add(obj);
        }
        LockonNewTarget();
        transform.LookAt(TargetPlayer.transform.position);
	}
	
   
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(WalkingPosition, transform.position) < 1.5f)
           MoveToRandomPosition(false);

        if (NextLockon > Time.time)
            LockonNewTarget();
	}

    public void LockonNewTarget()
    {
        if (Players.Count == 0)
        {
            NextLockon = Mathf.Infinity;
            return;
        }
        float Distance = 0f;
        int Chosen = 0;
        for (int i = 0; i < Players.Count; i++)
        {
            if(Vector3.Distance(transform.position, Players[i].transform.position) < Distance)
            {
                Distance = Vector3.Distance(transform.position, Players[i].transform.position);
                Chosen = i;
            }
        }
        NextLockon = Time.time + Random.Range(1, LockonTime);
        TargetPlayer = Players [Chosen];
    }

    public void MoveToRandomPosition(bool Fight)
    {
        if (Fight)
        {
            if (Vector3.Distance(WalkingPosition, transform.position) < 1f)
                Walkto(Random.insideUnitSphere * FightWalkDistance);
            else
                Walkto(WalkingPosition);
        } else
        {
            Walkto(Random.insideUnitSphere * RandomWalkDistance);
        }
    }

    public void Walkto(Vector3 Pos)
    {
        Pos += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(Pos, out hit, RandomWalkDistance, 1);
        Vector3 finalPosition = hit.position;

        RaycastHit RayHit;
        Debug.DrawLine(transform.position, finalPosition, Color.red, 5f);
        if(Physics.Linecast(transform.position, finalPosition, out RayHit))
        {
            Debug.DrawLine(transform.position, RayHit.point, Color.green, 5f);
            if(RayHit.collider.tag == "Lava")
            {
                MoveToRandomPosition(false);
                return;
            }
        }

        //Sets Final position to the distination
        WalkingPosition = finalPosition;

        this.GetComponent<NavMeshAgent>().destination = finalPosition;
    }
}
