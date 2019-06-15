using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpButtonManager : MonoBehaviour
{
    public List<GameObject> PowerUpButtons;
    public List<GameObject> Locations;
    public GameObject PowerUpPanel;

    private void Start()
    {
        for (int i = 0; i < Locations.Count; i++)
        {
            NewButton(i);
        }
    }

    GameObject RngButton()
    {
        int pos = Random.Range(0, PowerUpButtons.Count);
        return PowerUpButtons[pos];
    }

    public void NewButton(int location)
    {
        GameObject go = Instantiate(RngButton(), Locations[location].transform.position, Quaternion.identity);
        go.GetComponent<PowerUpButton>().location = location;
        go.transform.SetParent(PowerUpPanel.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.GetComponent<PowerUpButton>().PowerUpManager = gameObject;
        go.SetActive(true);

    }
}
