using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {

    public GameObject SpawnetChar;

    public void SpawnSelected(GameObject Char)
    {
        if(SpawnetChar == null)
        {
            if(Char != null)
                SpawnetChar = Instantiate(Char,transform.position, transform.rotation) as GameObject;
        }
        else
        {
            DespawnSelected();
            if(Char != null)
                SpawnetChar = Instantiate(Char,transform.position, transform.rotation) as GameObject;
        }
    }

    public void DespawnSelected()
    {
        if(SpawnetChar != null)
            Destroy(SpawnetChar);
    }
}
