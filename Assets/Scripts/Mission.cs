using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public static Mission mission;

    List<string> missions = new List<string>();
    int missionNum = 0;

    public string CurrentMission
    {
        get
        {
            return missions[missionNum];
        }
    }

    public int MissionCount
    {
        get
        {
            return missions.Count;
        }
    }

    void Awake()
    {
        if (Mission.mission != null)
        {
            Destroy(this.gameObject);
            return;
        }
        mission = this;
        DontDestroyOnLoad(this.gameObject);

        Screen.SetResolution(540, 960, false);

        try
        {
            string line;
            StreamReader file = new StreamReader(@"Mission.txt");
            while((line = file.ReadLine()) != null)
            {
                missions.Add(line.TrimEnd('\n'));
            }
            file.Close();
        }
        catch (System.Exception e)
        {
            if (e is FileNotFoundException || e is IsolatedStorageException)
            {
                missions = new List<string>();
                AddBasicStrings(missions);
            }
        }

        List<string> temp = new List<string>();
        foreach (string s in missions)
        {
            string tmp;
            bool b = true;
            tmp = s.ToUpperInvariant();
            foreach (char c in tmp.ToCharArray())
            {
                if (c != ' ' && (c < 'A' || c > 'Z'))
                {
                    b = false;
                    break;
                }
            }
            if (b)
            {
                temp.Add(tmp);
            }
        }

        if (temp.Count == 0)
        {
            missions = new List<string>();
            AddBasicStrings(missions);
        }
        else
        {
            missions = temp;
        }
    }

    void AddBasicStrings(List<string> list)
    {
        list.Add("THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG");
        list.Add("SUCH CIRCUMSTANCES ARE RANDOM VARIABLES");
        list.Add("THEY DECIDE TO CONDUCT A SMALL EXPERIMENT TO FIND OUT");
        list.Add("A GOOD EXAMPLE IS THE ACCELEROMETER");
        list.Add("THIS WAS NOVEL AT THE TIME");
    }

    public void IncreaseMissionNum()
    {
        missionNum++;
        if (missions.Count <= missionNum) missionNum = 0;
    }

}