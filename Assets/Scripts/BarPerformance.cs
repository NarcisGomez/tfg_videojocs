using UnityEngine;

public class BarPerformance : MonoBehaviour
{
    private bool isAtTheTop;
    [SerializeField] GameObject top;
    [SerializeField] GameObject bottom;
    [SerializeField] StatisticsManager statsManager;

    public void Plus()
    {
        if (!isAtTheTop)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 6, transform.position.z);
        }
    }

    public void Minus()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 4, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.Equals(top))
        {
            isAtTheTop = true;
        }
        if (collision.gameObject.Equals(bottom))
        {
            statsManager.EndGame();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isAtTheTop = false;

    }
}
