using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossLevel : MonoBehaviour
{
    private PlayerUnityMovement player;

    public GameObject holder;
    public Image panel;
    public Text speechText;
    private bool playedDialogue = false;

    private void Awake()
    {
        speechText.text = "";
        speechText.FadeOut(0);
        panel.FadeOut(0);
        holder.SetActive(false);
        player = GameObject.FindObjectOfType<PlayerUnityMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null && !playedDialogue)
        {
            MusicManager.Instance.ChangeMusicState(MusicState.FinalBossApproach);
            player.enabled = false;
            holder.SetActive(true);
            panel.FadeIn(1);
            StartCoroutine(DialogueFlow());
            playedDialogue = true;
        }
    }

    IEnumerator DialogueFlow()
    {
        yield return new WaitForSeconds(1f);
        speechText.FadeIn(2);
        speechText.text = "I know what you've done";

        yield return new WaitForSeconds(3f);
        speechText.FadeOut(1f);

        yield return new WaitForSeconds(1f);
        speechText.FadeIn(1);
        speechText.text = "All those people you've hurt.";

        yield return new WaitForSeconds(3f);
        speechText.FadeOut(1f);


        yield return new WaitForSeconds(1f);
        speechText.FadeIn(1);
        speechText.text = "You thought you were making them happy? No.";

        yield return new WaitForSeconds(3f);
        speechText.FadeOut(1f);


        yield return new WaitForSeconds(1f);
        speechText.FadeIn(1);
        speechText.text = "Carving smiles into people's faces...";

        yield return new WaitForSeconds(4f);
        speechText.FadeOut(1f);

        yield return new WaitForSeconds(1f);
        speechText.FadeIn(1);
        speechText.text = "...does NOT make them \"happy.\"";

        yield return new WaitForSeconds(4f);
        speechText.FadeOut(1f);

        yield return new WaitForSeconds(1f);
        speechText.FadeIn(1);
        speechText.text = "I'm a Doctor, my friend. You're sick. Let me help you.";

        yield return new WaitForSeconds(3f);

        speechText.FadeOut(1);
        panel.FadeOut(1);
        yield return new WaitForSeconds(1);
        holder.SetActive(false);
        player.enabled = true;

    }
}
