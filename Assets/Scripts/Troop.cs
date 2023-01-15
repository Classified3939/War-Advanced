using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Troop : MonoBehaviour
{
    private String[] partNames = new String[] { "Part1", "Part2", "Part3", "Part4", "Part5" };
    private GameObject[] troopParts;
    private new UnityEngine.BoxCollider2D collider;
    private UnitSettingsVariable settings;
    private TerrainVariable terrainSettings;
    private bool isDefending;
    private Vector2 targetVector;
    [NonSerialized] public bool isInfantry;

    public void setSettings(UnitSettingsVariable settings, TerrainVariable terrainSettings, bool isDefending)
    {
        this.settings = settings;
        this.isInfantry = settings.value.isInfantry;
        this.terrainSettings = terrainSettings;
        this.isDefending = isDefending;
    }

    public void setTarget(Vector2 target)
    {
        this.targetVector = target;
    }

    // Start is called before the first frame update
    void Start()
    {
        troopParts = new GameObject[partNames.Length];
        GameObject[] tempArray = new GameObject[partNames.Length];
        for (int i = 0; i < partNames.Length; i++)
        {
            tempArray[i] = gameObject.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < tempArray.Length; i++)
        {
            troopParts[i] = Array.Find(tempArray, p => p.name == partNames[i]);
        }

        collider = gameObject.GetComponent<UnityEngine.BoxCollider2D>();

        StartCoroutine(test());
    }

    IEnumerator Fire(int partNumber, bool isPrimary)
    {
        int damage;
        if (isPrimary)
        {
            damage = settings.value.primary.damage;
        }
        else
        {
            damage = settings.value.secondary.damage;
        }

        if (isPrimary)
        {
            for (int i = 0; i < settings.value.primary.shots; i++)
            {
                ShootBullet(partNumber, damage, true);
                yield return new WaitForSecondsRealtime(0.225f);
            }
        }
        else
        {
            for (int i = 0; i < settings.value.secondary.shots; i++)
            {
                ShootBullet(partNumber, damage, false);
                yield return new WaitForSecondsRealtime(0.225f);
            }
        }
    }

    void ShootBullet(int index, int damage, bool isPrimary)
    {
        if (troopParts[index].activeSelf)
        {
            int bulletX = 0;
            if (targetVector.x < 0)
            {
                bulletX = 2;
            }
            else
            {
                bulletX = -2;
            }
            GameObject bullet = Instantiate(getBulletType(isPrimary), new Vector2(bulletX, troopParts[index].transform.position.y), Quaternion.Euler(0, 0, 90));

            bool isPiercing;
            bool isExplosive;
            if (isPrimary)
            {
                isPiercing = settings.value.primary.isPiercing;
                isExplosive = settings.value.primary.isExplosive;
            }
            else
            {
                isPiercing = settings.value.secondary.isPiercing;
                isExplosive = settings.value.secondary.isExplosive;
            }

            bullet.GetComponent<MoveBullet>().setAttributes(targetVector, damage, isPiercing, isExplosive);
        }
    }

    GameObject getBulletType(bool isPrimary)
    {
        if (isPrimary)
        {
            if (settings.value.primary.isPiercing && settings.value.primary.isExplosive)
            {
                return Resources.Load<GameObject>("Prefabs/BulletMega");
            }
            else if (settings.value.primary.isPiercing)
            {
                return Resources.Load<GameObject>("Prefabs/BulletPiercing");
            }
            else if (settings.value.primary.isExplosive)
            {
                return Resources.Load<GameObject>("Prefabs/BulletExplosive");
            }
            else
            {
                return Resources.Load<GameObject>("Prefabs/Bullet");
            }
        }
        else
        {
            if (settings.value.secondary.isPiercing && settings.value.secondary.isExplosive)
            {
                return Resources.Load<GameObject>("Prefabs/BulletMega");
            }
            else if (settings.value.secondary.isPiercing)
            {
                return Resources.Load<GameObject>("Prefabs/BulletPiercing");
            }
            else if (settings.value.secondary.isExplosive)
            {
                return Resources.Load<GameObject>("Prefabs/BulletExplosive");
            }
            else
            {
                return Resources.Load<GameObject>("Prefabs/Bullet");
            }
        }
    }

    private void RemovePart()
    {
        for (int i = 0; i < troopParts.Length; i++)
        {
            if (troopParts[i].activeSelf)
            {
                troopParts[i].SetActive(false);
                return;
            }
        }
    }

    public void Hurt(int damage, bool isPiercing)
    {
        int armor = settings.value.armor;
        switch (terrainSettings.value)
        {
            case 1:
                if (isInfantry)
                {
                    armor += 1;
                }
                break;
            case 2:
                if (!isInfantry)
                {
                    armor += 1;
                }
                break;
            case 3:
                if (isDefending)
                {
                    armor += 1;
                }
                break;
        }
        if (isPiercing)
        {
            armor -= 2;
        }
        if (damage > armor)
        {
            RemovePart();
        }
    }

    IEnumerator test()
    {
        for (int i = 0; i < 9; i++)
        {
            bool didFire = false;
            if (i == settings.value.primary.initiative)
            {
                if (settings.value.isInfantry)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        StartCoroutine(Fire(j, true));
                        didFire = true;
                    }
                }
                else
                {
                    StartCoroutine(Fire(3, true));
                    didFire = true;
                }
            }
            if (i == settings.value.secondary.initiative)
            {
                StartCoroutine(Fire(4, false));
                didFire = true;
            }
            if (didFire)
            {
                yield return new WaitForSecondsRealtime(3);
            }
            else
            {
                yield return new WaitForSecondsRealtime(.2f);
            }

        }
    }
}
