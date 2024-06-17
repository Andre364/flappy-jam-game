using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayData : MonoBehaviour
{
    DataCollector dc;

    public TextMeshProUGUI tmp;

    private void Start()
    {
        dc = GameObject.Find("DataCollector").GetComponent<DataCollector>();

        //tmp.SetText(dc.minutes + ":" + (int)dc.seconds);

        string mins = dc.minutes.ToString();
        mins += " minute" + plural(dc.minutes) + " and ";
        if (dc.minutes == 0)
        {
            mins = "";
        }
        string secs = ((int)dc.seconds).ToString() + " second";
        secs += plural((int)dc.seconds);

        tmp.SetText("you spent " + mins + secs + " hunting... wow");
        tmp.SetText(tmp.text + "\n you smashed " + dc.timesSmashed + " time" + plural(dc.timesSmashed));
        tmp.SetText(tmp.text + "\n and killed " + dc.birdsKilled + " bird" + plural(dc.birdsKilled) + ".");
        tmp.SetText(tmp.text + "\n you collected " + dc.heartsCollected + " heart" + plural(dc.heartsCollected));
        tmp.SetText(tmp.text + "\n and each smash amounted to " + dc.killToSmashRatio + " dead birds");

        Destroy(dc.gameObject);
    }

    string plural(int i)
    {
        if (i != 1)
        {
            return "s";
        }
        else
        {
            return "";
        }
    }
}
