using Shooter.Gameplay;
using UnityEngine;

public class HomingMine : MonoBehaviour
{
    [SerializeField]
    private float m_Damage = 2;
    [SerializeField]
    private float m_Speed = 1.5f;
    [SerializeField]
    private float m_ChasingRadius = 4;
    [SerializeField]
    private float m_ExplosionRadius = 6;

    [SerializeField]
    private GameObject m_Explosion;

    private DamageControl m_DamageControl;

    [SerializeField]
    LayerMask m_LayersToDetect;

    private void Awake()
    {
        m_DamageControl = GetComponent<DamageControl>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_DamageControl.IsDead || Physics.OverlapSphere(transform.position, transform.localScale.y, m_LayersToDetect).Length > 0)
            Explode();

        Chase();
    }

    private void Chase()
    {
        Vector3 vectorToPlayer = PlayerChar.m_Current.transform.position - transform.position;
        if (vectorToPlayer.magnitude <= m_ChasingRadius)
        {
            vectorToPlayer.y = 0;
            vectorToPlayer.Normalize();
            transform.position += vectorToPlayer * Time.deltaTime * m_Speed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_ChasingRadius);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    DamageControl damageControl = other.GetComponent<DamageControl>();
    //    if (damageControl != null)
    //    {
    //        damageControl.ApplyDamage(m_Damage, transform.forward, 1);
    //        Explode();
    //    }
    //}

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
        foreach (Collider col in hits)
        {
            if (col.gameObject.name == gameObject.name) continue;

            DamageControl damageControl = col.GetComponent<DamageControl>();
            if (damageControl != null)
                damageControl.ApplyDamage(m_Damage, transform.forward, 1);
        }

        Instantiate(m_Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
