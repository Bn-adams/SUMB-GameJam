using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class DeathScript : MonoBehaviour
{
    [SerializeField] GameObject HUD;
    [SerializeField] TextMeshProUGUI txt1;
    [SerializeField] TextMeshProUGUI txt2;
    [SerializeField] MapGen mapGen;
    [SerializeField] Aim aim;
    [SerializeField] Pc pc;

    List<string> list = new List<string>();


    void Start()
    {


    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        list.Add("Breader luck next time.");
        list.Add("History repeats, you dont.");
        list.Add("The revolution has revolved on you.");
        list.Add("You lost your head, literally.");
        list.Add("Headless, but not fearless.");

        UnityEngine.Cursor.visible = true;
        HUD.SetActive(false);
        string output = "You beat:" + mapGen.RoomCounter.ToString() + " tiles";
        txt2.text = output;
        string outp = list[Random.Range(0, list.Count)];
        txt1.text = outp;
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
    }
}
