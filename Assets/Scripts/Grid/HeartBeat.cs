using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBeat : MonoBehaviour
{
    public Color lineColor = Color.red;
    public int numberOfPoints = 100;
    public float frequency = 5f;
    public float amplitude = 50f;
    private float timer;

    void OnGUI()
    {
        // Apply the color for the line
        GUI.color = lineColor;

        // Create a temporary texture for drawing lines
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, lineColor);
        texture.Apply();

        Vector2 previousPoint = Vector2.zero;
        for (int i = 0; i < numberOfPoints; i++)
        {
            float x = (i / (float)numberOfPoints) * Screen.width;
            float y = Mathf.Sin(i * frequency + timer) * amplitude + (Screen.height / 2);
            Vector2 currentPoint = new Vector2(x, y);

            if (i > 0)
            {
                DrawLine(previousPoint, currentPoint, texture);
            }

            previousPoint = currentPoint;
        }

        timer += Time.deltaTime * frequency;
    }

    void DrawLine(Vector2 start, Vector2 end, Texture2D texture)
    {
        // Draw the line
        GUI.DrawTexture(MakeRect(start, end), texture);
    }

    Rect MakeRect(Vector2 start, Vector2 end)
    {
        // Generate a rect that fits the start and end points for the line
        // This might need adjustments for width and to ensure it aligns with your desired line thickness and orientation
        Vector2 direction = end - start;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return new Rect(start.x, start.y, direction.magnitude, 1f);
    }
}
