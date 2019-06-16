using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpButton : MonoBehaviour
{
    public Image ButtonImage;
    public Image ButtonColor;
    public TextMeshProUGUI ButtonName;
    public TextMeshProUGUI ButtonCost;

    [HideInInspector]
    public GameObject PowerUpManager;

    public Animator anim;

    [HideInInspector]
    public int location;

    public int Cost;
    public Color Color;

    public void Start()
    {
        PowerUpManager=FindObjectOfType<PowerUpButtonManager>().gameObject;
    }

    private void OnEnable()
    {
       anim.SetTrigger("GoIn");
    }

    public void SetColor(Color c)
    {
        Color = c;
        ButtonColor.GetComponent<Image>().color = c;
    }

    public void SetButtonName(string n)
    {
        ButtonName.text = n;
    }
    
    public void SetButtonCost(int i)
    {
        Cost = i;
        ButtonCost.text = i.ToString();
    }

    public void payCost()
    {
        FindObjectOfType<ScoreAndTimer>().AddScore(-Cost);
    }
    
    public virtual void DoAction()
    {
        anim.SetTrigger("GoOut");
        Destroy(gameObject, 3f);
    }

    private void OnDisable()
    {
        NewButton();
    }

    public void NewButton()
    {
        PowerUpManager.GetComponent<PowerUpButtonManager>().NewButton(location);
    }
}
