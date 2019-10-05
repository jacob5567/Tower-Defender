using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject TurretPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
            buildTurret();
    }

    public void buildTurret()
    {
        GameObject turret;
        Transform closestPedestal = findClosestPedestal();
        turret = (GameObject)Instantiate(TurretPrefab, closestPedestal.position, transform.rotation);
        turret.transform.parent = GameObject.Find("Turrets").transform;
        turret.GetComponent<TurretScript>().SetPlayer(this.gameObject);
        turret.GetComponent<TurretScript>().SetEnemyGroupCenter(GameObject.Find("EnemyGroupCenter"));
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
