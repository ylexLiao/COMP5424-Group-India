using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Area Damage Item", menuName = "Items/Area Damage Item")]

public class AreaDamageItem : Item
{
    public float radius = 3f;
    public int damage = 50;

    public void Use(Vector3 userPosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(userPosition, radius);
        foreach (var hitCollider in hitColliders)
        {
            EnemyHealth enemy = hitCollider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
