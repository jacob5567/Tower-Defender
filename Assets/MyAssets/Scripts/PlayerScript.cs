// Jacob Faulk
// This script controls many player actions, primarily building turrets.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    /* TURRET PREFABS */
    public GameObject Turret1Prefab;
    public GameObject Turret3Prefab;
    public GameObject Turret4Prefab;
    public GameObject Turret6Prefab;

    public GameObject gun; // the player's gun
    public GameObject selectTurretText; // the text showing the turret numbers and the prices to build those turrets
    public GameObject confirmCancelIndicators; // small indicators showing the player how to place or remove a turret
    public GameObject selectTurretNum; // a small indicator over the crosshair showing which turret is selected to be built
    public Camera mainCam; // the main camera
    public int modeState; // 0=gun mode; 1=turret select mode; 2=turret placement mode
    public int selectedTurret; // the type of turret selected (1, 2, 3, 4)
    public int money; // the amount of money the player has for building turrets
    public int level; // the current level of the game

    // sets money to zero, level to one, and sets the placement indicator to not render, among other things
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

    // Updates the level and creates turrets if the player has enough money.
    // The different states control whether the player is selecting a turret type, choosing where to place a turret, or shooting the gun
    void Update()
    {
        GameObject.Find("CoinCount").GetComponent<Text>().text = "$" + money.ToString();
        if (level <= 20)
        {
            GameObject.Find("LevelNum").GetComponent<Text>().text = "Level " + level.ToString();
        }
        else
        {
            GameObject.Find("LevelNum").GetComponent<Text>().text = "Endless";
        }
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
            if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
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

    // builds the specified type of turret in the indicated turret pedestal
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

    // finds the closest pedestal to a point in front of the player.
    // The pedastal must be in the player's view and close to a point close in front of them.
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
