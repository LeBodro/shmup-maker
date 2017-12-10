using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Theatre : MonoBehaviour
{
    [SerializeField] Text dialogBox;
    [SerializeField] GameObject scene;
    [SerializeField] ActorHolder holder = null;

    Dialog currentDialog;
    System.Action onDone;

    bool IsPlaying { get; set; }

    public void Play(Dialog dialog, System.Action onDone)
    {
        this.onDone = onDone;
        scene.SetActive(true);
        currentDialog = dialog;
        currentDialog.Initialize();
        StartCoroutine(UnlockNextInput());
        Continue();
    }

    IEnumerator UnlockNextInput()
    {
        yield return new WaitForSeconds(0.33f);
        IsPlaying = true;
    }

    void Continue()
    {
        var replica = currentDialog.GetNextReplica();
        holder.SetCharacter(replica.actor);
        dialogBox.text = replica.text;
    }

    void Update()
    {
        if (IsPlaying && Input.GetButtonDown("Next"))
        {
            if (currentDialog.HasNotEnded)
                Continue();
            else
                End();
        }
    }

    void End()
    {
        currentDialog = null;
        IsPlaying = false;
        scene.SetActive(false);
        if (onDone != null)
            onDone();
    }
}
