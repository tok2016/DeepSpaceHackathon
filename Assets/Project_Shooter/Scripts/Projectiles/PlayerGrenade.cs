using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Shooter.Gameplay
{
    public class PlayerGrenade : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 m_StartPosition;
        [HideInInspector]
        public Vector3 m_TargetPosition;

        public AnimationCurve m_MoveCurve;

        public float Damage;
        public float ExplosionRange;
        public GameObject m_ExplodeParticle;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Co_Move());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator Co_Move()
        {
            float lerp = 0;
            while(lerp<1)
            {
                transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, lerp);
                transform.position += new Vector3(0, m_MoveCurve.Evaluate(lerp),0);
                lerp += Time.deltaTime;
                yield return null;
            }

            transform.position =  m_TargetPosition;
            Explode();
            CameraControl.m_Current.StartShake(.3f, .15f);
            GameObject obj = Instantiate(m_ExplodeParticle);
            obj.transform.position = transform.position;
            Destroy(obj, 3);
            Destroy(gameObject);
        }

        void Explode()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRange * transform.localScale.x);
            foreach (var col in hitColliders)
            {
                DamageControl d = col.gameObject.GetComponent<DamageControl>();
                if (d != null)
                {
                    d.ApplyDamage(Damage, transform.forward, 1);
                }
            }
        }
    }
}