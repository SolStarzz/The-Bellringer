using UnityEngine;

public class BellTower : MonoBehaviour
{
    public int shardsRequired = 3; 
    public bool isFinalBell = false;
    public string storyTitle;
    [TextArea] public string storyMessage;
    public SpriteRenderer bellSprite;
    public Color litColor = Color.yellow;

    private bool hasBeenRung = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenRung || !collision.CompareTag("Player")) return;

        if (GameManager.Instance.echoShards >= shardsRequired)
        {
            hasBeenRung = true;
            if (bellSprite != null) bellSprite.color = litColor;
            GameManager.Instance.RingBell(transform.position, storyTitle, storyMessage, isFinalBell);
        }
        else
        {
            GameManager.Instance.ShowStory("The Bell is Silent",
                $"You need at least {shardsRequired} Echo Shards to ring this Great Bell. Search the area for more shards!");
        }
    }
}