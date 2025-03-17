using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    private float tweenHeight = 0.3f;
    private float tweenTime = 1f;
    private float rotateTime = 2f;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if(wasPickedUp)
        {
            Destroy(gameObject);
        }
            
    }

    private void OnEnable()
    {
        Anim();
    }

    private void Anim()
    {
        LeanTween.moveY(gameObject, transform.position.y + tweenHeight, tweenTime).setEaseInOutSine().setLoopPingPong();
        LeanTween.rotateAround(gameObject, Vector3.up, 360, rotateTime).setLoopClamp();
    }

    private void OnDisable()
    {
        LeanTween.cancel(gameObject);
    }
}
