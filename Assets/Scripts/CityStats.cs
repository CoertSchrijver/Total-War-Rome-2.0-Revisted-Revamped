using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityStats : MonoBehaviour {
    public double popMax;
    public int income;
    public int population;
    public int armySize;
    public float building_modifier;
    public float population_growth;
    [HideInInspector]
    public bool isSelected = true;
    public string cityName;
    public int playerNumber;

    private Color32 color;
    private double timevalue = 0.1;
    public int initIncome;
    public int initPopulation;

    public Text cityStatsText;

	void Start() {

        Config.Fill_list();
        color = GetComponent<Renderer>().material.color;
        transform.Find("CityCanvas").Find("CityStatsPanel").Find("CityNameText").gameObject.GetComponent<Text>().text = cityName;
        transform.Find("CityNameCanvas").Find("CityName").gameObject.GetComponent<Text>().text = cityName;
    }

    void Update()
    {
        if (Time.time > timevalue)
        {
            timevalue+=0.1;
            int rate_of_growth = 4;
            // Sigmoid function for population
            population = (int)(initPopulation + (popMax - initPopulation) / (1 + Mathf.Exp((-population_growth * rate_of_growth / 60) * Time.time)) - (popMax - initPopulation) / 2) - armySize * 20;
            income = initIncome + (int)((population - initPopulation) * 0.05) + (int)(initIncome * 2 * building_modifier);
            cityStatsText.text = "Income: " + income.ToString() + "\nPopulation: " + population.ToString() + "\nBuilding Value: " + (building_modifier * 100).ToString() + "%\nArmies: " + armySize.ToString();
            // Income function based on population and building mod;
        }
        Update_color();
    }

    void OnMouseEnter()
    {       
        GetComponent<Renderer>().material.color = new Color32(75, 10, 1, 6);
    }
    void OnMouseExit()
    {
        if (!isSelected)
            GetComponent<Renderer>().material.color = color;
    }

    void Update_color()
    {
        if (isSelected)
            transform.GetComponent<Renderer>().material.color = new Color32(75, 10, 1, 6);
        else
            transform.GetComponent<Renderer>().material.color = color;
    }
}
