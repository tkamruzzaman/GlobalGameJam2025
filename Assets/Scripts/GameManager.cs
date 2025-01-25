using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField]
    public int Score{get; private set;}

    private void Awake() 
    {
        if(Instance == null){  Instance = this; }

        Score = 0;
    }

    public void UpdateScore(int amount)
    {
        Score += amount;
    }

}
