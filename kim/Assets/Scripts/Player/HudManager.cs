using UnityEngine;

public class HudManager
{
    private int HIDDEN_Z_INDEX = 1;
    private int SHOW_Z_INDEX = 0; 
    private HeartRateManager heartRateManager;
    private GameObject hudDangerSprite;
    private GameObject hudWarningSprite;
    private GameObject hudOkSprite;
    private bool showDanger;
    private bool showOk;
    private bool showWarning;

    public HudManager(HeartRateManager heartRateManager)
    {
        this.heartRateManager = heartRateManager;
        hudDangerSprite = GameObject.Find("hudDangerSprite");
        hudWarningSprite = GameObject.Find("hudWarningSprite");
        hudOkSprite = GameObject.Find("hudOkSprite");

        showOk = true;
        showDanger = false;
        showWarning = false;
    }

    public void Update()
    {
        float currentHeartRate = heartRateManager.GetCurrentHeartRate();
        if (currentHeartRate < HeartRateManager.MODERATE_HEART_RATE_BREAKPOINT)
        {
            if (!showOk)
            {
                hudOkSprite.transform.Translate(new Vector3(0.0f, 0.0f, -1.0f));
                showOk = true;
            }
            if (showWarning)
            {
                hudWarningSprite.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f));
                showWarning = false;
            }
            if (showDanger)
            {
                hudDangerSprite.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f));
                showDanger = false;
            }
        }
        else if (currentHeartRate < HeartRateManager.HIGH_HEART_RATE_BREAKPOINT)
        {

            if (showOk)
            {
                hudOkSprite.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f));
                showOk = false;
            }
            if (!showWarning)
            {
                hudWarningSprite.transform.Translate(new Vector3(0.0f, 0.0f, -1.0f));
                showWarning = true;
            }
            if (showDanger)
            {
                hudDangerSprite.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f));
                showDanger = false;
            }
        }
        else
        {

            if (showOk)
            {
                hudOkSprite.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f));
                showOk = false;
            }
            if (showWarning)
            {
                hudWarningSprite.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f));
                showWarning = false;
            }
            if (!showDanger)
            {
                hudDangerSprite.transform.Translate(new Vector3(0.0f, 0.0f, -1.0f));
                showDanger = true;
            }
        }

    }


}

