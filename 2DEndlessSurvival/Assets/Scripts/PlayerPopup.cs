using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPopup : MonoBehaviour
{
    public static PlayerPopup Create(Vector3 position, int amount, bool isDamage = false, bool isGainHealth = false, bool isGainMana = false)
    {
        Transform playerPopupTransform = Instantiate(CommonAssetsUsing.i.pfPlayerPopup, position, Quaternion.identity);
        PlayerPopup playerPopup = playerPopupTransform.GetComponent<PlayerPopup>();
        playerPopup.Setup(amount, isDamage, isGainHealth, isGainMana);
        return playerPopup;
    }

    private static int sortingOrder;
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private float DISAPPEAR_TIMER_MAX = 1f;
    private Vector3 moveVector;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int amount, bool isDamage, bool isGainHealth, bool isGainMana)
    {
        Color newColor;
        if (isDamage)
        {
            ColorUtility.TryParseHtmlString("#F11611", out newColor);
            textMesh.fontSize = 3;
            textColor = newColor;
            textMesh.SetText("- " + amount.ToString());
        }
        if(isGainHealth)
        {
            ColorUtility.TryParseHtmlString("#13AE22", out newColor);
            textMesh.fontSize = 4;
            textColor = newColor;
            textMesh.SetText("+ " + amount.ToString());
        }
        if(isGainMana)
        {
            ColorUtility.TryParseHtmlString("#1128F1", out newColor);
            textMesh.fontSize = 4;
            textColor = newColor;
            textMesh.SetText("+ " + amount.ToString());
        }        
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        moveVector = new Vector3(0, 1) * 5f;
    }

    public void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
