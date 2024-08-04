using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }   

    [SerializeField] private AudioClipListSO audioClipList;

    private float volume = 1f;


    private void Awake()
    {
        
        Instance = this;
       
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSucess += DeliveryManager_OnRecipeSucess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        BinCounter.OnAnyObjectTrashed += BinCounter_OnAnyObjectTrashed;


    }

    private void BinCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        PlaySound(audioClipList.TrashAudioList, ((BinCounter)sender).transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, EventArgs e)
    {
        PlaySound(audioClipList.DropAudioList, ((BaseCounter)sender).transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipList.PickUpAudioList, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySound(audioClipList.ChopAudioList, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipList.DeliveryFailed, deliveryCounter.transform.position);
        
    }

    private void DeliveryManager_OnRecipeSucess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipList.DeliverySuccess, deliveryCounter.transform.position);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);

    }
    private void PlaySound(AudioClip[] audioClipArrey, Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipArrey[UnityEngine.Random.Range(0,audioClipArrey.Length)], position, volumeMultiplier * volume);
    }

    public void PlayFootStepSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipList.FootStep1AudioList, position,volumeMultiplier);
    }

    public void ChangeVolume()
    {
        volume += .1f;

        if (volume > 1f)
        {
            volume = 0f;
        }
    }

    public float GetVolume()
    {
        return volume;
    }
}
