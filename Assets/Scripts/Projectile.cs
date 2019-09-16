using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Collider target;
    private float step;
    public float speed;
    public float damage;

    void Awake()
    {
        step = speed * Time.deltaTime;
    }

    void Update()
    {
        if(target != null)
        {
            transform.LookAt(target.transform.position);

            this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "DeadTree")
        {
            EnemyInfo enemy = other.gameObject.GetComponent<EnemyInfo>();
            TreeInfo tree = other.gameObject.GetComponent<TreeInfo>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            else if (tree != null)
            {
                tree.TakeDamage(damage);
            }
            Debug.Log("Projectile hit " + other.gameObject.name + " for " + damage + " Damage");
            Destroy(this.gameObject);

        }
    }
}
