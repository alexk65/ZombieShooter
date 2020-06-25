using TMPro;
using UnityEngine;

public class HPLabelScript : MonoBehaviour
{
    private TextMeshProUGUI text;
    private GameObject player;
    private PlayerController playerController;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

   
    void FixedUpdate()
    {
        text.SetText($"HP: {playerController.health}");
    }
}
