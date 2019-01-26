using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : MonoBehaviour
{
    public static List<Elemental> ActiveElementals = new List<Elemental>();

    public bool FollowPlayer;
    public Transform player;

	void Awake ()
    {
        ActiveElementals.Add(this);
	}

    private void OnDestroy()
    {
        ActiveElementals.Remove(this);
    }

    public void DoInteract()
    {
        Debug.Log("Aah!");
    }
}
