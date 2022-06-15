using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NoteBehavior : MonoBehaviour
{
    int id;
    bool stop;
    bool right;
    string note;
    [SerializeField] List<Sprite> images;
    Vector3 finalPosition;
    GameManager gameManager;
    int tempo;

    void Start()
    {
        right = false;
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
                PracticeManager practiceManager = FindObjectOfType<PracticeManager>();
                if (practiceManager != null) practiceManager.RemoveNote(this);
                BPM bpm = FindObjectOfType<BPM>();
                if (bpm != null) bpm.RemoveNote(this);
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
                note = "quarter";
                image.sprite = images[0];
                break;
            case "cross":
                note = "cross";
                image.sprite = images[1];
                break;
            case "circle":
                note = "circle";
                image.sprite = images[2];
                break;
            case "star":
                note = "star";
                image.sprite = images[3];
                break;
            case "quarter_right":
                note = "quarter_right";
                image.sprite = images[4];
                break;
            case "cross_right":
                note = "cross_right";
                image.sprite = images[5];
                break;
            case "circle_right":
                note = "circle_right";
                image.sprite = images[6];
                break;
            case "star_right":
                note = "star_right";
                image.sprite = images[7];
                break;
            case "quarter_wrong":
                note = "quarter_wrong";
                image.sprite = images[8];
                break;
            case "cross_wrong":
                note = "cross_wrong";
                image.sprite = images[9];
                break;
            case "circle_wrong":
                note = "circle_wrong";
                image.sprite = images[10];
                break;
            case "star_wrong":
                note = "star_wrong";
                image.sprite = images[11];
                break;
            case "phantom":
                note = "phantom";
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
    public string GetNote()
    {
        return note;
    }
    public Transform GetPosition()
    {
        return transform;
    }
    public bool GetRight()
    {
        return right;
    }
    public void SetRight(bool value)
    {
        right = value;
    }
}
