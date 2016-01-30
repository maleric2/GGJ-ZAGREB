using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.ListView;
using System;

public class GameManager : DefaultManagerView
{

    public static GameManager instance;

    public Text scoreLabel;

    private GameController controller;

    void Awake()
    {
        if (instance == null)
            instance = gameObject.GetComponent<GameManager>();
        if (controller == null)
            controller = new GameController();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetScore(int score)
    {
        this.controller.Score = score;
        scoreLabel.text = this.controller.Score.ToString();

    }
    public void AddScore(int score)
    {
        this.controller.Score += score;
        if(scoreLabel!=null) scoreLabel.text = this.controller.Score.ToString();

    }

    public override void OnBackButton()
    {
        throw new NotImplementedException();
    }
    public void OnProperties()
    {
        //
    }
}
