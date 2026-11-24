using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Stats")]
    public int maxHealth = 2;
    public int currentHealth;
    public int echoShards = 0;
    public int totalShards = 9;
    public int bellsRung = 0;
    public int totalBells = 3;
    public Vector3 respawnPoint;
    private int savedShards = 0;
    private List<GameObject> unsavedShards = new List<GameObject>();

    [Header("HUD UI")]
    public Slider healthBar;
    public TMP_Text shardsText;
    public TMP_Text bellsText;
    public TMP_Text checkpointText;
    [Header("Story & Ending Panels")]
    public GameObject storyPanel;
    public TMP_Text storyTitleText;
    public TMP_Text storyBodyText;
    public GameObject victoryPanel;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioClip jumpSFX;
    public AudioClip collectSFX;
    public AudioClip hurtSFX;
    public AudioClip bellChimeSFX;

    void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) respawnPoint = player.transform.position;

        if (storyPanel != null) storyPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (checkpointText != null) checkpointText.text = "";

        UpdateHUD();
        ShowStory("The Silent Valley",
            "Violet fog has put the Town to sleep. Collect Echo Shards and ring both Great Bells to awaken the village!");
    }

    public void AddShard(GameObject shard)
    {
        echoShards++;
        unsavedShards.Add(shard);
        PlaySFX(collectSFX);
        UpdateHUD();
    }

    public void TakeDamage(int amount, GameObject player)
    {
        currentHealth -= amount;
        PlaySFX(hurtSFX);
        UpdateHUD();

        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            player.transform.position = respawnPoint;
            echoShards = savedShards;
            foreach (GameObject shard in unsavedShards)
            {
                if (shard != null) shard.SetActive(true);
            }
            unsavedShards.Clear();

            UpdateHUD();
        }
    }

    public void RingBell(Vector3 newCheckpoint, string title, string message, bool isFinalBell)
    {
        bellsRung++;
        respawnPoint = newCheckpoint;
        savedShards = echoShards;
        unsavedShards.Clear();
        currentHealth = maxHealth; 
        PlaySFX(bellChimeSFX);
        UpdateHUD();

        if (checkpointText != null)
        {
            checkpointText.text = "Checkpoint Reached!";
            CancelInvoke(nameof(ClearCheckpointText));
            Invoke(nameof(ClearCheckpointText), 3f);
        }

        if (isFinalBell)
        {
            if (victoryPanel != null) victoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            ShowStory(title, message);
        }
    }

    public void ShowStory(string title, string message)
    {
        if (storyPanel == null) return;
        storyTitleText.text = title;
        storyBodyText.text = message;
        storyPanel.SetActive(true);
        Time.timeScale = 0f; 
    }
    public void CloseStory()
    {
        if (storyPanel != null) storyPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null) sfxSource.PlayOneShot(clip);
    }

    void ClearCheckpointText()
    {
        if (checkpointText != null) checkpointText.text = "";
    }

    void UpdateHUD()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        if (shardsText != null) shardsText.text = $"Echo Shards: {echoShards}/{totalShards}";
        if (bellsText != null) bellsText.text = $"Bells Rung: {bellsRung}/{totalBells}";
    }
}