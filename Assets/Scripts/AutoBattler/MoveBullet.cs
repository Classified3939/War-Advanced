/*Copyright (c) 2023, Classified39
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Vector2 targetVector;
    private int damage;
    private bool isPiercing;
    private bool isExplosive;
    private bool isIncendiary;
    private int turnNumber;

    public void setAttributes(Vector2 target, int damage, bool isPiercing, bool isExplosive, bool isIncendiary, int turnNumber)
    {
        this.targetVector = target;
        this.damage = damage;
        this.isPiercing = isPiercing;
        this.isExplosive = isExplosive;
        this.turnNumber = turnNumber;
        this.isIncendiary = isIncendiary;
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
            if (isExplosive && collision.gameObject.GetComponent<Troop>().isInfantry)
            {
                for (int i = 0; i < 5; i++)
                {
                    collision.gameObject.GetComponent<Troop>().Hurt(damage, isPiercing, isIncendiary, turnNumber);
                }
            }
            else
            {
                collision.gameObject.GetComponent<Troop>().Hurt(damage, isPiercing, isIncendiary, turnNumber);
            }

            Destroy(gameObject);
        }

    }
}
