using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuHueChange : MonoBehaviour
{
    public Light ColorLight;
    public GameObject MenuCanvas;
    public GameObject SelectionCubes;

    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ColorLight.color = ShiftHue(ColorLight.color);

        foreach (TextMeshProUGUI tmp in MenuCanvas.GetComponentsInChildren<TextMeshProUGUI>())
        {
            tmp.color = ShiftHue(tmp.color);
        }
        foreach (Button btn in MenuCanvas.GetComponentsInChildren<Button>())
        {
            ColorBlock cb = btn.colors;
            cb.highlightedColor = ShiftHue(cb.highlightedColor);
            cb.selectedColor = ShiftHue(cb.selectedColor);
            cb.pressedColor = ShiftHue(cb.pressedColor);
            btn.colors = cb;
        }
        foreach (MeshRenderer mr in SelectionCubes.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material.color = ShiftHue(mr.material.color);
        }
    }

    private Color ShiftHue(Color color)
    {
        Color.RGBToHSV(color, out float h, out float s, out float v);
        h += 0.1f * speed * Time.deltaTime;
        if (h > 1f) h -= 1f;
        return Color.HSVToRGB(h, s, v);
    }
}
