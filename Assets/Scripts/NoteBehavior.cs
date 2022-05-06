using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    int id;
    Transform finalPosition;
    GameManager gameManager;
    int tempo;

    void Start()
    {
        id = 38;
        gameManager = GameManager.getInstance();
        tempo = gameManager.GetSong().tempo;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPosition.position, Time.deltaTime * tempo * 3);

        if(transform.position == finalPosition.position)
        {
            Destroy(gameObject);
        }
    }
    public void Init(Transform end)
    {
        finalPosition = end;
    }

    public void SetPosition(Transform position)
    {
        finalPosition = position;
    }

    public int GetId()
    {
        return id;
    }
}
