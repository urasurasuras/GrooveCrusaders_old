using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject owner;

    public float velX;
    public Rigidbody2D rb;
    public GameObject liteDmgParticle;
    public GameObject liteHealParticle;

    public double value_base=10;            //the starting value of the projectiles
    public double value_heal;               //respective values then multiplied
    public double value_damage;

    public double value_final;              //final value of whatever this projectile is doing

    // Start is called before the first frame update
    void Start()
    {
        value_final = owner.GetComponent<PlayerControl>().power * value_base;  //multiply by combo

        if (gameObject.tag == "f_damage")
        {
            value_final *= GameManager.Instance.mult_damage;
            print("Damage of " + this.name + ": " + value_final);
        }
        if (gameObject.tag == "f_healing")
        {
            value_final *= GameManager.Instance.mult_heal;
            print("Healing of " + this.name + ": " + value_final);
        }
        if (!owner.GetComponent<PlayerControl>().facingRight)
            velX *= -1;     //shoot projectile other way if the char is facinf that way
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * velX * Time.deltaTime;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && this.gameObject.tag == "f_damage")
        {
            Instantiate(liteDmgParticle,transform.position,Quaternion.identity);
            Destroy(gameObject, 0f);
        }
        if (collision.gameObject.tag == "Player" && this.gameObject.tag == "f_healing" && collision.gameObject != owner)
        {
            //print("proejectile: " + this.gameObject.tag + "hit: " + collision);
            Instantiate(liteHealParticle,transform.position, Quaternion.identity);
            Destroy(gameObject, 0f);
        }
    }
}
