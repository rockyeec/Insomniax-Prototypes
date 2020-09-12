using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private Transform gun = null;
    [SerializeField] private Transform turret = null;


    private void Update()
    {
        LookAt();
    }

    void LookAt()
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
            turret.rotation = Quaternion.Slerp(turret.rotation, towerRot, 6.9f * Time.deltaTime);

            Vector3 gunDir = nearest.transform.position - gun.position;
            Quaternion gunRot = Quaternion.LookRotation(gunDir);
            Vector3 gunEul = gunRot.eulerAngles;
            gunEul.y = 0.0f;
            gunRot.eulerAngles = gunEul;
            gun.localRotation = Quaternion.Slerp(gun.localRotation, gunRot, 6.9f * Time.deltaTime);

        }
    }
}
