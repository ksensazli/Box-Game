using System;
using DG.Tweening;
using UnityEngine;

public static class TKExtensions
{
    #region COLOR
    public static Color Transparent(this Color i_Color) => SetAlpha(i_Color, 0);
    public static Color SetAlpha(this Color i_Color, float i_Alpha) => new Color(i_Color.r, i_Color.g, i_Color.b, i_Alpha);
    #endregion

    #region MATH
    public static string FormatNumber(int i_Num)
    {
        if (i_Num >= 100000000)
        {
            return (i_Num / 1000000D).ToString("0.#M");
        }
        if (i_Num >= 1000000)
        {
            return (i_Num / 1000000D).ToString("0.##M");
        }
        if (i_Num >= 100000)
        {
            return (i_Num / 1000D).ToString("0.#k");
        }
        if (i_Num >= 10000)
        {
            return (i_Num / 1000D).ToString("0.##k");
        }

        return i_Num.ToString("#,0");
    }
    
    /// /// <summary>
    /// Usefull for getting percentagewise values for 2 different ranges. Example 50 from 1 to 100 is 5 from 1 to 10.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="in_min">Current range min</param>
    /// <param name="in_max">Current range max</param>
    /// <param name="out_min">Target range min</param>
    /// <param name="out_max">Target range max</param>
    /// <returns></returns>
    public static float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (Mathf.Clamp(x, in_min, in_max) - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
    
    public static float ClampAngle(float angle)
    {
        if (angle > 180)
            return angle - 360;
        else
            return angle;
    }
    public static float ClampAngle0360( float relativeAngleFrom, float relativeAngleTo)
    {
      
        float angle = Mathf.Atan2(relativeAngleFrom, relativeAngleTo) * Mathf.Rad2Deg;
        if (angle > 180) angle += 360;

        if (IsBetween(angle,-180,0))
        {
            angle *= -1;
        }
        else
        {
            angle = 360 - angle;
        }

        return angle;
    }

    #endregion


    #region COMPARISIONS
    public static bool IsBetween(this float val, float low, float high)
    {
        return val >= low && val <= high;
    }

    public static bool IsBetween(this int val, int low, int high)
    {
        return val >= low && val <= high;
    }

    public static bool IsBetween(this int val, float low, float high)
    {
        return val >= low && val <= high;
    }
    
    #endregion

    #region TRANSFORM
    public static Vector3 GetWorldScale(this Transform transform)
    {
        Vector3 worldScale = transform.localScale;
        Transform parent = transform.parent;
       
        while (parent != null)
        {
            worldScale = Vector3.Scale(worldScale,parent.localScale);
            parent = parent.parent;
        }
       
        return worldScale;
    }
    public static void SetX(this Transform i_Transform, float i_X)
    {
        i_Transform.position = new Vector3(i_X, i_Transform.position.y, i_Transform.position.z);
    }

    public static void SetY(this Transform i_Transform, float i_Y)
    {
        i_Transform.position = new Vector3(i_Transform.position.x, i_Y, i_Transform.position.z);
    }

    public static void SetZ(this Transform i_Transform, float i_Z)
    {
        i_Transform.position = new Vector3(i_Transform.position.x, i_Transform.position.y, i_Z);
    }

    public static void SetLocalX(this Transform i_Transform, float i_X)
    {
        i_Transform.localPosition = new Vector3(i_X, i_Transform.localPosition.y, i_Transform.localPosition.z);
    }

    public static void SetLocalY(this Transform i_Transform, float i_Y)
    {
        i_Transform.localPosition = new Vector3(i_Transform.localPosition.x, i_Y, i_Transform.localPosition.z);
    }

    public static void SetLocalZ(this Transform i_Transform, float i_Z)
    {
        i_Transform.localPosition = new Vector3(i_Transform.localPosition.x, i_Transform.localPosition.y, i_Z);
    }

    public static void SetLocalScaleX(this Transform i_Transform, float i_X)
    {
        i_Transform.localScale = new Vector3(i_X, i_Transform.localScale.y, i_Transform.localScale.z);
    }

    public static void SetLocalScaleY(this Transform i_Transform, float i_Y)
    {
        i_Transform.localScale = new Vector3(i_Transform.localScale.x, i_Y, i_Transform.localScale.z);
    }

    public static void SetLocalScaleZ(this Transform i_Transform, float i_Z)
    {
        i_Transform.localScale = new Vector3(i_Transform.localScale.x, i_Transform.localScale.y, i_Z);
    }
    #endregion

    public static void ScaleUp(this  Transform target, Action callback = null)
    {
        DOTween.Kill(target);
        target.gameObject.SetActive(true);
        target.DOScale(Vector3.one, GameConfig.Instance.DefaultTweenVariables.DefaultScaleUpTween.Duration)
            .SetEase(GameConfig.Instance.DefaultTweenVariables.DefaultScaleUpTween.Ease)
            .From(Vector3.zero)
            .OnComplete(()=>callback?.Invoke());
    }
    public static void ScaleDown(this  Transform target, Action callback = null)
    {
        DOTween.Kill(target);
        target.DOScale(Vector3.zero, GameConfig.Instance.DefaultTweenVariables.DefaultScaleDownTween.Duration)
            .SetEase(GameConfig.Instance.DefaultTweenVariables.DefaultScaleDownTween.Ease)
            .SetUpdate(UpdateType.Fixed)
            .OnComplete(() =>
            {
                callback?.Invoke();
                target.gameObject.SetActive(false);
            });
    }
    public static void FadeIn(this SpriteRenderer target)
    {
        DOTween.Kill(target);
        target.gameObject.SetActive(true);
        target.DOFade(1, GameConfig.Instance.DefaultTweenVariables.DefaultFadeTween.Duration)
            .SetEase(GameConfig.Instance.DefaultTweenVariables.DefaultFadeTween.Ease)
            .From(0);
    }
    public static void FadeIn(this  CanvasGroup target, Action callback =null)
    {
        DOTween.Kill(target);
        target.gameObject.SetActive(true);
        target.DOFade(1, GameConfig.Instance.DefaultTweenVariables.DefaultFadeTween.Duration)
            .SetEase(GameConfig.Instance.DefaultTweenVariables.DefaultFadeTween.Ease)
            .From(0)
            .OnComplete(() =>
            {
                callback?.Invoke();
            });
    }
    public static void FadeOut(this SpriteRenderer target)
    {
        DOTween.Kill(target);
        target.DOFade(0, GameConfig.Instance.DefaultTweenVariables.DefaultFadeTween.Duration)
            .SetEase(GameConfig.Instance.DefaultTweenVariables.DefaultFadeTween.Ease)
            .From(1);
    }
    public static void FadeOut(this  CanvasGroup target, Action callback = null)
    {
        DOTween.Kill(target);
        target.DOFade(0, GameConfig.Instance.DefaultTweenVariables.DefaultFadeTween.Duration)
            .SetEase(GameConfig.Instance.DefaultTweenVariables.DefaultFadeTween.Ease)
            .OnComplete(()=>
            {
                callback?.Invoke();
                target.gameObject.SetActive(false);
            });
    }
        
    public static float EvaluateCurve(AnimationCurve curve, float targetTime)
    {
        return curve.Evaluate(Mathf.Clamp(targetTime, 0, curve.keys[curve.keys.Length-1].time));
    }
    
    public static Vector3 CalculateVelocity(Vector3 startPos,Vector3 targetPos, float rotationAngle)
    {
        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians
        float angle = rotationAngle * Mathf.Deg2Rad;
 
        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(targetPos.x, 0, targetPos.z);
        Vector3 planarPostion = new Vector3(startPos.x, 0,startPos.z);
 
        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = startPos.y - targetPos.y;
 
        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));
 
        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));
 
        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion);
   
        //Debug.LogError(angleBetweenObjects + "  " + velocity);
        if (targetPos.x<startPos.x)
        {
            angleBetweenObjects *= -1;
        }
        return Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;
    
        // rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
    }
    
}