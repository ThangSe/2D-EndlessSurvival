using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] spellCasting;
    public AudioClip[] rangeAttack;
    public AudioClip[] meleeAttack;
    public AudioClip[] walking;
    public AudioClip[] running;
    public AudioClip[] skeletonWalking;
    public AudioClip[] skeletonMelleAttack;
    public AudioClip[] skeletonRangeAttack;
    public AudioClip[] skeletonBlock;
    public AudioClip[] arrowHit;
}
