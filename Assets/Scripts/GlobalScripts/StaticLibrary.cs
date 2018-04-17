using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticLibrary : MonoBehaviour {

    public static int spawnArmyCounter = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public void DeselectAll()
    {
        foreach (GameObject city in GameObject.FindGameObjectsWithTag("Town")) // Only one town selected at once
        {
            city.GetComponent<CityStats>().isSelected = false;
        }
        foreach (GameObject city in GameObject.FindGameObjectsWithTag("Army")) // Deselect armies
        {
            city.GetComponent<ArmyStats>().isSelected = false;
        }
    }

    static public void DeselectTown()
    {
        foreach (GameObject city in GameObject.FindGameObjectsWithTag("Town")) // Only one town selected at once
        {
            city.GetComponent<CityStats>().isSelected = false;
        }
    }

    static public void DeselectArmy()
    {
        foreach (GameObject city in GameObject.FindGameObjectsWithTag("Army")) // Deselect armies
        {
            city.GetComponent<ArmyStats>().isSelected = false;
        }
    }
}
