using UnityEngine;

public class CameraPositioner 
{
    public void SetPosition(int width, int heigth, float offset)
    {
        Vector3 position = new Vector3(0, 0, offset);

        position.x = (width % 2 == 0) ? width / 2 - 0.5f : width / 2;
        position.y = (heigth % 2 == 0) ? heigth / 2 - 0.5f : heigth / 2;

        Camera.main.transform.position = position;        
    }
}
