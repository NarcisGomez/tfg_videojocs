using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarDetector : MonoBehaviour
{
    List<NoteBehavior> notes = new List<NoteBehavior>();
    void Start()
    {
        
    }

    public void HitNote(int note)
    {
            if (notes.Count != 0)
            {
                foreach (NoteBehavior n in notes)
            {
                if(n.GetId() == 38) {
                    notes.Remove(n);
                    Destroy(n.gameObject);
                }
                break;
            }
                
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        notes.Add(collision.gameObject.GetComponent<NoteBehavior>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NoteBehavior note = collision.gameObject.GetComponent<NoteBehavior>();

        if (notes.Contains(note))
        {
            notes.Remove(note);
        }
    }
}
