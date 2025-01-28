using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static bool isGameOver;

    [field: SerializeField]
    public int Score { get; private set; }

    [Header("Game Over UI")]
    [SerializeField] private RectTransform gameoverPanel;
    [SerializeField] private RectTransform gameSuccess;
    [SerializeField] private RectTransform gameFailed;

    [Header("Bad Bubbles")]
    [SerializeField] private List<BadBubble> badBubbleList;
    [SerializeField] private int initialBadBubblesCount;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        Score = 0;
        gameoverPanel.gameObject.SetActive(false);
        isGameOver = false;
    }

    private void Start()
    {
        badBubbleList = FindObjectsByType<BadBubble>(FindObjectsSortMode.None).ToList();
        initialBadBubblesCount = badBubbleList.Count;
    }

    public void UpdateScore(int amount = 1)
    {
        Score += amount;
    }

    public void ShowGameOver(bool isSccess)
    {
        gameSuccess.gameObject.SetActive(isSccess);
        gameFailed.gameObject.SetActive(!isSccess);
        gameoverPanel.gameObject.SetActive(true);
    }

    public void RemoveFromBubbleList(BadBubble badBubble)
    {
        badBubbleList.Remove(badBubble);
        
        if (badBubbleList.Count == 0)
        {
            ShowGameOver(Score == initialBadBubblesCount);
        }
    }
}
