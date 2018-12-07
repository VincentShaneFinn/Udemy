using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{

    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit _hit;
    public RaycastHit hit
    {
        get { return _hit; }
    }

    Layer _layerHit;
    public Layer currentLayerHit
    {
        get { return _layerHit; }
    }

    public delegate void OnLayerChange(Layer newLayer); //declare a new delegate type
    public event OnLayerChange layerChangeObservers; // instantiate an observer set

    void Start()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                _hit = hit.Value;
                if (_layerHit != layer)
                {
                    _layerHit = layer;
                    layerChangeObservers(_layerHit); // call the delegates
                }
                return;
            }
        }

        // Otherwise return background hit
        _hit.distance = distanceToBackground;
        if (_layerHit != Layer.RaycastEndStop)
        {
            _layerHit = Layer.RaycastEndStop;
            layerChangeObservers(_layerHit); // call the delegates
        }
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
