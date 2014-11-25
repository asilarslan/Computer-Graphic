using UnityEngine;
using System.Collections;

public class BresenhamLines : MonoBehaviour
{
    // Bresenham's line algorithm - converted to unity - Asil Arslan - http://aarslan.com
    // original source: http://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm#Simplification
    // *attach this scrip to a plane, then draw with mouse: click startpoint, click endpoint

    private Texture2D texture;
    public int texture_width = 256;
    public int texture_height = 256;
    private bool point1 = false;
    private bool point2 = false;
    private Vector2 startpoint;
    private Vector2 endpoint;
    public int dx, dy, err, e2, sx, sy;
    public int randomStartx, randomStarty, randomEndx, randomEndy;

    void Start()
    {
        texture = new Texture2D(texture_width, texture_height);
        renderer.material.mainTexture = texture;

        drawLine(140, 75, 140, 140, new Color(0, 1, 0, 1));
        drawLine(140, 140, 75, 140, new Color(0, 1, 0, 1));
        drawLine(75, 140, 75, 75, new Color(0, 1, 0, 1));
        drawLine(75, 75, 140, 75, new Color(0, 1, 0, 1));
    }

    void Update()
    {
        // draw lines with mouse

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (!point1)
                {
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= texture.width;
                    pixelUV.y *= texture.height;
                    startpoint = new Vector2(pixelUV.x, pixelUV.y);
                    point1 = true;

                    print(startpoint);
                }
                else
                {
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= texture.width;
                    pixelUV.y *= texture.height;
                    endpoint = new Vector2(pixelUV.x, pixelUV.y);
                    point1 = false;
                    Debug.Log("start x = " + (int)startpoint.x + "start y = " + (int)startpoint.y + "end x = " + (int)endpoint.x + "end y = " + (int)endpoint.y);
                    drawLine((int)startpoint.x, (int)startpoint.y, (int)endpoint.x, (int)endpoint.y, new Color(1, 1, 1, 1));
                    //drawLine(180, 59, 106, 142);
                    print(endpoint);
                }
            }
        }
    }

    // from wikipedia Bresenham
    void drawLine(int x0, int y0, int x1, int y1, Color c)
    {
        dx = Mathf.Abs(x1 - x0);
        dy = Mathf.Abs(y1 - y0);
        if (x0 < x1) { sx = 1; } else { sx = -1; }
        if (y0 < y1) { sy = 1; } else { sy = -1; }
        err = dx - dy;


        while (true)
        {
            
            texture.SetPixel(x0, y0, c);
            if (x0>75 && x0 < 140 && y0>75 && y0 < 140)
            {
                texture.SetPixel(x0, y0, new Color(0, 0, 1, 1));
            }
            if ((x0 == x1) && (y0 == y1)) break;
            e2 = 2 * err;
            if (e2 > -dy)
            {
                err = err - dy;
                x0 = x0 + sx;
            }
            if (e2 < dx)
            {
                err = err + dx;
                y0 = y0 + sy;
            }
        }
        texture.Apply();
    }

    void OnGUI()
    {
        
        // Make a background box
        GUI.Box(new Rect(10, 10, 100, 90), "Menu");

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(20, 40, 80, 20), "Create Line"))
        {
            randomStartx = Random.Range(50, 180);
            randomStarty = Random.Range(50, 180);
            randomEndx = Random.Range(50, 180);
            randomEndy = Random.Range(50, 180);
            //Application.LoadLevel(1);
            Debug.Log("Clicked the button with an image");
            drawLine(randomStartx, randomStarty, randomEndx, randomEndy, new Color(1, 1, 1, 1));
        }

        // Make the second button.
        if (GUI.Button(new Rect(20, 70, 80, 20), "Clean Lines"))
        {
            //Application.LoadLevel(2);
            Debug.Log("Clicked the button with an image");
        }





    }



}