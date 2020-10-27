using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Display the Health
    public GameObject healthBarUI;  //Canvas
    public Slider slider;           //Slider

    [SerializeField]
    private float maxHealth = 10f;
    private float currentHealth;
    private Animator anim;
    private Camera cam;

    // OnEnable
    void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        slider.value = CalculateHealth();
        healthBarUI.SetActive(false); //Show Healthbar upon injury
    }

    void LateUpdate()
    {
        healthBarUI.transform.LookAt(cam.transform);
        healthBarUI.transform.rotation = Quaternion.LookRotation(cam.transform.forward);
    }

    //Taking Damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarUI.SetActive(true); //Show Healthbar upon injury
        slider.value = CalculateHealth();

        if (anim != null) //Checks to see if there is an Animator
        {
            anim.SetTrigger("damage");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Calculate the Health for the Slider
    float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    // You Die
    private void Die()
    {
        if (anim != null) //Checks to see if there is an Animator
        {
            anim.SetTrigger("death");
        }

        healthBarUI.SetActive(false);//Hide Healthbar
        Destroy(gameObject, .1f);
    }
}