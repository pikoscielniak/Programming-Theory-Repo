using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Get => GameObject.Find("GameManager").GetComponent<GameManager>();
    public bool IsGameOn => _isGameOn;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private GameObject gameLostPanel;
    [SerializeField] private GameObject onGamePanel;
    [SerializeField] private TextMeshProUGUI gameDurationText;
    [SerializeField] private Player player;

    private readonly List<GameObject> _panels = new(4);
    private float _gameDuration;
    private bool _isGameOn;
    private float _startTime;

    private void Start()
    {
        AddPanels();
        TurnOnPanel(startPanel);
    }

    private void AddPanels()
    {
        _panels.Add(startPanel);
        _panels.Add(gameLostPanel);
        _panels.Add(gameWonPanel);
        _panels.Add(onGamePanel);
    }

    // ABSTRACTION
    private void TurnOnPanel(GameObject gameObj)
    {
        foreach (var panel in _panels)
        {
            panel.SetActive(panel == gameObj);
        }
    }

    public void StartEasy()
    {
        StartGame(Levels.Easy);
    }

    public void StartMedium()
    {
        StartGame(Levels.Medium);
    }

    public void StartHard()
    {
        StartGame(Levels.Hard);
    }

    private void StartGame(Levels level)
    {
        TurnOnPanel(onGamePanel);
        _gameDuration = GameDurationsForLevel[level];
        _startTime = Time.time;
        _isGameOn = true;
        player.StartNewGame();
    }

    private void Update()
    {
        if (!_isGameOn)
        {
            return;
        }

        var gameTime = Time.time - _startTime;

        gameDurationText.text = $"Time left: {_gameDuration - gameTime:F2}";

        var isGameFinished = gameTime >= _gameDuration;
        if (isGameFinished)
        {
            GameWon();
        }
    }

    private void GameWon()
    {
        _isGameOn = false;
        TurnOnPanel(gameWonPanel);
    }

    public void ShowStartMenu()
    {
        TurnOnPanel(startPanel);
    }

    public void GameLost()
    {
        _isGameOn = false;
        TurnOnPanel(gameLostPanel);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private static readonly Dictionary<Levels, int> GameDurationsForLevel = new()
    {
        {Levels.Easy, 60},
        {Levels.Medium, 120},
        {Levels.Hard, 180},
    };
}

public enum Levels
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}