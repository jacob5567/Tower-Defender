using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int money;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        level = 1;
        mainCam = Camera.main;
        gun = GameObject.Find("Gun");
        modeState = 0;
        selectedTurret = 0;
        GameObject.Find("PlacementIndicator").GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        GameObject.Find("CoinCount").GetComponent<Text>().text = "$" + money.ToString();
        GameObject.Find("LevelNum").GetComponent<Text>().text = "Level " + level.ToString();
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
            if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(1))
            {
                modeState = 0;
            }
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                if (money >= 100)
                {
                    selectedTurret = 1;
                    modeState = 2;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                if (money >= 300)
                {
                    selectedTurret = 2;
                    modeState = 2;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                if (money >= 400)
                {
                    selectedTurret = 3;
                    modeState = 2;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                if (money >= 600)
                {
                    selectedTurret = 4;
                    modeState = 2;
                }
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
                switch (selectedTurret)
                {
                    case 1:
                        money -= 100;
                        break;
                    case 2:
                        money -= 300;
                        break;
                    case 3:
                        money -= 400;
                        break;
                    case 4:
                        money -= 600;
                        break;
                    default:
                        break;
                }
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
