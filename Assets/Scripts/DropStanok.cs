using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropStanok : MonoBehaviour
{
    [SerializeField] Text stanokName;
    [SerializeField] List<GameObject> stanki;
    [SerializeField] GameObject panel;
    public void Delete()
    {
            foreach (GameObject stanok in stanki)
            {
                if (stanok.name == stanokName.text)
                {
                    stanok.SetActive(false);
                    panel.SetActive(false);
                    stanok.GetComponent<EconomyScript>().currentLvl = 0;
                    stanok.GetComponent<EconomyScript>().rashod = 150;
                    stanok.GetComponent<EconomyScript>().modernCost = 153;
                break;
                }
            }
    }
}
