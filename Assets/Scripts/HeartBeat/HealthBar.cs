using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject LinePrefab;
    Transform target;
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
    void Update()
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
        if(target.position.x>endPos.position.x)
        {
            timer = 0f;
            isStraight = true;
            //target.position = startPosition;
        }
    }
}
