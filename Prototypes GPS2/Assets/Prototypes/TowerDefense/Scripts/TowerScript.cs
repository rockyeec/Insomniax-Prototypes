using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private Transform gun = null;
    [SerializeField] private Transform turret = null;

    FireVariables fireVariables = new FireVariables();
    class FireVariables
    {
        float interval = 0.3f;
        float time = 0.0f;

        public bool IsNextInterval()
        {
            if (Time.time >= time)
            {
                time = Time.time + interval;
                return true;
            }
            return false;
        }
    }   

    private void Update()
    {
        float delta = Time.deltaTime;
        LookAt(delta);
    }

    void Fire(float delta)
    {
        if (fireVariables.IsNextInterval())
        {
            Instantiate(bullet, gun.position, gun.rotation);
        }
    }

    void LookAt(float delta)
    {
        Collider[] colliders = Physics.OverlapSphere(turret.position, 16.9f, 1 << 10);
        if (colliders.Length != 0)
        {
            var nearest = colliders.OrderBy(
                t => Vector3.Distance(transform.position, t.transform.position))
                .FirstOrDefault();

            Vector3 turretDir = nearest.transform.position - turret.position;
            turretDir.y = 0.0f;
            Quaternion towerRot = Quaternion.LookRotation(turretDir);
            turret.rotation = Quaternion.Slerp(turret.rotation, towerRot, 6.9f * delta);

            Vector3 gunDir = nearest.transform.position - gun.position;
            Quaternion gunRot = Quaternion.LookRotation(gunDir);
            Vector3 gunEul = gunRot.eulerAngles;
            gunEul.y = 0.0f;
            gunRot.eulerAngles = gunEul;
            gun.localRotation = Quaternion.Slerp(gun.localRotation, gunRot, 6.9f * delta);

            Fire(delta);
        }
    }
}
