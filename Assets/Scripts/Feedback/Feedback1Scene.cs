using UnityEngine;

public class Feedback1Scene : MonoBehaviour 
{
    public Transform Player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FeedbackEvents.FeedbackPosition.Invoke("PlayerJumpStart", Player.position);
        }
    }
}