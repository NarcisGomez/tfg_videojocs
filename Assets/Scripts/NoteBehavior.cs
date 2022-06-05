using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class NoteBehavior : MonoBehaviour
{
    int id;
    bool stop;
    [SerializeField] List<Sprite> images;
    Vector3 finalPosition;
    GameManager gameManager;
    int tempo;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        tempo = int.Parse(gameManager.GetSongInfo().tempo);
    }

    void Update()
    {
        if (!stop)
        {
            //Update hardcoded number with barCount in the future
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, Time.deltaTime * (tempo * 4));

            if (transform.position == finalPosition)
            {
                FindObjectOfType<PracticeManager>().RemoveNote(this);
                Destroy(gameObject);
            }
        }
        
    }

    public void SetPosition(Vector3 position)
    {
        finalPosition = position;
    }

    public void SetImage(string name)
    {
        Image image = gameObject.GetComponent<Image>();

        switch (name)
        {
            case "quarter":
                image.sprite = images[0];
                break;
            case "cross":
                image.sprite = images[1];
                break;
            case "circle":
                image.sprite = images[2];
                break;
            case "star":
                image.sprite = images[3];
                break;
            case "phantom":
                image.color = new Color(255f, 255f, 255f,0f);
                break;
        }
    }

    public int GetId()
    {
        return id;
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void SetStop( bool value)
    {
        stop = value;
    }
}
