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

        tmp.SetText(dc.minutes + ":" + (int)dc.seconds);
        tmp.SetText(tmp.text + "\n times smashed: " + dc.timesSmashed);
        tmp.SetText(tmp.text + "\n birds killed: " + dc.birdsKilled);
        tmp.SetText(tmp.text + "\n hearts collected: " + dc.heartsCollected);
        tmp.SetText(tmp.text + "\n kill to smash ratio: " + dc.killToSmashRatio);
    }
}
