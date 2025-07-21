using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public string npcName = "Chara";
    public AudioClip healingMusic;
    private AudioSource audioSource;

    [TextArea(2, 5)]
    public string[] dialogueLines;

    public GameObject dialoguePanel;
    public Text nameText;
    public Text dialogueText;

    public GameObject interactPrompt;

    private bool isPlayerInRange = false;
    private int currentLine = 0;
    private bool isDialogueActive = false;

    private WarriorController warriorController;
    private CameraZoom cameraZoom;
    public string[] initialDialogueLines;
    public string[] questDialogueLines;
    public string[] completedDialogueLines;
    public bool isQuestNPC = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
        interactPrompt?.SetActive(false);
        cameraZoom = Camera.main.GetComponent<CameraZoom>();
        audioSource = GameObject.FindWithTag("AudioManager")?.GetComponent<AudioSource>();

    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive)
                StartDialogue();
            else
                ShowNextLine();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        interactPrompt?.SetActive(false);
        nameText.text = npcName;

        if (isQuestNPC)
        {
            if (!SideQuestManager.Instance.questAccepted)
            {
                dialogueLines = initialDialogueLines; // “Please find the Magic Crystal!”
                SideQuestManager.Instance.questAccepted = true;
            }
            else if (SideQuestManager.Instance.questAccepted && !SideQuestManager.Instance.crystalFound)
            {
                dialogueLines = questDialogueLines; // “Did you find it yet?”
            }
            else if (SideQuestManager.Instance.crystalFound && !SideQuestManager.Instance.questCompleted)
            {
                dialogueLines = completedDialogueLines;
                SideQuestManager.Instance.CompleteQuest();

                // 🔊 Play healing music
                if (audioSource != null && healingMusic != null)
                {
                    audioSource.clip = healingMusic;
                    audioSource.Play();
                }

                // ❤️ Heal and add XP
                if (warriorController != null && !warriorController.GetComponent<WarriorHealth>().IsDead())
                {
                    warriorController.GetComponent<WarriorHealth>().HealToFull();

                    // 🌟 Add XP
                    PlayerStats stats = warriorController.GetComponent<PlayerStats>();
                    if (stats != null)
                    {
                        stats.GainXP(20);
                        Debug.Log("✅ Gained 20 XP from quest!");
                    }
                    else
                    {
                        Debug.LogWarning("PlayerStats not found on player!");
                    }
                }
            }


            else
            {
                dialogueLines = new string[] { "Thanks again, hero!" };
            }
        }

        dialogueText.text = dialogueLines[0];
        currentLine = 0;

        if (warriorController != null)
            warriorController.isFrozen = true;

        cameraZoom?.ZoomIn();
    }

    void ShowNextLine()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        currentLine = 0;

        if (warriorController != null)
            warriorController.isFrozen = false;

        cameraZoom?.ZoomOut();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactPrompt?.SetActive(true);
            warriorController = other.GetComponent<WarriorController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactPrompt?.SetActive(false);

            if (isDialogueActive)
                EndDialogue();
        }
    }
}
