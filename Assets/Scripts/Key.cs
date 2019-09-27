using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{

    public Text wpmText;
    public Text questText;
    public List<GameObject> keys = new List<GameObject>();
    Text myText;
    bool startTyping = false;
    bool endTyping = false;
    float timing = 0f;
    Mission m;

    // Start is called before the first frame update
    void Start()
    {
        m = Mission.mission;
        myText = GetComponent<Text>();
        myText.text = "|";
        wpmText.text = "0 wpm, 0 seconds";

        if (m.MissionCount == 0) questText.text = "Type any sentences.";
        questText.text = "Type \"" + m.CurrentMission + "\" as fast as possible.";
    }

    void Update()
    {
        // words per minute = 5 * characters per minute
        if (startTyping && !endTyping)
        {
            float timingDiff = Time.time - timing;
            if (timingDiff > 0)
                wpmText.text = ((int)((myText.text.Length - 1) / timingDiff * 12f * 1000f) / 1000f)
                    + " wpm, " + ((int)(timingDiff * 1000f) / 1000f) + " seconds";

            if (m.CurrentMission.Contains(myText.text.Substring(0, myText.text.Length - 1))) {
                myText.color = new Color(0f, 0f, 0f);
            }
            else
            {
                myText.color = new Color(0.8301887f, 0f, 0f);
            }
        }

        if (m.MissionCount == 0) return;

        if (!endTyping && myText.text.Equals(m.CurrentMission + "|"))
        {
            endTyping = true;
            m.IncreaseMissionNum();
            questText.text = "Type \"" + m.CurrentMission + "\" as fast as possible.";
            foreach (GameObject g in keys)
            {
                g.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void AddText(string c)
    {
        myText.text = myText.text.Replace("|", c + "|");

        if (!startTyping && myText.text.Length > 1)
        {
            startTyping = true;
            timing = Time.time;
        }
    }

    public void Backspace()
    {
        if (myText.text.Length > 1)
        {
            myText.text = myText.text.Substring(0, myText.text.Length - 2);
            myText.text += "|";
        }

        if (myText.text.Length <= 1)
        {
            startTyping = false;
            endTyping = false;
            wpmText.text = "0 wpm, 0 seconds";

            foreach (GameObject g in keys)
            {
                g.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void DeleteAllText()
    {
        myText.text = "|";
        startTyping = false;
        endTyping = false;
        wpmText.text = "0 wpm, 0 seconds";

        foreach (GameObject g in keys)
        {
            g.GetComponent<Button>().interactable = true;
        }
    }

    public void UseOptiKeyboard()
    {
        SceneManager.LoadScene(0);
        Screen.SetResolution(540, 960, false);
    }
    
    public void UseQwertyKeyboard()
    {
        SceneManager.LoadScene(1);
        Screen.SetResolution(770, 960, false);
    }

    public void UseDvorakKeyboard()
    {
        SceneManager.LoadScene(2);
        Screen.SetResolution(810, 960, false);
    }
}
