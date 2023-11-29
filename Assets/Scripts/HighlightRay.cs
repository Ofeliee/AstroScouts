using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.XR;

//Hey Unicorns :) there is alot of comments here i have marked the ones i think might be nore importent for future work with a "###" the rest is for general understanding and learning /Oscar 
//### The function of this script is to highlight "IsHighlightable" objects and too call the code to get the object in your palm


public class HighlightRay : MonoBehaviour
{

    // public variables in the MonoBehaviour or inherited from MonoBehaviour is automatically serialized, meaning that it will show up in the inspector of the script as a Object field .

    public float sphereCastRadius = 0.5f;
    public LayerMask interactionLayers;
    public Material highlightMaterial;
    private GameObject lastHitObject = null;

    // Saves Gameobjects as Keys and Materials as Values
    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();

    void Update()
    {
        RaycastHit hit;
        //### Notice the transform.UP when the script is added to another object this can make it so that the ray goes in the wrong direction, then test with "transform.forward etc" 
        bool isHit = Physics.SphereCast(transform.position, sphereCastRadius, transform.forward, out hit, Mathf.Infinity, interactionLayers);

        //### IsHighlightable is the unity tag that is used to distingush the game object you want to be highlightable, add the tag to the objects you want.
        //!### Add If "hand is open" here. or "While", but that might not be needed since this is in Update, hmm.
        if (isHit && hit.collider.CompareTag("IsHighlightable"))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (lastHitObject != hitObject)
            {
                if (lastHitObject != null)
                {
                    RemoveHighlight(lastHitObject);
                }

                ApplyHighlight(hitObject);
                TriggerHapticFeedback();
                lastHitObject = hitObject;
            }
        }
        else
        {
            if (lastHitObject != null)
            {
                RemoveHighlight(lastHitObject);
                lastHitObject = null;
            }
        }
    }

    void ApplyHighlight(GameObject obj)
    {
        if (!originalMaterials.ContainsKey(obj))
            originalMaterials[obj] = obj.GetComponent<Renderer>().material;

        obj.GetComponent<Renderer>().material = highlightMaterial;
    }

    void RemoveHighlight(GameObject obj)
    {
        if (originalMaterials.TryGetValue(obj, out Material originalMat))
        {
            obj.GetComponent<Renderer>().material = originalMat;
            originalMaterials.Remove(obj);
        }
    }

    void TriggerHapticFeedback()
    {
        // Add haptic fedback, might need to add some oculus package. Read documentation.
    }
}