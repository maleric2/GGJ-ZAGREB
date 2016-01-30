using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.ListView;
using System;

public class GameManager : DefaultManagerView
{

    public static GameManager instance;
    private int score = 0;

    public Text scoreLabel;

    private GameController controller;

    public int maxLevelScore;

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
        scoreLabel.text = score.ToString() + " / " + maxLevelScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore()
    {
        score++;
        scoreLabel.text = score.ToString() + " / " + maxLevelScore.ToString() ;

    }

    public override void OnBackButton()
    {
        throw new NotImplementedException();
    }
}
