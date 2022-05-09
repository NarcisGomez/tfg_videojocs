using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NoteBehavior : MonoBehaviour
{
    int id;
    [SerializeField] List<Sprite> images;
    Vector3 finalPosition;
    float spawnDistance;
    GameManager gameManager;

    int tempo;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        tempo = gameManager.GetSong().tempo;
    }

    void Update()
    {
        //Update hardcoded number with barCount in the future
        transform.position = Vector3.MoveTowards(transform.position, finalPosition, Time.deltaTime * (tempo * 4));

        if(transform.position == finalPosition)
        {
            Destroy(gameObject);
        }
    }

    public void SetPosition(Vector3 position)
    {
        finalPosition = position;
    }

    public void SetDistance(float distance)
    {
        spawnDistance = distance;
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
}
