using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Transform itemHoldPosition; // A point where items will be held (e.g., a child Transform of the Player)
    private GameObject currentItem; // The item currently being held

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Press E to interact
        {
            if (currentItem == null)
            {
                TryPickUpItem();
            }
            else
            {
                DropItem();
            }
        }
    }

    void TryPickUpItem()
    {
        // Raycast to detect an item
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f)) // Adjust distance as needed
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the object is an item
            if (hitObject.CompareTag("Item"))
            {
                PickUpItem(hitObject);
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        currentItem = item;

        // Disable Rigidbody and Collider
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Collider col = item.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // Move item to the hold position
        item.transform.SetParent(itemHoldPosition);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    void DropItem()
    {
        if (currentItem != null)
        {
            // Re-enable Rigidbody and Collider
            Rigidbody rb = currentItem.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            Collider col = currentItem.GetComponent<Collider>();
            if (col != null) col.enabled = true;

            // Detach item from player
            currentItem.transform.SetParent(null);

            // Add slight forward force to simulate dropping
            rb.AddForce(transform.forward * 2f, ForceMode.Impulse);

            currentItem = null;
        }
    }
}
