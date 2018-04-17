using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceController : MonoBehaviour {

    // Use this for initialization
    public Text balanceText;
    public static int balance;
    private int y = 5;
    public static test;
    public int unit1; 
    GameObject[] allTowns;

    // Use this for initialization
    void Start()
    {
        balance = 10000;
        balanceText.text = "Balance: " + balance;
        allTowns = GameObject.FindGameObjectsWithTag("Town");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > x)
        {
            x = x + 5;
            foreach(GameObject town in allTowns)
            {
                balance += town.GetComponent<CityStats>().income;
            }
            balanceText.text = "Balance: " + balance;
        }
        balanceText.text = "Balance: " + balance;
    }
}
