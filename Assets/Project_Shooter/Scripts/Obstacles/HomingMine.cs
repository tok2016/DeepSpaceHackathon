using Shooter.Gameplay;
using UnityEngine;

public class HomingMine : MonoBehaviour
{
    [SerializeField]
    private float m_Danage = 2;
    [SerializeField]
    private float m_Speed = 1.5f;
    [SerializeField]
    private float m_ChasingRadius = 4;

    [SerializeField]
    private GameObject m_Explosion;

    private DamageControl m_DamageControl;

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
        if(m_DamageControl.IsDead)
            Explode();
        
        Vector3 vectorToPlayer = PlayerChar.m_Current.transform.position - transform.position;
        if(vectorToPlayer.magnitude <= m_ChasingRadius)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerChar.m_Current.gameObject.GetComponent<DamageControl>()?.ApplyDamage(m_Danage, transform.forward, 1);
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(m_Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
