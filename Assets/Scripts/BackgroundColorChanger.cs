using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    public float every;   //The public variable "every" refers to "Lerp the color every X"
    float colorstep;
    Color[] colors = new Color[10]; //Insert how many colors you want to lerp between here, hard coded to 4
    Color lerpedColor = new Color32(156, 102, 123, 0);  //This should optimally be the color you are going to begin with
    int i;
    private bool _colorEnded = false;


    void Start()
    {
        //In here, set the array colors you are going to use, optimally, repeat the first color in the end to keep transitions smooth

        colors[0] = new Color32(142, 93, 112, 0);
        colors[1] = new Color32(129, 84, 101, 0);
        colors[2] = new Color32(115, 75, 91, 0);
        colors[3] = new Color32(102, 66, 80, 0);
        colors[4] = new Color32(89, 58, 70, 0);
        colors[5] = new Color32(75, 49, 59, 0);
        colors[6] = new Color32(62, 40, 48, 0);
        colors[7] = new Color32(48, 31, 38, 0);
        colors[8] = new Color32(35, 22, 27, 0);
        colors[9] = new Color32(22, 14, 17, 0);
    }

    void Update()
    {
        {
            if (colorstep < every && !_colorEnded && !_colorEnded)
            { //As long as the step is less than "every"
                lerpedColor = Color.Lerp(colors[i], colors[i + 1], colorstep);
                this.GetComponent<Camera>().backgroundColor = lerpedColor;
                colorstep += 0.025f;  //The lower this is, the smoother the transition, set it yourself

            }
            else
            { //Once the step equals the time we want to wait for the color, increment to lerp to the next color

                colorstep = 0;

                if (i < (colors.Length - 2))
                { //Keep incrementing until i + 1 equals the Lengh
                    i++;
                }
                else
                { //and then reset to zero
                    _colorEnded = true;
                }
            }
        }
    }
}
