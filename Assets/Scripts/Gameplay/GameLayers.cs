using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectsLayer; // Layer of the solid objects
    [SerializeField] LayerMask interactableLayer; // Layer of the interactable objects 
    [SerializeField] LayerMask grassLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask fovLayer;

    public static GameLayers i { get; set; }

    private void Awake()
    {
        i = this;
    }

    public LayerMask SolidLayer
    {
        get
        {
            return solidObjectsLayer;
        }
    }
    public LayerMask InteractableLayer
    {
        get
        {
            return interactableLayer;
        }
    }
    public LayerMask GrassLayer
    {
        get
        {
            return grassLayer;
        }
    }
    public LayerMask PlayerLayer
    {
        get
        {
            return playerLayer;
        }
    }
    public LayerMask FovLayer
    {
        get
        {
            return fovLayer;
        }
    }
}
