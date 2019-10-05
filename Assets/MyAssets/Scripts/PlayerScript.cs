using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public GameObject Turret1Prefab;
    public GameObject Turret3Prefab;
    public GameObject Turret4Prefab;
    public GameObject Turret6Prefab;
    public GameObject gun;
    public GameObject selectTurretText;
    public GameObject confirmCancelIndicators;
    public GameObject selectTurretNum;
    public Camera mainCam;
    public int modeState; // 0=gunmode; 1=turretselect; 2=placement
    public int selectedTurret;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        gun = GameObject.Find("Gun");
        modeState = 0;
        selectedTurret = 0;
        GameObject.Find("PlacementIndicator").GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (modeState == 0)
        {
            GameObject.Find("PlacementIndicator").GetComponent<Renderer>().enabled = false;
            gun.SetActive(true);
            selectTurretText.SetActive(false);
            confirmCancelIndicators.SetActive(false);
            if (Input.GetKeyUp(KeyCode.E))
            {
                modeState = 1;
            }
        }
        else if (modeState == 1)
        {
            selectTurretText.SetActive(true);
            GameObject.Find("PlacementIndicator").GetComponent<Renderer>().enabled = false;
            gun.SetActive(false);
            confirmCancelIndicators.SetActive(false);
            if (Input.GetKeyUp(KeyCode.E))
            {
                modeState = 0;
            }
            // TODO show graphic
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                selectedTurret = 1;
                modeState = 2;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                selectedTurret = 2;
                modeState = 2;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                selectedTurret = 3;
                modeState = 2;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                selectedTurret = 4;
                modeState = 2;
            }
        }
        else if (modeState == 2)
        {
            confirmCancelIndicators.SetActive(true);
            selectTurretNum.GetComponent<Text>().text = selectedTurret.ToString();
            selectTurretText.SetActive(false);
            gun.SetActive(false);
            GameObject.Find("PlacementIndicator").GetComponent<Renderer>().enabled = true;
            Transform closestPedestal = findClosestPedestal();
            if (closestPedestal != null)
                GameObject.Find("PlacementIndicator").transform.position = closestPedestal.position;
            if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(1))
            {
                modeState = 0;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                buildTurret(selectedTurret);
                selectedTurret = 0;
                modeState = 0;
            }
        }
    }

    public void buildTurret(int num)
    {
        GameObject turret;
        Transform closestPedestal = findClosestPedestal();
        closestPedestal.gameObject.GetComponent<PedestalScript>().fill();
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
            currentDistance = Vector3.Distance(p.transform.position, mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 4.0f)));//transform.position);
            if (currentDistance < minimumDistance && !p.gameObject.GetComponent<PedestalScript>().isFilled() && p.gameObject.GetComponent<PedestalScript>().isInFrame())
            {
                minimumDistance = currentDistance;
                closest = p;
            }
        }
        return closest;
    }
}
