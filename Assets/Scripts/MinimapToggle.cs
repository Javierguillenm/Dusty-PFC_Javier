using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for Button component

public class MinimapToggle : MonoBehaviour
{
    [Header("Minimap References")]
    [Tooltip("Drag the parent GameObject of your minimap (e.g., MinimapContainer) here.")]
    public GameObject minimapContainer; // Assign this in the Inspector

    [Tooltip("Optional: Reference to the button's Text component to change its label.")]
    public Text toggleButtonText; // Assign this if you want to change button text

    private bool isMinimapActive = true; // Initial state (assuming minimap is visible)

    void Start()
    {
        // Ensure the initial state of the minimap matches our variable
        if (minimapContainer != null)
        {
            minimapContainer.SetActive(isMinimapActive);
            UpdateToggleButtonText(); // Update button text on start
        }
        else
        {
            Debug.LogError("Minimap Container not assigned to MinimapToggle script!", this);
        }
    }

    // This method will be called by the UI Button
    public void ToggleMinimap()
    {
        if (minimapContainer != null)
        {
            isMinimapActive = !isMinimapActive; // Invert the state
            minimapContainer.SetActive(isMinimapActive); // Apply the new state
            UpdateToggleButtonText(); // Update the button's text
            Debug.Log("Minimap Toggled. Active: " + isMinimapActive);
        }
    }

    // Optional: Update the button's text based on the minimap's state
    private void UpdateToggleButtonText()
    {
        if (toggleButtonText != null)
        {
            toggleButtonText.text = isMinimapActive ? "Hide Minimap (R)" : "Show Minimap (R)";
        }
    }

    // --- Keyboard Input (Optional - choose one based on your input system) ---

    // --- OPTION A: Legacy Input Manager ---
    void Update()
    {
        // Check for 'R' key press (only on the frame it's pressed)
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleMinimap(); // Call the same toggle function
        }
    }
    // ------------------------------------

 
}