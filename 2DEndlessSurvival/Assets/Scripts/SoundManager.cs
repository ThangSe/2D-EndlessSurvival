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
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }
    private void Start()
    {
        Player.Instance.CastingAttackingAction += Player_CastingAttackingAction;
        Player.Instance.RangeAttackAction += Player_RangeAttackAction;
        Player.Instance.StrongAttackingAction += Player_StrongAttackingAction;
        PlayerAnimations.Instance.OnAttackDuelDamage += PlayerAnimations_OnAttackDuelDamage;
        EnemyAI.OnAnyMelleAttack += EnemyAI_OnAnyMelleAttack;
        EnemyAI.OnAnyShootArrow += EnemyAI_OnAnyShootArrow;
        EnemyAI.OnAnyDrawBow += EnemyAI_OnAnyDrawBow;
        EnemyAI.OnAnyMove += EnemyAI_OnAnyMove;
        Enemy.OnAnyBlockHit += Enemy_OnAnyBlockHit;
        SkeletonArrow.OnAnyArrowHit += SkeletonArrow_OnAnyArrowHit;
    }

    private void SkeletonArrow_OnAnyArrowHit(object sender, System.EventArgs e)
    {
        SkeletonArrow skeletonArrow = sender as SkeletonArrow;
        PlaySound(audioClipRefsSO.arrowHit, skeletonArrow.transform.position);
    }

    private void Enemy_OnAnyBlockHit(object sender, System.EventArgs e)
    {
        Enemy enemy = sender as Enemy;
        int randomSound = Random.Range(0, audioClipRefsSO.skeletonBlock.Length);
        PlaySound(audioClipRefsSO.skeletonBlock, enemy.transform.position, randomSound);
    }
    private void EnemyAI_OnAnyMove(object sender, EnemyAI.OnAnyMoveEventArgs e)
    {
        EnemyAI enemy = sender as EnemyAI;
        PlaySound(audioClipRefsSO.skeletonWalking, enemy.transform.position, e.walkingnum);
    }
    private void EnemyAI_OnAnyDrawBow(object sender, System.EventArgs e)
    {
        EnemyAI enemy = sender as EnemyAI;
        PlaySound(audioClipRefsSO.skeletonRangeAttack, enemy.transform.position);
    }
    private void EnemyAI_OnAnyShootArrow(object sender, System.EventArgs e)
    {
        EnemyAI enemy = sender as EnemyAI;
        int arrowShot = 1;
        PlaySound(audioClipRefsSO.skeletonRangeAttack, enemy.transform.position, arrowShot);
    }

    private void EnemyAI_OnAnyMelleAttack(object sender, System.EventArgs e)
    {
        EnemyAI enemy = sender as EnemyAI;
        PlaySound(audioClipRefsSO.skeletonMelleAttack, enemy.transform.position);
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

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayWalkingSound(Vector3 position, int num, float volume)
    {
        PlaySound(audioClipRefsSO.walking, position, num, volume);
    }

    public void PlayRunningSound(Vector3 position, int num, float volume)
    {
        PlaySound(audioClipRefsSO.running, position, num, volume);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1.05f)
        {
            volume = 0;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
