using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private void Start()
    {
        image.alphaHitTestMinimumThreshold = 0.5f;
    }
}
