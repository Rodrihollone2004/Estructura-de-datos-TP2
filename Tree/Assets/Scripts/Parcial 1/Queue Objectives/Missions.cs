using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Missions : MonoBehaviour
{
    TMP_Text missionsText;

    Queue<string> missions = new Queue<string>();

    public Queue<string> _Missions { get => missions; set => missions = value; }

    private void Awake()
    {
        missionsText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        missions.Enqueue("Mision 1: Recoger la manzana");
        missions.Enqueue("Mision 2: Recoger las bananas");
        missions.Enqueue("Mision 3: Recoger la sandía");
        missions.Enqueue("Mision 4: Recoger la pera");
        missions.Enqueue("Mision 5: Recoger la naranja");
        UIUpdate();
    }

    public void UIUpdate() 
    {
        if (missions.Count > 0)
        {
            ShowTexts();
        }
        else
        {
            missionsText.text = "Juego completado";
        }
    }

    private void ShowTexts()
    {
        string actualMission = missions.Peek();
        missionsText.text = actualMission;
    }
}
