using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] float currentScore = 0f;
    [SerializeField] float pointsToAdd = 10f;
    [SerializeField] float pointsToSubstract = 2f;
    [SerializeField] TMP_Text pointsText;

    void Start()
    {
        GameEvents.CollectibleEarned.AddListener(AddPoints);
        GameEvents.ObstacleHit.AddListener(SubtractPoints);
    }

    void AddPoints()
    {
        currentScore += pointsToAdd;
        pointsText.text = currentScore.ToString();
    }

    void SubtractPoints()
    {
        currentScore -= pointsToSubstract;
        if (currentScore < 0) currentScore = 0;
        pointsText.text = currentScore.ToString();
    }
}
