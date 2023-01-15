using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Vector2 targetVector;
    private int damage;
    private bool isPiercing;
    private bool isExplosive;

    public void setAttributes(Vector2 target, int damage, bool isPiercing, bool isExplosive)
    {
        this.targetVector = target;
        this.damage = damage;
        this.isPiercing = isPiercing;
        this.isExplosive = isExplosive;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(transform.position, targetVector, 6f * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.gameObject.name;
        if (name == "InfantryUnitBlue(Clone)" || name == "InfantryUnitRed(Clone)" || name == "VehicleUnitBlue(Clone)" || name == "VehicleUnitRed(Clone)")
        {
            if (isExplosive && collision.gameObject.GetComponent<Troop>().isInfantry){
                for (int i = 0; i < 5; i++){
                    collision.gameObject.GetComponent<Troop>().Hurt(damage, isPiercing);
                }
            }
            else{
                collision.gameObject.GetComponent<Troop>().Hurt(damage, isPiercing);
            }

            Destroy(gameObject);
        }

    }
}