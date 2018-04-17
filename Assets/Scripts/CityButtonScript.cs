using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CityButtonScript : MonoBehaviour {
    public Dictionary<string, Dictionary<string, int>> levelsTown; // A dictionary containing the level dictionary per town.

    // Panels
    public GameObject buildingPanel;
    public GameObject legionsPanel;
    public GameObject army;

    void Start()
    {
        levelsTown = new Dictionary<string, Dictionary<string, int>>();
        buildingPanel.SetActive(false);
        legionsPanel.SetActive(false);
    }

    public void ClickBuildingButton()
    {
        
        GameObject button = EventSystem.current.currentSelectedGameObject; // The button that was clicked
        GameObject city = button.transform.parent.parent.parent.gameObject;
        CityStats buttonParent = city.GetComponent<CityStats>();  // Gets the parent from the button that was clicked, aka a City

        string buildingname = "";
        for (int i = 0; i < button.name.Length; i++) // Removes "Button" from the string, leaving the name of the building. Eg, FarmButton becomes Farm.
        {
            buildingname = buildingname + button.name[i];
            if (button.name[i + 1].Equals('B'))
            {
                break;
            }
        }
        if (!levelsTown.ContainsKey(city.name))
        {
            levelsTown.Add(city.name, new Dictionary<string, int>());
        }

        if (!levelsTown[city.name].ContainsKey(buildingname)) // If the key is not yet in the dictionary it will be added.
        {
            levelsTown[city.name].Add(buildingname, 0);
        }


        if (buildingname == "Townhall")
        {
            buttonParent.popMax = 1200 * Mathf.Pow(levelsTown[city.name]["Townhall"] + 1, 2) + 5000;
        }

        if (Config.buildingconfig[buildingname]["Balance"].Count > levelsTown[city.name][buildingname] && BalanceController.balance >= (int)Config.buildingconfig[buildingname]["Balance"][levelsTown[city.name][buildingname]]) // There needs to be a level for the specific building and enough money.
        {
            // Update balance, buildingmod and popgrowth based on the buildingname config.
            Stats_update((int)Config.buildingconfig[buildingname]["Balance"][levelsTown[city.name][buildingname]],
                Config.buildingconfig[buildingname]["Buildingmod"][0],
                Config.buildingconfig[buildingname]["Popgrowth"][0], 0, buttonParent);
            if (Config.buildingconfig[buildingname]["Balance"].Count > (levelsTown[city.name][buildingname]) + 1) // Update to the price of the next level, if there is one.
            {
                button.GetComponentInChildren<Text>().text = buildingname + " " + (levelsTown[city.name][buildingname] + 2).ToString()  // Takes the buttons children text and updates the price
                    + ": " + (int)Config.buildingconfig[buildingname]["Balance"][levelsTown[city.name][buildingname] + 1];
            }
            else
            {
                button.GetComponentInChildren<Text>().text= buildingname+ " Max";
            }
            levelsTown[city.name][buildingname]++;
        }
    }

    public void ClickLegionsButton()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject; // The button that was clicked
        CityStats buttonParent = button.transform.parent.parent.parent.gameObject.GetComponent<CityStats>(); // Gets the parent from the button that was clicked, aka a City

        string legionname = "";
        for (int i = 0; i < button.name.Length; i++) // Removes "Button" from the string, leaving the name of the building. Eg, FarmButton becomes Farm.
        {
            legionname = legionname + button.name[i];
            if (button.name[i + 1].Equals('B'))
            {
                break;
            }
        }
        if (Config.legionsConfig[legionname] <= BalanceController.balance && buttonParent.population > 20)
        {
            Stats_update(Config.legionsConfig[legionname], 0, 0, 1, buttonParent);
        }
    }

    private void Stats_update(int Balance, double buildingmod, double popgrowth, int army, CityStats buttonparent) // Updating popgrowth, buildingmod and balance based on button clicked.
    {
        if (buttonparent.building_modifier < 100) // To be sure
        {
            buttonparent.building_modifier += (float)buildingmod;
        }        
        buttonparent.population_growth += (float)popgrowth;
        BalanceController.balance += -Balance;
        buttonparent.armySize += army;
    }

    public void OnBuildings() // On click buildings button, if building menu is active it will deactivate and vice versa.
    {
        if (buildingPanel.activeSelf)
        {
            buildingPanel.SetActive(false);
        }
        else
        {
            buildingPanel.SetActive(true);
        }      
        legionsPanel.SetActive(false);
    }

    public void OnLegions() // On click legions button, if legions menu is active it will deactivate and vice versa.
    {
        buildingPanel.SetActive(false);
        if (legionsPanel.activeSelf)
        {
            legionsPanel.SetActive(false);
        }
        else
        {
            legionsPanel.SetActive(true);
        }     
    }

    public void Send_Armies() // Send armies button
    {
        GameObject button = EventSystem.current.currentSelectedGameObject; // The button that was clicked
        CityStats buttonParent = button.transform.parent.parent.gameObject.GetComponent<CityStats>(); // Gets the parent from the button that was clicked, aka a City

        if (buttonParent.armySize > 0) // Makes a new army object
        {
            // Make a new army
            GameObject newArmy = Instantiate(army, buttonParent.transform.position, Quaternion.identity);
            StaticLibrary.DeselectAll(); // All others all deselected

            newArmy.transform.parent = GameObject.Find("_Dynamic").transform; // Becomes a dynamic object
            newArmy.GetComponent<ArmyStats>().armySize = buttonParent.armySize;            
            newArmy.GetComponent<ArmyStats>().isSelected = true; // The city spawned is selected
            newArmy.GetComponent<ArmyStats>().spawnNumber = StaticLibrary.spawnArmyCounter;
            newArmy.GetComponent<ArmyStats>().playerNumber = buttonParent.playerNumber; 

            buttonParent.armySize = 0; // The city where the army spawned from now has 0 armies
                  
            StaticLibrary.spawnArmyCounter++;

            Transform[] parameters = new Transform[2] {newArmy.transform, buttonParent.transform};
            StopCoroutine("DeleteArmy");
            StartCoroutine("DeleteArmy", parameters);
        }
    }

    IEnumerator DeleteArmy(Transform[] para) // When the army is not moved for 2 seconds it will be removed and added to the city armySize
    {
        yield return new WaitForSeconds(2f);
        if (para[0] == null)
            yield break;
        if (para[1].position == para[0].position)
        {
            para[1].GetComponent<CityStats>().armySize += para[0].GetComponent<ArmyStats>().armySize;
            Destroy(para[0].gameObject);
            StaticLibrary.spawnArmyCounter--;
        }
    }

    public void Close_Button()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject; // The button that was clicked
        button.transform.parent.gameObject.SetActive(false);           
    }	
}
