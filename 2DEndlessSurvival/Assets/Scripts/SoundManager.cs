using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Player.Instance.CastingAttackingAction += Player_CastingAttackingAction;
        Player.Instance.RangeAttackAction += Player_RangeAttackAction;
        Player.Instance.StrongAttackingAction += Player_StrongAttackingAction;
        PlayerAnimations.Instance.OnAttackDuelDamage += PlayerAnimations_OnAttackDuelDamage;
    }

    private void PlayerAnimations_OnAttackDuelDamage(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.meleeAttack, Player.Instance.transform.position);
    }

    private void Player_StrongAttackingAction(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.meleeAttack, Player.Instance.transform.position);
    }

    private void Player_RangeAttackAction(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.rangeAttack, Player.Instance.transform.position);
    }

    private void Player_CastingAttackingAction(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.spellCasting, Player.Instance.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, int num = 0, float volume = 1f)
    {

        PlaySound(audioClipArray[num], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayWalkingSound(Vector3 position, int num, float volume)
    {
        PlaySound(audioClipRefsSO.walking, position, num, volume);
    }

    public void PlayRunningSound(Vector3 position, int num, float volume)
    {
        PlaySound(audioClipRefsSO.running, position, num, volume);
    }

    public void PlayArrowHitSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.arrowHit, position);
    }

    public void PlaySkeletonMeeleAttack(Vector3 position)
    {
        PlaySound(audioClipRefsSO.skeletonMelleAttack, position);
    }

    public void PlaySkeletonDrawBow(Vector3 position)
    {
        PlaySound(audioClipRefsSO.skeletonRangeAttack, position);
    }

    public void PlaySkeletonShotArrow(Vector3 position)
    {
        int arrowShot = 1;
        PlaySound(audioClipRefsSO.skeletonRangeAttack, position, arrowShot);
    }

    public void PlaySkeletonBlock(Vector3 position)
    {
        int randomSound = Random.Range(0, audioClipRefsSO.skeletonBlock.Length);
        PlaySound(audioClipRefsSO.skeletonBlock, position, randomSound);
    }

    public void PlayeSkeletonWalking(Vector3 position, int num)
    {
        float volume = .3f;
        PlaySound(audioClipRefsSO.skeletonWalking, position, num, volume);
    }
}
