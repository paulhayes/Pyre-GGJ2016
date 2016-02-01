using UnityEngine;
using System.Collections;

public class MouseOverload : MonoBehaviour {

    public Texture2D cursor;
    public Vector2 size;
    public bool fixedToCenter;

    public static Vector2 lastMouse;
    public static Vector2 lastJoystick;
    public static float lastTimeMouseMoved;
    public static float lastTimeJoystickMoved;

	public  static Vector2 elasticMousePosition {
        get {

            Vector2 pos;
            Vector2 joystickPosition = new Vector2( Input.GetAxis("Elastic X"), Input.GetAxis("Elastic Y") );

            Vector2 axis = joystickPosition;
                Debug.Log(axis);
                pos = 0.5f * ( axis + Vector2.one );
                pos = Vector2.Scale(pos, new Vector2 (Screen.width, Screen.height) );

            return pos;
        }
    }

    public static Vector2 intelligentMousePosition{
        get {
            Vector2 realMousePosition = Input.mousePosition;
            Vector2 joystickPosition = new Vector2( Input.GetAxis("Elastic X"), Input.GetAxis("Elastic Y") );

            Vector2 pos;

            if( Vector2.Distance( realMousePosition, lastMouse ) > 5 ){
                lastTimeMouseMoved = Time.time;
            }

            if( Vector2.Distance( joystickPosition, lastJoystick ) > 0.01 ){
                lastTimeJoystickMoved = Time.time;
            }

            if( lastTimeMouseMoved > lastTimeJoystickMoved ){
                pos = realMousePosition;
            }
            else {
                pos = elasticMousePosition;
            }

            lastMouse = realMousePosition;


            return pos;
        }
    }

    void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    void OnPostRender(){
        if( Camera.current != Camera.main ){
            return;
        }
        GL.PushMatrix ();
        GL.LoadPixelMatrix();

        Vector2 pos = Camera.current.ViewportToScreenPoint( 0.5f * Vector2.one );
        if( !fixedToCenter ){
            pos = intelligentMousePosition;
        } 
        //pos.y = Screen.height-pos.y;
        Rect rectPos = new Rect( pos - 0.5f * size, size ); 
        Graphics.DrawTexture( rectPos, cursor );

        GL.PopMatrix ();
    }

}
