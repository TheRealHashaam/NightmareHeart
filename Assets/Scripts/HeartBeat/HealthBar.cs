using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject LinePrefab;
    Transform target;
    public Transform Parent;
    public RectTransform startPosition;
    public RectTransform endPos;
    private TrailRenderer trailRenderer;
    public float straightDuration = 2f;
    public float pulseDuration = 1f;
    public float normalspeed = 2f;
    public float pulsespeed;
    public float amplitude = 1f;
    public float frequency = 5f;
    private float timer = 0f;
    private bool isStraight = true;
    public Color FullHealth;
    public Color MidHealth;
    public Color LowHealth;

    private void Start()
    {
        InstantiateLine();
    }
    public void InstantiateLine()
    {
        GameObject g = Instantiate(LinePrefab,startPosition.position,Quaternion.identity, Parent);
        target = g.transform;
        trailRenderer = target.GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if(target)
        {
            timer += Time.deltaTime;
            if (isStraight)
            {
                float xOffset = normalspeed * Time.deltaTime;
                Vector3 newPosition = target.position + new Vector3(xOffset, 0f, 0f);
                target.position = newPosition;
                if (timer >= straightDuration)
                {
                    timer = 0f;
                    isStraight = false;
                }
            }
            else
            {
                float xOffset = pulsespeed * Time.deltaTime;
                float yOffset = Mathf.Sin(frequency * timer * Mathf.PI * 2f) * amplitude * Time.deltaTime;

                Vector3 newPosition = target.position + new Vector3(xOffset, yOffset, 0f);

                target.position = newPosition;

                if (timer >= pulseDuration)
                {
                    timer = 0f;
                    isStraight = true;
                }
            }
        }
        if(target.position.x>endPos.position.x)
        {
            timer = 0f;
            isStraight = true;
            GameObject g = target.gameObject;
            Destroy(g,2f);
            InstantiateLine();
            //target.position = startPosition;
        }
    }

    public void HealthStateNormal()
    {
        trailRenderer.startColor = FullHealth;
        trailRenderer.endColor = FullHealth;
        pulseDuration = 1;
        straightDuration = 2;
        frequency = 1;
    }
    public void HealthStateCausious()
    {
        trailRenderer.startColor = MidHealth;
        trailRenderer.endColor = MidHealth;
        pulseDuration = 2;
        straightDuration = 1;
        frequency = 1;
        amplitude = 300;
    }
    public void HealthStateDanger()
    {
        trailRenderer.startColor = MidHealth;
        trailRenderer.endColor = MidHealth;
        pulseDuration = 9;
        straightDuration = 1;
        frequency = 1;
        amplitude = 300;
    }

    public void HealthStateDead()
    {
        trailRenderer.startColor = MidHealth;
        trailRenderer.endColor = MidHealth;
        pulseDuration = 0;
        straightDuration = 1;
        frequency = 1;
        amplitude = 0;
    }
}
