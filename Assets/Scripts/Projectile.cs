using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Collider target;
    private EnemyInfo enemyInfo;
    private TreeInfo treeInfo;
    private float step;
    public float speed;
    public float damage;

    public Type type;
    public enum Type
    {
        Projectile,
        FirePower,

    }

    void Start()
    {
        step = speed * Time.deltaTime;
    }

    void Update()
    {
        if (target != null)
        {
            enemyInfo = target.gameObject.GetComponent<EnemyInfo>();
            // If target is an Enemy
            if (enemyInfo != null)
            {
                if (enemyInfo.currentHealth > 0)
                {
                    Vector3 hitPos;
                    if (enemyInfo.name == "BigBoy")
                    {
                        hitPos = target.transform.position + new Vector3(0, 8f, 0);
                    }
                    else
                    {
                        hitPos = target.transform.position + new Vector3(0, 1f, 0);
                    }
                    transform.LookAt(hitPos);

                    this.transform.position = Vector3.MoveTowards(this.transform.position, hitPos, step);
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
            // If target is explosive
            else if (target.tag == "Explosive")
            {
                Vector3 hitPos;
                hitPos = target.transform.position + new Vector3(0, 0, 0);
                transform.LookAt(hitPos);

                this.transform.position = Vector3.MoveTowards(this.transform.position, hitPos, step);
            }
            else if (target.tag == "DeadTree")
            {
                treeInfo = target.gameObject.GetComponent<TreeInfo>();
                if (treeInfo != null)
                {
                    if (treeInfo.currentHealth > 0)
                    {
                        Vector3 hitPos;
                        hitPos = target.transform.position + new Vector3(0, 5f, 0);
                        transform.LookAt(hitPos);

                        this.transform.position = Vector3.MoveTowards(this.transform.position, hitPos, step);
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
            else if (target.tag == "Hero")
            {
                Vector3 hitPos;
                hitPos = target.transform.position;
                transform.LookAt(hitPos);
                this.transform.position = Vector3.MoveTowards(this.transform.position, hitPos, step);

            }

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

        if (other.tag == "Explosive")
        {
            if (other.transform.parent.gameObject.GetComponent<Explosive>() != null)
            {
                Debug.Log("Explosive hit");
                other.transform.parent.gameObject.GetComponent<Explosive>().Explode();
                Destroy(this.gameObject);
            }
        }
        if (type == Type.FirePower)
        {
            if (other.tag == "Hero")
            {
                Debug.Log("Hero got snacked some fire");
                Destroy(this.gameObject);
            }
        }
    }
}
