using UnityEngine;
namespace Shooter.Gameplay
{
    public class PWeapon_Rifle : Weapon_Base
    {
        private int BurstCount = 0;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (m_PowerLevel == 0)
            {
                FireDelay = 0.5f;
            }
            else if (m_PowerLevel == 1)
            {
                FireDelay = .33f;
            }
            if(m_PowerLevel == 0)
            {
                BurstDelay = 0.15f;
            }
            else if (m_PowerLevel == 1)
            {
                BurstDelay = 0.05f;
            }
            FireDelayTimer -= Time.deltaTime;
            if (FireDelayTimer <= 0)
                FireDelayTimer = 0;
            BurstDelayTimer -= Time.deltaTime;
            if (BurstDelayTimer <= 0)
                BurstDelayTimer = 0;
            RecoilTimer -= 10 * Time.deltaTime;
            if (RecoilTimer <= 0)
                RecoilTimer = 0;

            if (HoldToFire ? Input_FireHold : Input_FireDown)
            {
                if (FireDelayTimer == 0)
                {
                    if (BurstDelayTimer == 0)
                    {
                        FireWeapon();
                        BurstDelayTimer = BurstDelay;
                        BurstCount++;
                    }
                    if(BurstCount == 3 + m_PowerLevel*3)
                    {
                        FireDelayTimer = FireDelay;
                        RecoilTimer = 1f;
                        BurstCount = 0;
                    }
                }

            }
        }

        public override void FireWeapon()
        {

            GameObject obj;
            audioSource?.Play();

            obj = Instantiate(BulletPrefab);
            obj.transform.position = m_FirePoint.position;
            obj.transform.forward = Quaternion.Euler(0, 0, 0) * m_FirePoint.forward;
            Projectile_Base proj = obj.GetComponent<Projectile_Base>();
            proj.Creator = m_Owner;
            proj.Speed = ProjectileSpeed;
            proj.Damage = Damage;
            proj.m_Range = Range;
            Destroy(obj, 5);


            obj = Instantiate(EffectPrefab);
            obj.transform.SetParent(m_ParticlePoint);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.forward = m_ParticlePoint.forward;
            Destroy(obj, 3);
            Recoil(m_Owner);
        }
    }
}
