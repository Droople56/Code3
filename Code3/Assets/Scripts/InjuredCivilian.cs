using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredCivilian : MonoBehaviour
{
    private int health;
    private float currentTime;
    private Renderer meshRenderer;
    private bool isDying;
    private bool isDead;

    [SerializeField]
    private float damageTimer = 6f;

    public Color defaultColor;
    public Color damagedColor;
    public Color deadColor;

    [SerializeField]
    private int flashTimer = 2;

    // Start is called before the first frame update
    void Start()
    {
        health = 20;
        isDying = true;
        meshRenderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= damageTimer && isDying)
        {
            StartCoroutine(FlashRed(flashTimer));
            TakeDamage(10);

            currentTime = 0;
        }
        if (isDead)
        {
            meshRenderer.material.color = deadColor;
        }
    }

    /// <summary>
    /// Reduces the civilian's health.
    /// </summary>
    /// <param name="damage">Amount of damage taken</param>
    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
            
        }
    }

    /// <summary>
    /// Flashes a red color when the civilian is injured
    /// </summary>
    /// <param name="flashTime">The flash timer</param>
    /// <returns>Red flashy color</returns>
    public IEnumerator FlashRed(int flashTime)
    {
        // Flash renderer different colors
        for (int i = 0; i < flashTime; i++)
        {
            meshRenderer.material.color = damagedColor;
            yield return new WaitForSeconds(.15f);
            meshRenderer.material.color = defaultColor;
            yield return new WaitForSeconds(.15f);
        }
    }

    /// <summary>
    /// Destroys the game object and stops flashing red
    /// </summary>
    void Death()
    {
        StopCoroutine(FlashRed(flashTimer));
        isDying = false;
        isDead = true;
        //Destroy(this.gameObject);
    }

    //For Defib
    public void Revive()
    {
        if (isDead)
        {
            health = 50;
            isDead = false;
            isDying = true;
        }
        else
        {
            Debug.Log("Victim doesn't need a shock.");
        }
        
    }

    //For Medkit
    public void Heal()
    {
        if (!isDead)
        {
            health = 100;
            isDying = false;
            gameObject.transform.Rotate(new Vector3(0, 0, -90));
        }
        else
        {
            Debug.Log("Victim needs defib.");
        }
    }
}
