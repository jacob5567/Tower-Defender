using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public Transform shootElement;
	public Transform LookAtObj;
	public int dmg = 10;
	public GameObject bullet;
	public Transform target;
    public float shootDelay;
	bool isShoot;
	void Start () {
	
	}
	
	void Update () {
	if(target)
	{
            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            Debug.Log("XXX" + target.transform.position.x);
            Debug.Log("YYY" + transform.position.y);
            Debug.Log("ZZZ" + target.transform.position.z);

            LookAtObj.transform.LookAt(targetPosition);

            //LookAtObj.transform.LookAt(targetPosition);
            if (!isShoot)
	{
		StartCoroutine(shoot());
	}
	}
	}

	IEnumerator shoot()
	{
		isShoot = true;
		yield return new WaitForSeconds(shootDelay);
		GameObject b = GameObject.Instantiate(bullet,shootElement.position,Quaternion.identity) as GameObject;
		b.GetComponent<bulletTower>().target = target;
        b.GetComponent<bulletTower>().twr = this;
        isShoot = false;
	}
}
