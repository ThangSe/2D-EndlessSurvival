using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    private bool coolDownFade;
    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }
    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        float delayTime = 1f;
        transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealPercent(), 1);        
        if (!coolDownFade) StartCoroutine(Effect(delayTime, coolDownFade));
    }

    private IEnumerator Effect(float delayTime, bool coolDownFade)
    {
        this.coolDownFade = !coolDownFade;
        yield return new WaitForSeconds(delayTime);
        transform.Find("BarDamaged").localScale = new Vector3(healthSystem.GetHealPercent(), 1);
        this.coolDownFade = coolDownFade;
    }

    private void Update()
    {
        
    }
}
