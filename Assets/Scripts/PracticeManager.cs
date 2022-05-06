using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PracticeManager : MonoBehaviour
{
    [SerializeField] TMP_Text songTitle;
    [SerializeField] TMP_Text sectionTitle;
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform finishPoint;
    [SerializeField] GameObject quarter;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.getInstance();
        songTitle.text = gameManager.GetSong().title;
        sectionTitle.text = gameManager.GetSection();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("instance");
            NoteBehavior note = Instantiate(quarter, startingPoint).GetComponent<NoteBehavior>();
            note.SetPosition(finishPoint);
        }
    }
}
