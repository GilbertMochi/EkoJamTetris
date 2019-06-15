using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("CoolSound");
    }
}
