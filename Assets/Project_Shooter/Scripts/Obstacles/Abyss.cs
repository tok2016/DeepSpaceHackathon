using Shooter.Gameplay;
using UnityEngine;

public class Abyss : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageControl damageControl = other.GetComponent<DamageControl>();
        if (damageControl != null)
            damageControl.ApplyDamage(1000, other.transform.forward, 1);
    }
}
