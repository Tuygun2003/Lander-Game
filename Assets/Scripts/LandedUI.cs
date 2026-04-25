using TMPro;
using UnityEngine;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TitleTextMesh;
    [SerializeField] private TextMeshProUGUI StateTextMesh;



    private void Start()
    {
        PlayerController.Instance.OnLanded += Lander_OnLanded;

        Hide();
    }

    private void Lander_OnLanded(object sender, PlayerController.OnLandedEventArgs e)
    {
        if(e.LandingType == PlayerController.LandingType.Success)
        {
            TitleTextMesh.text = "SUCCESSFUL LANDING!";
        }
        else
        {
            TitleTextMesh.text = "!!!!CRASH!!!!";
        }

        StateTextMesh.text = Mathf.Round(e.landingSpeed*2f) + "\n" +
            Mathf.Round(e.dotVector*100f) +  "\n " + e.score + "\n" +
            "x" + e.scoreMultiplier;


        Show();

    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
