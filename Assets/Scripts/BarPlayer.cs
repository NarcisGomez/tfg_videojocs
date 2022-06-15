using System.Collections.Generic;
using UnityEngine;

public class BarPlayer : MonoBehaviour
{

    List<NoteBehavior> notes = new List<NoteBehavior>();
    [SerializeField] StatisticsManager statsManager;
    [SerializeField] BarPerformance barPerformance;

    public void HitNote(int note)
    {
        if (notes.Count != 0)
        {
            foreach (NoteBehavior n in notes)
            {
                if (n.GetId() == note)
                {
                    statsManager.AddHitNote();
                    barPerformance.Plus();
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
        statsManager.AddPlayedNote();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NoteBehavior note = collision.gameObject.GetComponent<NoteBehavior>();

        if (notes.Contains(note))
        {
            statsManager.AddMissedNote();
            barPerformance.Minus();
            notes.Remove(note);
        }
    }
}
