using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI statsTextMesh;
    [SerializeField] private GameObject speedUpArrowKey;
    [SerializeField] private GameObject speedDownArrowKey;
    [SerializeField] private GameObject speedLeftArrowKey;
    [SerializeField] private GameObject speedRightArrowKey;
    [SerializeField] private Image fuelAmount;



    private void Update()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {




        speedUpArrowKey.SetActive(PlayerController.Instance.GetSpeedY()>=0);
        speedDownArrowKey.SetActive(PlayerController.Instance.GetSpeedY() < 0);
        speedLeftArrowKey.SetActive(PlayerController.Instance.GetSpeedX() < 0);
        speedRightArrowKey.SetActive(PlayerController.Instance.GetSpeedX() >= 0);

        fuelAmount.fillAmount= PlayerController.Instance.GetFuelAmountNormalized();

        statsTextMesh.text = GameManager.Instance.GetScore() + "\n"
           +Mathf.Round(GameManager.Instance.GetTime()) + "\n"+
           Mathf.Abs(Mathf.Round(PlayerController.Instance.GetSpeedY()*10f)) + "\n"
           +Mathf.Abs(Mathf.Round(PlayerController.Instance.GetSpeedX()*10f)) + "\n"
           ;





    }
}
