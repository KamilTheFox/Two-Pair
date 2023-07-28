using System.Collections;
using UnityEngine;


public class TachControll : MonoBehaviour
{
    private Touch touch;
    private Camera mainCamera;
    ITakeCard takeCard;
    [SerializeField] FinancialSystem financial;
    private void Start()
    {
        mainCamera = Camera.main;
        financial = FinancialSystem.Instance;
    }
    private void Update()
    {
        if (takeCard != null)
            takeCard.transform.position = pointInTable(touch.position);
        if (Input.touchCount <= 0)
            return;
        touch = Input.GetTouch(0);
        Vector2 position = pointInTable(touch.position);
        Collider2D collider = Physics2D.OverlapPoint(position);
        if (collider == null) return;
        if (touch.phase == TouchPhase.Began)
        {
            Destroy(collider.
            gameObject);
            Debug.Log("DeleteCard");
        }
            //GetComponent<ITakeCard>();
    }
    private Vector2 pointInTable(Vector2 vector)
    {
        float valueX = vector.x / Screen.width;
        float valueY = vector.y / Screen.height;
        float DeltaX = Table.Instance.SizeBackground.x;
        float DeltaY = Table.Instance.SizeBackground.y;
        return new Vector2(valueX * DeltaX - DeltaX / 2, valueY * DeltaY - DeltaY / 2);
    }

}