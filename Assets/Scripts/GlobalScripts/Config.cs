using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Config : MonoBehaviour {

    public static Dictionary<string, Dictionary<string, List<float>>> buildingconfig;
    public static Dictionary<string, int> legionsConfig;

    public static void Fill_list()
    {
        #region buildingconfig

        Dictionary<string, List<float>> farm;
        Dictionary<string, List<float>> harbour;
        Dictionary<string, List<float>> roads;
        Dictionary<string, List<float>> mine;
        Dictionary<string, List<float>> residence;
        Dictionary<string, List<float>> townhall;
        buildingconfig = new Dictionary<string, Dictionary<string,List<float>>>();

        farm = new Dictionary<string, List<float>>();
        List<float> farmBalances = new List<float>()
        {
            800,
            1500,
            5500
        };
        List<float> farmBuildmod = new List<float>()
        {
            0.01f
        };
        List<float> farmPopgrowth = new List<float>()
        {
            0.005f
        };
        farm.Add("Balance", farmBalances);
        farm.Add("Buildingmod", farmBuildmod);
        farm.Add("Popgrowth", farmPopgrowth);


        harbour = new Dictionary<string, List<float>>();
        List<float> harbourBalances = new List<float>()
        {
            2000,
            4000,
            7000,
        };
        List<float> harbourBuildmod = new List<float>()
        {
            0.05f
        };
        List<float> harbourPopgrowth = new List<float>()
        {
            0.001f
        };
        harbour.Add("Balance", harbourBalances);
        harbour.Add("Buildingmod", harbourBuildmod);
        harbour.Add("Popgrowth", harbourPopgrowth);

        roads = new Dictionary<string, List<float>>();
        List<float> roadsBalances = new List<float>()
        {
            700,
            1500,
            2800,
            4000
        };
        List<float> roadsBuildmod = new List<float>()
        {
            0.06f
        };
        List<float> roadsPopgrowth = new List<float>()
        {
            0.00f
        };
        roads.Add("Balance", roadsBalances);
        roads.Add("Buildingmod", roadsBuildmod);
        roads.Add("Popgrowth", roadsPopgrowth);

        mine = new Dictionary<string, List<float>>();
        List<float> mineBalances = new List<float>()
        {
            3000,
            7000
        };
        List<float> mineBuildmod = new List<float>()
        {
            0.10f
        };
        List<float> minePopgrowth = new List<float>()
        {
            0.0f
        };
        mine.Add("Balance", mineBalances);
        mine.Add("Buildingmod", mineBuildmod);
        mine.Add("Popgrowth", minePopgrowth);

        residence = new Dictionary<string, List<float>>();
        List<float> residenceBalances = new List<float>()
        {
            1500,
            3400,
            7500,
        };
        List<float> residenceBuildmod = new List<float>()
        {
            0.02f
        };
        List<float> residencePopgrowth = new List<float>()
        {
            0.004f
        };
        residence.Add("Balance", residenceBalances);
        residence.Add("Buildingmod", residenceBuildmod);
        residence.Add("Popgrowth", residencePopgrowth);

        townhall = new Dictionary<string, List<float>>();
        List<float> townhallBalances = new List<float>()
        {
            700,
            2000,
            10800,
            24000,
        };
        List<float> townhallBuildmod = new List<float>()
        {
            0.04f
        };
        List<float> townhallPopgrowth = new List<float>()
        {
            0.005f
        };
        townhall.Add("Balance", townhallBalances);
        townhall.Add("Buildingmod", townhallBuildmod);
        townhall.Add("Popgrowth", townhallPopgrowth);

        buildingconfig.Add("Farm", farm);
        buildingconfig.Add("Harbour", harbour);
        buildingconfig.Add("Roads", roads);
        buildingconfig.Add("Mine", mine);
        buildingconfig.Add("Residence", residence);
        buildingconfig.Add("Townhall", townhall);
        #endregion
        #region legionsconfig
        legionsConfig = new Dictionary<string, int>
        {
            { "Infantry", 200 },
            { "Archers", 180 },
            { "Cavalry", 300 },
            { "Garrison", 100 },
            { "Militia", 70 },
            { "Trebuchet", 500 }
        };

        #endregion
    }
}
