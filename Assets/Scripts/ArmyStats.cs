using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArmyStats : MonoBehaviour {

    public int armySize;
    public float armySpeed;
    public float armyForce;
    public bool isSelected;
    public Text armySizeText;
    private Color color;
    [HideInInspector]
    public int spawnNumber;
    public int playerNumber;


	// Use this for initialization
	void Start () {
        color = GetComponent<Renderer>().material.color;
    }

	// Update is called once per frame
	void Update () {
        armySizeText.text = armySize.ToString();
        Update_color();
	}

    void Update_color()
    {
        if (isSelected)
            transform.GetComponent<Renderer>().material.color = new Color32(75, 10, 1, 6);
        else
            transform.GetComponent<Renderer>().material.color = color;
    }
}
