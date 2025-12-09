using Shooter.Gameplay;
using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private float m_Damage = 1;
    [SerializeField]
    private float m_TimeBetweenDamage = 1f;
    private float timer;

    private DamageControl m_DamageControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            m_DamageControl = PlayerChar.m_Current.gameObject.GetComponent<DamageControl>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(m_DamageControl)
            if(timer <= 0)
            { 
                m_DamageControl.ApplyDamage(m_Damage, m_DamageControl.transform.forward, 1);
                timer = m_TimeBetweenDamage;
            }
            else
                timer -= Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0;
        m_DamageControl = null;
    }
}
