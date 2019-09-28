using System.Collections;
using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    Animator theAnimator;
    int count;

    // Use this for initialization
    void Start()
    {
        theAnimator = GetComponent<Animator>(); //get handle to the Animator
        theAnimator.SetFloat("Speed", 0);
        theAnimator.SetFloat("Direction", 0.5f);
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (count == 500)
            this.startWalking();
        else if (count == 1000)
            this.stopWalking();
    }

    public void startWalking()
    {
        Debug.Log("here");
        theAnimator.SetFloat("Speed", 1);
    }

    public void stopWalking()
    {
        Debug.Log("here2");
        theAnimator.SetFloat("Speed", 0);
    }
}
