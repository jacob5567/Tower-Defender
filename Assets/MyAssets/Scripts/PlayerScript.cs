using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject Turret1Prefab;
    public GameObject Turret3Prefab;
    public GameObject Turret4Prefab;
    public GameObject Turret6Prefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            buildTurret(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            buildTurret(2);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            buildTurret(3);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            buildTurret(4);
        }
    }

    public void buildTurret(int num)
    {
        GameObject turret;
        Transform closestPedestal = findClosestPedestal();
        GameObject turretPrefab = null;
        switch (num)
        {
            case 1:
                turretPrefab = Turret1Prefab;
                break;
            case 2:
                turretPrefab = Turret3Prefab;
                break;
            case 3:
                turretPrefab = Turret4Prefab;
                break;
            case 4:
                turretPrefab = Turret6Prefab;
                break;
            default:
                Debug.Log("ERROR: NOT A VALID TURRET TYPE");
                break;
        }
        turret = (GameObject)Instantiate(turretPrefab, closestPedestal.position, transform.rotation);
        turret.transform.parent = GameObject.Find("Turrets").transform;
        if (num != 4)
        {
            turret.GetComponent<TurretScript>().SetPlayer(this.gameObject);
            turret.GetComponent<TurretScript>().SetEnemyGroupCenter(GameObject.Find("EnemyGroupCenter"));
        }
        else
        {
            turret.GetComponent<MultiHitTurretScript>().SetPlayer(this.gameObject);
            turret.GetComponent<MultiHitTurretScript>().SetEnemyGroupCenter(GameObject.Find("EnemyGroupCenter"));
        }
    }

    private Transform findClosestPedestal()
    {
        GameObject pedestals = GameObject.Find("Pedestals");
        float minimumDistance = float.PositiveInfinity;
        Transform closest = null;
        float currentDistance = 0;
        foreach (Transform p in pedestals.transform)
        {
            currentDistance = Vector3.Distance(p.transform.position, transform.position);
            if (currentDistance < minimumDistance)
            {
                minimumDistance = currentDistance;
                closest = p;
            }
        }
        return closest;
    }
}
