using UnityEngine;

public static class AxisCheck
{
    public static bool CheckPointer(float current, float needed, float cosAllowed)
    {
        float dot = Vector2.Dot(new Vector2(current, 0), new Vector2(needed, 0));

        return dot + 1 >= cosAllowed;
    }
}
