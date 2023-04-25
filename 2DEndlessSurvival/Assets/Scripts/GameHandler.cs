using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Player player;
    public Transform playerHealthBar;
    private void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);
        Transform healthBarTransform = Instantiate(playerHealthBar, player.transform.GetChild(0).Find("HealthBarPoint").position, Quaternion.identity, player.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }
}
