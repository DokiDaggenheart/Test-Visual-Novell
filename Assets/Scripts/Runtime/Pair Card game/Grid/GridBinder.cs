using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class GridBinder : MonoBehaviour
{
    public static GridBinder Instance { get; private set; }
    public bool isGameEnded;
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 3;
    [SerializeField] private Vector2 startPosition = new Vector2(-2f, 2f);
    [SerializeField] private float offsetX = 1.5f;
    [SerializeField] private float offsetY = 2f;
    [SerializeField] private List<CardData> cardsData;
    private GridController _controller;
    private ICardSpawner _cardSpawner;

    [Inject]
    private void Construct(ICardSpawner cardSpawner)
    {
        _cardSpawner = cardSpawner;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        var gridModel = new GridModel(rows, columns, startPosition, offsetX, offsetY);
        _controller = new GridController(gridModel, cardsData, _cardSpawner);
        _controller.OnGameEnded += () => isGameEnded = true;
        StartGame();
    }

    public void StartGame()
    {
        _controller.Init();
    }
}
