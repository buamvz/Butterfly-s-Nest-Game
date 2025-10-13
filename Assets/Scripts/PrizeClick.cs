using UnityEngine;

public class PrizeClick : MonoBehaviour
{
    private Prize prize;

    private void Start()
    {
        prize = GetComponent<Prize>();
    }

    private void OnMouseDown()
    {
        if (prize != null)
        {
            PointCalculator.Instance.AddPrizePoints(prize);

            foreach (PrizeClick other in FindObjectsOfType<PrizeClick>())
                other.enabled = false;

            Debug.Log($"{prize.prizeName} picked");
        }
    }
}