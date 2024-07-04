using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] shoot;
    public AudioClip[] jump;
    public AudioClip[] explosion;
    public AudioClip[] hit;
}
