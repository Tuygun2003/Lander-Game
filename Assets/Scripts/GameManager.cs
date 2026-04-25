using System;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float score;
    private float time;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        PlayerController.Instance.CoinSystem += CoinSystem;
        PlayerController.Instance.OnLanded += Landed_OnLanded;
    }
    private void Update()
    {
        time += Time.deltaTime;
    }

    private void Landed_OnLanded(object sender,PlayerController.OnLandedEventArgs e)
    {
        AddScore(e.score);

    }
    private void CoinSystem(object sender, System.EventArgs e)
    {
        AddScore(500);

    }

    private void AddScore(int amountOfScoreRise)
    {

        score += amountOfScoreRise;
        Debug.Log("score: " +  score);
       

    }

    public float GetScore()
    {

        return score;
    }
    public float GetTime()
    {

        return time;
    }

}
