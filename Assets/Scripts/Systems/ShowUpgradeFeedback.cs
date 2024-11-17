using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowUpgradeFeedback : MonoBehaviour
{
    private readonly string DASH_UPGRADE_TIP = "Congratulations! You unlocked the dash ability. Press L1 to use it";
    private readonly string BLACK_HOLE_UPGRADE_TIP = "Congratulations! You unlocked the black hole ability. Press R2 to use it";
    private readonly string SUN_UPGRADE_TIP = "Congratulations! You unlocked the sun ability. Press L2 to use it";

    [SerializeField] private PowerUp.PowerUpType type;
    [SerializeField] private GameObject feedbackScreen;
    [SerializeField] private TextMeshProUGUI feedbackLabel;

    public void ShowFeedback(PowerUp.PowerUpType powerUpType)
    {
        type = powerUpType;
        StartCoroutine(ShowFeedbackCoroutine());
    }

    private IEnumerator ShowFeedbackCoroutine()
    {
        feedbackScreen.SetActive(true);
        feedbackLabel.text = type switch
        {
            PowerUp.PowerUpType.BLACK_HOLE => BLACK_HOLE_UPGRADE_TIP,
            PowerUp.PowerUpType.SUN => SUN_UPGRADE_TIP,
            PowerUp.PowerUpType.DASH => DASH_UPGRADE_TIP
        };

        yield return new WaitForSeconds(5f);
        feedbackScreen.SetActive(false);
    }
}
