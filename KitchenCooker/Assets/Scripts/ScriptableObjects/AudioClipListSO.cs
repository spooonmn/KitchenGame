using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AudioClip List", menuName = "Audio Clip List")]
public class AudioClipListSO : ScriptableObject
{
    public AudioClip[] ChopAudioList;
    public AudioClip[] DeliveryFailed;
    public AudioClip[] DeliverySuccess;
    public AudioClip[] FootStep1AudioList;
    public AudioClip[] FootStep2AudioList;
    public AudioClip[] DropAudioList;
    public AudioClip[] PickUpAudioList;
    public AudioClip[] PanSizzleAudioList;
    public AudioClip[] TrashAudioList;
    public AudioClip[] WarningAudioList;
}
