using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LavaRaiserScript : MonoBehaviour {

    public GameObject LavaMesh;
    public float Speed;
    private SphereCollider ThisCollider;
	
    void Start()
    {
        ThisCollider = GetComponent<SphereCollider>();
    }

	// Update is called once per frame
	void Update () {
	    if (GameState.instance.Overtime)
        {
            if(ThisCollider.radius > 0)
                ThisCollider.radius -= Speed * Time.deltaTime;

            Mesh mesh = LavaMesh.GetComponent<MeshFilter>().mesh;
            Vector3[] verts = mesh.vertices;
            for (int i = 0; i < verts.Length; i++) {
                if(Vector3.Distance(transform.position, verts[i]) > ThisCollider.radius && verts[i].y != this.transform.position.y)
                    verts[i] = new Vector3(verts[i].x, transform.position.y, verts[i].z);
            }
            //Recalculating verts of the lava
            mesh.vertices = verts;
            mesh.RecalculateBounds();

        }
	}
}
