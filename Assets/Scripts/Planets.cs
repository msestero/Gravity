using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planets : MonoBehaviour
{
    public int numPlanets;
    public float gravityConst;
    public float initialRandForce;
    GameObject[] planets;
    
    void Start()
    {
        planets = new GameObject[numPlanets];
        for (int i = 0; i < numPlanets; i++)
        {
            planets[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planets[i].transform.position = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f));
            float scale = Random.Range(1f, 10f);
            planets[i].transform.localScale = new Vector3(scale, scale, scale);
            planets[i].transform.parent = gameObject.transform;
            Rigidbody rb = planets[i].AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddForce(new Vector3(Random.Range(-initialRandForce, initialRandForce), Random.Range(-initialRandForce, initialRandForce), Random.Range(-initialRandForce, initialRandForce)));
        }
    }

    void Update()
    {
        for (int i = 0; i < numPlanets; i++)
        {
            Rigidbody rb = planets[i].GetComponent<Rigidbody>();
            for (int j = 0; j < numPlanets; j++)
            {
                if (j != i)
                {
                    Vector3 planetIV = planets[i].transform.position;
                    Vector3 planetJV = planets[j].transform.position;
                    float xSqrDist = (planetIV.x - planetJV.x) * (planetIV.x - planetJV.x);
                    float ySqrDist = (planetIV.y - planetJV.y) * (planetIV.y - planetJV.y);
                    float zSqrDist = (planetIV.z - planetJV.z) * (planetIV.z - planetJV.z);
                    float dist = Mathf.Sqrt(xSqrDist + ySqrDist + zSqrDist);
                    float m1 = (4f / 3f) * Mathf.PI * planets[i].transform.localScale.x;
                    float m2 = (4f / 3f) * Mathf.PI * planets[j].transform.localScale.x;
                    float force = gravityConst * ((m1 * m2) / dist);
                    float xForce = -force * (planetIV.x - planetJV.x) / dist;
                    float yForce = -force * (planetIV.y - planetJV.y) / dist;
                    float zForce = -force * (planetIV.z - planetJV.z) / dist;
                    rb.AddForce(new Vector3(xForce, yForce, zForce));
                }
            }
        }
    }
}
