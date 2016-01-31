using UnityEngine;
using System.Collections;

public class MouseOverload : MonoBehaviour {

    public Texture2D cursor;
    public Vector2 size;

	public  static Vector2 mousePosition {
        get {
            Vector2 pos;

            Vector2 axis = new Vector2( Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") );
            pos = 0.5f * ( axis + Vector2.one );
            pos = Vector2.Scale(pos, new Vector2 (Screen.width, Screen.height) );

            return pos;
        }
    }

    void OnGUI(){
        Rect rectPos = new Rect( mousePosition - 0.5f * size, size ); 
        Graphics.DrawTexture( rectPos, cursor );
    }

}
