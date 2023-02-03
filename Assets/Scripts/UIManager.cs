using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image healthFill;
    public Image ammoFill;

    private Attack playerAmmo;
    private Target playerHealth;


    private void Awake()
    {
        playerAmmo= GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
        playerHealth= GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmoFill();
        UpdateHealthFill();
    }

    private void UpdateAmmoFill()
    {
        ammoFill.fillAmount = (float)playerAmmo.GetAmmo / playerAmmo.GetClipSize;  // ammoFill i fillAmount ile b�y�t�p k���lt�yoruz ve float de�er al�yor 0-1 aras�. E�er oraya float koymazsak ya 0 ya da 1 d�nece�i i�in 0.6 gibi de�erler d�enemeyecek.
    }

    private void UpdateHealthFill()
    {
        healthFill.fillAmount = (float)playerHealth.GetHealth/ playerHealth.GetMaxHealth;
    }

}
