using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCrit)
    {
        Transform damagePopupTransform = Instantiate(CommonAssetsUsing.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCrit);
        return damagePopup;
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


    public void Setup(int damageAmount, bool isCrit)
    {
        Color newColor;
        if(isCrit)
        {
            textMesh.fontSize = 5;
            ColorUtility.TryParseHtmlString("#EA0837", out newColor);
            textColor = newColor;
        } else
        {
            ColorUtility.TryParseHtmlString("#D94D0C", out newColor);
            textMesh.fontSize = 4;
            textColor = newColor;
        }
        textMesh.SetText(damageAmount.ToString());
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

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            float increseScaleAmount = 1f;
            transform.localScale += Vector3.one * increseScaleAmount * Time.deltaTime;
        } else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0 )
            {
                Destroy(gameObject);
            }
        }
    }

}
