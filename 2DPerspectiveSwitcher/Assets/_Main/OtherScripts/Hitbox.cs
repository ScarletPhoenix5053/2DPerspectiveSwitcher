using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour
{
    Collider Collider;

    protected virtual void CheckForHit()
    {
        Debug.Log("Checking");

        Collider2D[] others = new Collider2D[3];
        if (Collider.OverlapCollider(_contactFilter, others) != 0)
        {
            if (others != null)
            {
                foreach (Collider2D other in others)
                {
                    if (other != null)
                    {
                        Health targetHealth;
                        if (targetHealth = other.GetComponent<Health>())
                        {
                            ActionOnHit(targetHealth);
                        }
                    }
                }
            }
        }
    }
}