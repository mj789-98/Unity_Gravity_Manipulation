using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Singleton pattern: a static instance to access the InputManager from other scripts
    public static InputManager Instance { get; private set; }

    // Declare input variables
    public Vector2 WASD { get; private set; }
    public bool Space { get; private set; }
    public bool LeftArrowKey { get; private set; }
    public bool RightArrowKey { get; private set; }
    public bool UpArrowKey { get; private set; }
    public bool DownArrowKey { get; private set; }
    public bool Enter { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        // Handle input and update input variables
        WASD = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Space = Input.GetKeyDown(KeyCode.Space);

        LeftArrowKey = Input.GetKey(KeyCode.LeftArrow);
        RightArrowKey = Input.GetKey(KeyCode.RightArrow);
        UpArrowKey = Input.GetKey(KeyCode.UpArrow);
        DownArrowKey = Input.GetKey(KeyCode.DownArrow);

        Enter = Input.GetKeyDown(KeyCode.Return);

    }
}
