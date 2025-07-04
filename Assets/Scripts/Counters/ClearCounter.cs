using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {   // There is no kitchen object here
            if (player.HasKitchenObject())
            {   // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {   // Player is not carrying anything

            }
        }
        else
        {   // There is a kitchen object    
            if (player.HasKitchenObject())
            {   // Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {   // Plaer is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        KitchenGameMultiplayer.Instance.DestroyKitchenObject(GetKitchenObject());
                    }
                }
                else
                {   // Player is not carrying Plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {   // Counter is holding a Plate 
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            KitchenGameMultiplayer.Instance.DestroyKitchenObject(player.GetKitchenObject());
                        }
                    }
                }
            }
            else
            {   // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}

