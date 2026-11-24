using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject objectToToggle; 
    public bool setActiveState = false; 
    public SpriteRenderer switchSprite;
    public Color activatedColor = Color.green;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated || !collision.CompareTag("Player")) return;

        activated = true;
        if (objectToToggle != null) objectToToggle.SetActive(setActiveState);
        if (switchSprite != null) switchSprite.color = activatedColor;
        GameManager.Instance.PlaySFX(GameManager.Instance.collectSFX);
    }
}