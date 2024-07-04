using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More then one sound manager Instance");
        }
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);

        
    }

    private void Start()
    {
        Bullet.OnAnyHit += Bullet_OnAnyHit;
        ExplousivBarrale.OnAnyExplosion += ExplousivBarrale_OnAnyExplosion;
        Player.OnJump += Player_OnJump;
        Rifle.OnAnyShoot += Rifle_OnAnyShoot;
    }

    private void Rifle_OnAnyShoot(object sender, System.EventArgs e)
    {
        Rifle rifle = sender as Rifle;

        PlaySounds(audioClipRefsSO.shoot, rifle.transform.position);
    }

    private void Player_OnJump(object sender, System.EventArgs e)
    {
        PlaySounds(audioClipRefsSO.jump, Player.Instance.transform.position);
    }

    private void ExplousivBarrale_OnAnyExplosion(object sender, System.EventArgs e)
    {
        ExplousivBarrale barrale = sender as ExplousivBarrale;

        PlaySounds(audioClipRefsSO.explosion, barrale.transform.position);
    }

    private void Bullet_OnAnyHit(object sender, System.EventArgs e)
    {
        Bullet bullet = sender as Bullet;

        PlaySounds(audioClipRefsSO.hit, bullet.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    private void PlaySounds(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
}
