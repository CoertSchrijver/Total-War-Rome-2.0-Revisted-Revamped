using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateCities : MonoBehaviour {

    public GameObject newCity;
    private GameObject city;

	void Start () {

        // Make a new city with location, name, max population, population growth, building mod, armies 
        InstantiateCity(new Vector3(-10, 0.5f, 0), "Constantiople", 5000, 0.02f, 0, 0);
        InstantiateCity(new Vector3(10, 0.5f, 4), "NorthGarth", 5000, 0.02f, 0, 0);
    }

    void InstantiateCity(Vector3 location, string name, int popMax, float popGrowth, float buildMod, int army) // Instantias a new city
    {
        city = Instantiate(newCity, location, Quaternion.identity);
        city.transform.parent = GameObject.Find("_Dynamic").transform;
        city.name = name;

        city.GetComponent<CityStats>().initPopulation = Random.Range(1000, 5000);
        city.GetComponent<CityStats>().initIncome = Random.Range(250, city.GetComponent<CityStats>().initPopulation / 4);
        city.GetComponent<CityStats>().income = city.GetComponent<CityStats>().initIncome;
        city.GetComponent<CityStats>().population = city.GetComponent<CityStats>().initPopulation;
        city.GetComponent<CityStats>().popMax = popMax;
        city.GetComponent<CityStats>().population_growth = popGrowth;
        city.GetComponent<CityStats>().building_modifier = buildMod;
        city.GetComponent<CityStats>().armySize = army;
        city.GetComponent<CityStats>().cityName = name;

        city.GetComponent<CityButtonScript>().buildingPanel = city.transform.Find("CityCanvas").Find("CityBuildingPanel").gameObject;
        city.GetComponent<CityButtonScript>().legionsPanel = city.transform.Find("CityCanvas").Find("CityLegionsPanel").gameObject;
        city.GetComponent<CityStats>().cityStatsText = city.transform.Find("CityCanvas").Find("CityStatsPanel").Find("CityStatsText").gameObject.GetComponent<Text>();
    }
}
