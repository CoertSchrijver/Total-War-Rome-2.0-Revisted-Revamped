using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveArmies : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    private float moveSpeedx, moveSpeedz, relX, relZ, x, z;
    public GameObject army;

	// Use this for initialization
	void Start () {
        x = transform.position.x;
        z = transform.position.z;
	}

    void OnTriggerEnter(Collider collision) 
        // When an other prefab army is hit they collide, based on wether it is an enemy or ally it responds accordingly.
    {
        if (collision.gameObject.CompareTag("Army") && collision.GetComponent<ArmyStats>().spawnNumber < gameObject.GetComponent<ArmyStats>().spawnNumber)
        {          
            gameObject.GetComponent<ArmyStats>().armySize += collision.transform.GetComponent<ArmyStats>().armySize;
            gameObject.GetComponent<ArmyStats>().isSelected = true;

            if (collision.GetComponent<ArmyStats>().isSelected) // If the one being destroyed is selected the one that he collides with takes his movespeed and location
            {
                moveSpeedx = collision.GetComponent<MoveArmies>().moveSpeedx; // If the one destroyed 
                moveSpeedz = collision.GetComponent<MoveArmies>().moveSpeedz;

                StopCoroutine("MoveArmyRoutine");
                StartCoroutine("MoveArmyRoutine", new Vector3(collision.GetComponent<MoveArmies>().x, 0f, collision.GetComponent<MoveArmies>().z));
            }           
            Destroy(collision.gameObject);
        }        
    }
	
	// Update is called once per frame
	void Update ()
    {       
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (transform.GetComponent<ArmyStats>().isSelected) // Move the army only if it is selected
                {
                    x = hit.point.x;
                    z = hit.point.z;
                    CalculateMovespeed();
                    StopCoroutine("MoveArmyRoutine");
                    StartCoroutine("MoveArmyRoutine", new Vector3(x, 0f, z));
                }
            }
        }    
    }

    IEnumerator MoveArmyRoutine(Vector3 target)
    {
        while (transform.position.z > target.z + 0.2 || transform.position.z < target.z - 0.2 || transform.position.x > target.x + 0.2 || transform.position.x < target.x - 0.2)
        {
           // transform.position = new Vector3(transform.position.x + moveSpeedx, 0.2f, transform.position.z + moveSpeedz);
            transform.Translate(new Vector3(moveSpeedx * Time.deltaTime, 0f, moveSpeedz * Time.deltaTime));
            yield return new WaitForSeconds(0.02f);
        }
    }

    void CalculateMovespeed() // Moves the army to the mouseclick
    {
        // Relative position of mouseclick to the object
        if (transform.position.z > z)
            relZ = -Mathf.Abs(transform.position.z - z);
        else
            relZ = Mathf.Abs(transform.position.z - z);
        if (transform.position.x > x)
            relX = -Mathf.Abs(transform.position.x - x);
        else
            relX = Mathf.Abs(transform.position.x - x);

        if (Mathf.Abs(relX) > Mathf.Abs(relZ)) // Gets a ratio of the z and x so that the army moves directly to the mouseclick
        {
            moveSpeedx = (relX / (Mathf.Abs(relX) + Mathf.Abs(relZ))) * transform.GetComponent<ArmyStats>().armySpeed;
            if (relZ > 0)
            {
                moveSpeedz = Mathf.Abs((relZ / (Mathf.Abs(relX) + Mathf.Abs(relZ))) * transform.GetComponent<ArmyStats>().armySpeed);
            }
            else
            {
                moveSpeedz = -Mathf.Abs((relZ / (Mathf.Abs(relX) + Mathf.Abs(relZ))) * transform.GetComponent<ArmyStats>().armySpeed);
            }        
        }

        else if (Mathf.Abs(relX) < Mathf.Abs(relZ)) // Gets a ratio of the z and x so that the army moves directly to the mouseclick
        {
            moveSpeedz = (relZ / (Mathf.Abs(relX) + Mathf.Abs(relZ))) * transform.GetComponent<ArmyStats>().armySpeed;
            if (relX > 0)
            {
                moveSpeedx = Mathf.Abs((relX / (Mathf.Abs(relX) + Mathf.Abs(relZ))) * transform.GetComponent<ArmyStats>().armySpeed);
            }
            else
            {
                moveSpeedx = -Mathf.Abs((relX / (Mathf.Abs(relX) + Mathf.Abs(relZ))) * transform.GetComponent<ArmyStats>().armySpeed);
            }
        }
    }
}
